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
    private bool controlsDisplayed = false;
    private float originalTimeScale;
    private PlayerController pc;
    private float fixedDeltaTime;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject ControlScreen;
    private SoundAction[] soundActions;


    // === TELEMETRY STUFF ===
    public Telemetry.PlayerMovement playerMovement;
    public Telemetry.PuzzleCompletion puzzleCompletion;
    public Telemetry.ControlScreen controlScreenTracking;
    public Telemetry.OverallStats overallstats;


    // === New VARIABLES ===
    public float total_run_time = 0f;
    public float current_puzzle_timestamp = 0f;


    private void Awake()
    {
        rumblers = Resources.FindObjectsOfTypeAll<RumbleAction>();
        TorchSounds = Resources.FindObjectsOfTypeAll<TorchSounds>();
        soundActions = Resources.FindObjectsOfTypeAll<SoundAction>();
        _controls = new PlayerControls();
        _controls.GameManager.ReloadScene.performed += context => ReloadScene();
        _controls.GameManager.ViewControls.performed += context =>
        {
            if (!controlsDisplayed) ControlsShow();
            else if (controlsDisplayed) ControlsHide();
        };
        _controls.GameManager.PauseGame.performed += context =>
        {
            if (!paused) PauseGame();
            else if (paused) UnPauseGame();
        };
        originalTimeScale = Time.timeScale;
        if (pauseMenu == null) GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
        ControlScreen.SetActive(false);

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
        total_run_time += Time.deltaTime;
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
        puzzleCompletion.endTime = total_run_time;
        StartCoroutine(Telemetry.SubmitPuzzleCompletion(puzzleCompletion));
        //reset puzzle info
        puzzleCompletion.puzzle_name = "";
    }

    private static float lastDeath;

    public void SubmitPlayerDeath()
    {
        if (lastDeath == total_run_time) return;
        lastDeath = total_run_time;
        var data = new Telemetry.PlayerDeath
        {
            puzzle_name = puzzleCompletion.puzzle_name,
            timestamp = total_run_time
        };
        // update overall
        overallstats.total_deaths += 1;
        StartCoroutine(Telemetry.SubmitPlayerDeath(data));
    }

    public void SubmitControlScreen()
    {
        StartCoroutine(Telemetry.SubmitControlScreen(controlScreenTracking));
    }

    public void SubmitLockPuzzleStat(int state, bool sucessful)
    {
        var data = new Telemetry.LockPuzzleStat
        {
            state = state,
            successful = sucessful,
            timestamp = total_run_time
        };
        StartCoroutine(Telemetry.SubmitLockPuzzleStat(data));
    }

    public void SubmitPuzzleSolving(float dur, string correctNotes, string playedNotes)
    {
        var data = new Telemetry.PuzzleSolvingStats
        {
            sing_duration = dur,
            current_correct_notes = correctNotes,
            notes_played = playedNotes,
            puzzle_name = puzzleCompletion.puzzle_name,
            timestamp = total_run_time
        };

        StartCoroutine(Telemetry.SubmitPuzzleSolving(data));
    }

    public void SubmitOverallData()
    {
        overallstats.time_to_beat = total_run_time;
        StartCoroutine(Telemetry.SubmitOverallData(overallstats));
    }

    public void ControlsShow()
    {
        ControlScreen.SetActive(true);
        controlsDisplayed = true;
        //telemetry below
        controlScreenTracking.OpenedTimestamp = total_run_time;
        controlScreenTracking.puzzle_name = playerMovement.checkpoint;
    }

    public void ControlsHide()
    {
        ControlScreen.SetActive(false);
        controlsDisplayed = false;
        //telemetry below
        controlScreenTracking.ClosedTimestamp = total_run_time;
        SubmitControlScreen();
    }

    public void PauseGame()
    {
        foreach (SoundAction sa in soundActions)
        {
            sa.PauseInstance(true);
        }
        foreach (RumbleAction ra in rumblers)
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