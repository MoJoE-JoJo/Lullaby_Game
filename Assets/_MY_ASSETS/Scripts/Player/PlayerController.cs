using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject noteSelector;
    private PlayerControls _controls;
    [SerializeField]
    private RegisterActivators activators;

    [SerializeField]
    private SongData _songBeingSung;
    [SerializeField]
    private bool _isSinging;

    [SerializeField]
    private SongData _songBeingSung;
    [SerializeField]
    private bool _isSinging;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.NoteSelector.Move.performed += context => ActivateNoteSelector();
        _controls.NoteSelector.Move.canceled += context => DeactivateNoteSelector();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (noteSelector.activeSelf)
        {
            
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
}
