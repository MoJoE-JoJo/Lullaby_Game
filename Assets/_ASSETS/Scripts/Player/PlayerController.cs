using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private NoteController noteController;
    private GameObject noteSelector;
    [SerializeField] private SongData _songBeingSung;
    [SerializeField] private bool _isSinging;
    [SerializeField, Tooltip("Time in seconds between the character starts singing at full, and the signal starts getting sent to nearby activators")] private float songDelay;
    [SerializeField] private float playerSpeed;
    [SerializeField, Tooltip("Must be above 0.")] private float accelerationTime;
    [SerializeField] private float jumpForce;
    private float accTimer = 0.0f;
    private float decTimer = 0.0f;
    private PlayerControls _controls;
    private ActivatorSensor actiSensor;
    private Rigidbody2D _rb2d;
    //private bool _jumpFlag;
    public bool _isGrounded;
    private Vector2 _move;
    private float songDelayTimer;

    public SongData SongBeingSung
    {
        get => _songBeingSung;
        set => _songBeingSung = value;
    }
    public bool IsSinging
    {
        get => _isSinging;
        set => _isSinging = value;
    }

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.NoteSelector.Move.performed += context => ActivateNoteSelector();
        _controls.NoteSelector.Move.canceled += context => DeactivateNoteSelector();

        _controls.Player.Move.performed += context => _move = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => _move = Vector2.zero;

        _controls.Player.Jump.performed += context => Jump();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        actiSensor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ActivatorSensor>();
        noteController = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<NoteController>();
        noteSelector = GameObject.FindGameObjectWithTag("NoteWheel");
        DeactivateNoteSelector();
        songDelayTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdateNoteController();
        SingToActivators();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }

    private void ActivateNoteSelector()
    {
        if (!noteSelector.activeSelf)
        {
            noteSelector.GetComponent<NoteSelectorNew>().CenterNoteSelector();
            noteSelector.SetActive(true);
        }
    }
    void DeactivateNoteSelector()
    {
        noteSelector.SetActive(false);
        _isSinging = false;
    }

    void MovePlayer()
    {
        if(_move.x != 0.0f)
        {
            if (decTimer > 0.0f) decTimer = 0.0f;
            if(accTimer < accelerationTime) accTimer += Time.deltaTime;
            accTimer = Mathf.Clamp(accTimer, 0.0f, accelerationTime);
            float t = accTimer / accelerationTime;
            Vector2 speed = new Vector2(Mathf.SmoothStep(0.0f, playerSpeed * _move.x, t), _rb2d.velocity.y);
            _rb2d.velocity = speed;
        }       
        if (_move.x == 0.0f)
        {
            if (accTimer > 0.0f) accTimer = 0.0f;
            if (decTimer < accelerationTime) decTimer += Time.deltaTime;
            decTimer = Mathf.Clamp(decTimer, 0.0f, accelerationTime);
            float t = decTimer / accelerationTime;
            Vector2 speed = new Vector2(Mathf.SmoothStep(_rb2d.velocity.x, 0.0f, t), _rb2d.velocity.y);
            _rb2d.velocity = speed;
            
        }
    }

    void Jump()
    {
        if (_isGrounded)
        {
            _rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void SingToActivators()
    {
        if (_isSinging)
        {
            if (songDelayTimer >= songDelay)
            {
                //Debug.Log("yolo");
                _songBeingSung.Duration = Time.deltaTime;
                actiSensor.SendSong(_songBeingSung);
            }
            if (songDelayTimer < songDelay) songDelayTimer += Time.deltaTime;
        }
        else if(songDelayTimer > 0.0001f && !_isSinging)
        {
            //Debug.Log(songDelayTimer);
            songDelayTimer = 0.0f;
        }
    }

    private void UpdateNoteController()
    {
        noteController.SongData = _songBeingSung;
        noteController.IsSinging = _isSinging;
    }
    
}
