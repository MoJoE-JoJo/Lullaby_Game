using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private PlayerControls _controls;
    private bool paused = false;
    private float originalTimeScale;
    private PlayerController pc;
    private float fixedDeltaTime;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.GameManager.ReloadScene.performed += context => ReloadScene();
        _controls.GameManager.PauseGame.performed += context => PauseGame();
        originalTimeScale = Time.timeScale;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0.0f;
            pc.enabled = false;
            pc.Animator.enabled = false;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            paused = true;
        }
        else if (paused)
        {
            Time.timeScale = originalTimeScale;
            pc.enabled = true;
            pc.Animator.enabled = true;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            paused = false;
        }
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