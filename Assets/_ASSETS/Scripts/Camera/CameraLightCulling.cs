using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CameraLightCulling : MonoBehaviour
{
    [SerializeField] private float triggerSizeMultiplierX = 1.25f;
    [SerializeField] private float triggerSizeMultiplierY = 1.25f;
    [SerializeField] private BoxCollider2D cameraCollider;
    [SerializeField] private Light2D globalLight;
    private BoxCollider2D collider;
    private ShadowCaster2D[] shadows;

    private Light2D[] lights;


    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        LoadLights();
    }

    void Start()
    {
    }

    private void Update()
    {
        globalLight.enabled = true;
        var cameraSize = cameraCollider.size;
        cameraSize.x *= triggerSizeMultiplierX;
        cameraSize.y *= triggerSizeMultiplierY;
        collider.size = cameraSize;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var light = collision.GetComponent<Light2D>();
        var shadow = collision.GetComponent<ShadowCaster2D>();
        if (light != null) light.enabled = true;
        if (shadow != null) shadow.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var light = collision.GetComponent<Light2D>();
        var shadow = collision.GetComponent<ShadowCaster2D>();
        if (light != null) light.enabled = false;
        if (shadow != null) shadow.enabled = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("StartScreen"))
        {
            collider = GetComponent<BoxCollider2D>();
            LoadLights();
        }
    }

    private void LoadLights()
    {
        
        lights = Resources.FindObjectsOfTypeAll<Light2D>();
        foreach (Light2D light in lights)
        {
            light.enabled = false;
        }

        shadows = Resources.FindObjectsOfTypeAll<ShadowCaster2D>();
        foreach(ShadowCaster2D sc2d in shadows)
        {
            sc2d.enabled = false;
        }
    }

    

    }
