using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Facing { RIGHT, LEFT }

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
    [SerializeField] private float wheelDeactivateDelay;
    private float accTimer = 0.0f;
    private float decTimer = 0.0f;
    private PlayerControls _controls;
    private ActivatorSensor actiSensor;
    private Rigidbody2D _rb2d;
    //private bool _jumpFlag;
    public bool _isGrounded;
    private Vector2 _move;
    private float songDelayTimer;
    private Coroutine _deactivateWheel;
    private float _volume;

    private int ground; 
    //Animation stuff
    private Animator _animator;
    private Facing _facing = Facing.RIGHT;
    private GameObject _legs;
    private Animator _legsAnimator;
    [SerializeField]private float playerTurningThreshold;
    private Coroutine _invert = null;

    private JumpPlayer jumpSoundPlayer;
    private float jumpTimer = 0.0f;

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
    public Animator Animator
    {
        get => _animator;
    }

    public int getGround()
    {
        return ground;
    }
    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.NoteSelector.Move.performed += context => ActivateNoteSelector();
        _controls.NoteSelector.Move.canceled += context => DeactivateNoteSelector();

        _controls.Player.Move.performed += context => _move = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => _move = Vector2.zero;

        _controls.NoteSelector.Sing.performed += context => Sing(context);
        _controls.NoteSelector.Sing.canceled += context => StopSing();

        _controls.Player.Jump.performed += context => Jump();

    }

    // Start is called before the first frame update
    void Start()
    {
        jumpSoundPlayer = GetComponent<JumpPlayer>();
        _legs = transform.Find("Legs").gameObject;
        _legsAnimator = _legs.GetComponent<Animator>();
        _animator = GetComponent<Animator>();
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
        //Debug.Log(_isSinging);
        
        //Debug.Log(_songBeingSung);
        UpdateAnimation();

        if (!_isGrounded) 
        {
            jumpTimer += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        UpdateNoteController();

        SingToActivators();

        //UpdateNoteController();

        //SingToActivators();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
        _isSinging = false;
        noteController.IsSinging = _isSinging;
        _animator.SetFloat("RunSpeed", 0.0f);
        _animator.SetFloat("JumpSpeed", 0.0f);
        _animator.SetBool("IsGrounded", true);

        _legsAnimator.SetFloat("RunSpeed", 0.0f);
        _legsAnimator.SetFloat("JumpSpeed", 0.0f);
        _legsAnimator.SetBool("IsGrounded", true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("MainCamera") &&
            !collision.gameObject.CompareTag("LoadZone") &&
            collision.gameObject.layer != LayerMask.NameToLayer("Camera") &&
            !_isGrounded)
        {
            _isGrounded = true;
            if (jumpTimer > jumpSoundPlayer.minDuration) jumpSoundPlayer.playSound();
        }
        if (collision.gameObject.GetComponent<IsElevatorScript>() != null)
        {
            if (collision.gameObject.GetComponent<IsElevatorScript>().isElevator) ground = 2;
            else ground = 1;
        }
        else ground = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        jumpTimer = 0.0f;
        _isGrounded = false;
    }

    private void ActivateNoteSelector()
    {
        if (!noteSelector.activeSelf)
        {
            noteSelector.GetComponent<NoteSelectorNew>().CenterNoteSelector();
            noteSelector.SetActive(true);
        }

        if (_deactivateWheel != null)
        {
            StopCoroutine(_deactivateWheel);
        }
    }
    void DeactivateNoteSelector()
    {
        _deactivateWheel = StartCoroutine(DeactivateWheel());
    }

    private IEnumerator DeactivateWheel()
    {
        yield return new WaitForSeconds(wheelDeactivateDelay);
        noteSelector.SetActive(false);
        //_isSinging = false;
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
                _songBeingSung.Duration = Time.deltaTime;
                actiSensor.SendSong(_songBeingSung);
            }
            else if (songDelayTimer < songDelay) songDelayTimer += Time.deltaTime;
        }
        else if(songDelayTimer > 0.0001f && !_isSinging)
        {
            //Debug.Log(songDelayTimer);
            songDelayTimer = 0.0f;
        }
    }

    private void UpdateNoteController()
    {
        //Debug.Log(_songBeingSung.Notes.ToString());
        noteController.SongData = _songBeingSung;
        noteController.IsSinging = _isSinging;
    }

    private void Sing(InputAction.CallbackContext context)
    {
        _isSinging = true;
        _volume = EaseVolume(context.ReadValue<float>()); 
        _songBeingSung.Volume = _volume;

    }

    private void StopSing()
    {
        _isSinging = false;
    }
    
    private void UpdateAnimation()
    {
        Facing wasFacing = _facing;
        

        if (wasFacing == Facing.RIGHT && _move.x < -playerTurningThreshold)
        {
            if (_invert == null)
            {
                _invert = StartCoroutine(InvertPlayer(Facing.LEFT));
            }
        }
        if (wasFacing == Facing.LEFT && _move.x > playerTurningThreshold)
        {
            if (_invert == null)
            {
                _invert = StartCoroutine(InvertPlayer(Facing.RIGHT));
            }
        }
        
        //Player animator
        _animator.SetFloat("RunSpeed", Mathf.Abs(_rb2d.velocity.x));
        _animator.SetFloat("JumpSpeed", _rb2d.velocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetBool("IsSinging", _isSinging);
        _animator.SetFloat("SingVolume", _volume);
        
        //Legs animator
        _legsAnimator.SetFloat("RunSpeed", Mathf.Abs(_rb2d.velocity.x));
        _legsAnimator.SetFloat("JumpSpeed", _rb2d.velocity.y);
        _legsAnimator.SetBool("IsGrounded", _isGrounded);
        _legsAnimator.SetBool("IsSinging", _isSinging);

        var currentClip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (currentClip == "Player_Running")
        {
            _legsAnimator.speed = GetAnimatorSpeedLegs(Mathf.Abs(_rb2d.velocity.x / playerSpeed));
            _animator.speed = GetAnimatorSpeedBody(Mathf.Abs(_rb2d.velocity.x / playerSpeed));
        }
        else
        {
            _legsAnimator.speed = 1f;
            _animator.speed = 1f;
        }
    }
    
    float GetAnimatorSpeedBody(float moveX)
    {
        return (0.85f + moveX * (1-0.85f));
    }

    float GetAnimatorSpeedLegs(float moveX)
    {
        return (0.80f + moveX * (1-0.80f));
    }
    
    float EaseVolume(float volume)
    {
        return volume;
        
        float vol = 1.1f - Mathf.Pow(1-volume, 3);
        if (vol >= 1.0f)
        {
            vol = 1.0f;
        }

        return vol;
    }

    private IEnumerator InvertPlayer(Facing facing)
    {
        yield return new WaitForSeconds(0.001f);

        switch (facing)
        {
            case Facing.LEFT:
                if (_move.x < -playerTurningThreshold)
                {
                    //Debug.Log("Player x vel: " + _rb2d.velocity.x);
                    transform.Rotate(new Vector3(0,-180,0));
                    _facing = Facing.LEFT;
                }
                break;
            case Facing.RIGHT:
                if (_move.x > playerTurningThreshold)
                {
                    //Debug.Log("Player x vel: " + _rb2d.velocity.x);
                    transform.Rotate(new Vector3(0,-180,0));
                    _facing = Facing.RIGHT;
                }
                break;
        }

        _invert = null;

    }
    
}
