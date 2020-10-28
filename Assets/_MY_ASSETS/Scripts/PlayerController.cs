using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject noteSelector;
    private PlayerControls _controls;

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
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateNoteSelector();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            DeactivateNoteSelector();
        }*/
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
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
    }
}
