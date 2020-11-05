using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject noteSelector;
    [SerializeField] private SongData _songBeingSung;
    [SerializeField] private bool _isSinging;
    [SerializeField, Tooltip("Time in seconds between the character starts singing at full, and the signal starts getting sent to nearby activators")] private float songDelay;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    private PlayerControls _controls;
    private ActivatorSensor actiSensor;
    private Rigidbody2D _rb2d;
    private bool _jumpFlag;
    private bool _isGrounded;
    private Vector2 _move;
    private float songDelayTimer;
    public List<Activator> Activators
    {
        get => actiSensor.RegisteredActivators;
    }

    private void Awake()
    {
        _controls = new PlayerControls();
        actiSensor = GetComponent<ActivatorSensor>();

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
        songDelayTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        SingToActivators();
    }

    private void FixedUpdate()
    {
        if (_jumpFlag && _isGrounded)
        {
            _rb2d.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            _jumpFlag = false;
            _isGrounded = false;
        }
    }

    private void ActivateNoteSelector()
    {
        if (!noteSelector.activeSelf)
        {
            noteSelector.GetComponent<NoteSelector>().CenterNoteSelector();
            noteSelector.SetActive(true);
        }
    }
    
    void DeactivateNoteSelector()
    {
        noteSelector.SetActive(false);
        _isSinging = false;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
    }

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

    void MovePlayer()
    {
        Vector2 move = new Vector2(_move.x, 0) * (playerSpeed * Time.deltaTime);
        transform.Translate(move, Space.World);
    }

    void Jump()
    {
        if (_isGrounded)
        {
            _jumpFlag = true;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }*/

    private void OnCollisionStay2D(Collision2D other)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void SingToActivators()
    {
        if (_isSinging)
        {
            if (songDelayTimer >= songDelay)
            {
                Debug.Log("yolo");
                actiSensor.SendSong(_songBeingSung);
            }
            songDelayTimer += Time.deltaTime;
        }
        else if(songDelayTimer > 0.0001f && !_isSinging)
        {
            Debug.Log(songDelayTimer);
            songDelayTimer = 0.0f;
        }
    }
    
}
