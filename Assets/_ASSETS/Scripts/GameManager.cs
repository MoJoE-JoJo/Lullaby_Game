using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    private float originalTimeScale;
    private PlayerController pc;
    private float fixedDeltaTime;
    [SerializeField] private GameObject pauseMenu;
    private SoundAction[] soundActions;


    // === TELEMETRY STUFF ===
    public Telemetry.PlayerMovement playerMovement;
    public Telemetry.PuzzleCompletion puzzleCompletion;


    // === New VARIABLES ===
    public float total_run_timestamp = 0f;
    public float current_puzzle_timestamp = 0f;


    private void Awake()
    {
        rumblers = Resources.FindObjectsOfTypeAll<RumbleAction>();
        TorchSounds = Resources.FindObjectsOfTypeAll<TorchSounds>();
        soundActions = Resources.FindObjectsOfTypeAll<SoundAction>();
        _controls = new PlayerControls();
        _controls.GameManager.ReloadScene.performed += context => ReloadScene();
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

        // == data telemetry stuff
        if (Instance == null)
        {
            Instance = this;
            //this is what generates a unique id for the Game session. 
            Telemetry.GenerateNewRunID();
        }
    }
    private void Start()
    {
        ClearTelemetryData();
    }

    private void Update()
    {
        total_run_timestamp += Time.deltaTime;
        if (true) // add condition for when a puzzle is entered
        { 
            current_puzzle_timestamp += Time.deltaTime;
        }
    }


    //This function make sure to clear all the data. 
    public void ClearTelemetryData()
    {
        playerMovement.checkpoint = "";
        playerMovement.timestamp = TimeSpan.Zero;
    }

    public void SubmitPlayerMovement()
    {
        StartCoroutine(Telemetry.SubmitPlayerMovement(playerMovement));
    }

    public void SubmitPuzzleCompletion()
    {
        StartCoroutine(Telemetry.SubmitPuzzleCompletion(puzzleCompletion));
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