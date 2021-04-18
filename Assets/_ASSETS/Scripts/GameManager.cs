using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundAction[] SoundActions
    {
        get => soundActions;
        set => soundActions = value;
    }
    public TorchSounds[] TorchSounds
    {
        get;
        set;
    }
    private RumbleAction[] rumblers;
    private PlayerControls _controls;
    private bool paused = false;
    private bool controlsDisplayed = false;
    private float originalTimeScale;
    private PlayerController pc;
    private float fixedDeltaTime;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject ControlScreen;
    private SoundAction[] soundActions;

    private void Awake()
    {
        rumblers = Resources.FindObjectsOfTypeAll<RumbleAction>();
        TorchSounds = Resources.FindObjectsOfTypeAll<TorchSounds>();
        soundActions = Resources.FindObjectsOfTypeAll<SoundAction>();
        _controls = new PlayerControls();
        _controls.GameManager.ReloadScene.performed += context => ReloadScene();
        _controls.GameManager.ViewControls.performed += context =>
        {
            if (!controlsDisplayed) ControlsView();
            else if (controlsDisplayed) ControlsHide();
        };
        _controls.GameManager.PauseGame.performed += context =>
        {
            if (!paused) PauseGame();
            else if (paused) UnPauseGame();
        };
        originalTimeScale = Time.timeScale;
        if(pauseMenu == null) GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
        ControlScreen.SetActive(false);
    }

    public void ControlsView()
    {
        ControlScreen.SetActive(true);
        controlsDisplayed = true;
    }

    public void ControlsHide()
    {
        ControlScreen.SetActive(false);
        controlsDisplayed = false;
    }

    public void PauseGame()
    {
        foreach(SoundAction sa in soundActions)
        {
            sa.PauseInstance(true);
        }
        foreach(RumbleAction ra in rumblers)
        {
            ra.Deactivate();
        }
        Time.timeScale = 0.0f;
        pc.enabled = false;
        pc.Animator.enabled = false;
        Time.fixedDeltaTime = float.PositiveInfinity;
        pauseMenu.SetActive(true);
        paused = true;
    }

    public void UnPauseGame()
    {
        foreach (SoundAction sa in soundActions)
        {
            sa.PauseInstance(false);
        }
        Time.timeScale = originalTimeScale;
        pc.enabled = true;
        pc.Animator.enabled = true;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        pauseMenu.SetActive(false);
        paused = false;
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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