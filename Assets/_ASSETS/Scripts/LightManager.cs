using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum State_LightManager { INMENU, INGAME};

public class LightManager : MonoBehaviour
{
    /*
    private State_LightManager state = State_LightManager.INMENU;
    private MiniwheelSegmentHandler[] hints;
    private List<float> hintsBaseLow = new List<float>();
    private List<float> hintsBaseHigh = new List<float>();
    
    private TorchFlickering[] flickerings;
    private List<float> flickBaseInten = new List<float>();
    
    private LightZone[] lightZones;
    //private List<float> zonesBaseOld = new List<float>();
    private List<float> zonesBaseNew = new List<float>();
    
    private ChangeLightAction[] lightChangers;
    private List<float> lightChangersActivateIntensity = new List<float>();
    private List<float> lightChangersOriginalIntensity = new List<float>();
    
    private Light2D[] lights;
    private List<float> lightsBaseInten = new List<float>();

    //Add change light action


    public float lightMultiplier;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LightManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        lightMultiplier = PlayerPrefs.GetFloat("brightness", 1.0f);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("StartScreen"))
        {
            lightMultiplier = PlayerPrefs.GetFloat("brightness", 1.0f);
            state = State_LightManager.INGAME;
            LoadLights();
            UpdateLights(lightMultiplier);
        }
    }

    private void LoadLights()
    {
        hints = Resources.FindObjectsOfTypeAll<MiniwheelSegmentHandler>();
        foreach(MiniwheelSegmentHandler msh in hints)
        {
            hintsBaseLow.Add(msh.LowIntensity);
            hintsBaseHigh.Add(msh.HighIntensity);
        }

        flickerings = Resources.FindObjectsOfTypeAll<TorchFlickering>();
        foreach(TorchFlickering tf in flickerings)
        {
            flickBaseInten.Add(tf.BaseIntensity);
        }

        lightZones = Resources.FindObjectsOfTypeAll<LightZone>();
        foreach (LightZone lz in lightZones)
        {
            //zonesBaseOld.Add(lz.StartIntensity);
            zonesBaseNew.Add(lz.EndIntensity);
        }

        lightChangers = Resources.FindObjectsOfTypeAll<ChangeLightAction>();
        foreach(ChangeLightAction cla in lightChangers)
        {
            lightChangersActivateIntensity.Add(cla.OnActivateIntensity);
            lightChangersOriginalIntensity.Add(cla.OriginalIntensity); 
        }

        lights = Resources.FindObjectsOfTypeAll<Light2D>();
        foreach(Light2D light in lights)
        {
           lightsBaseInten.Add(light.intensity);
        }
    }

    public void UpdateLights(float multiplier)
    {
        lightMultiplier = multiplier;
        if(state == State_LightManager.INGAME)
        {
            for(int i = 0; i<hints.Length; i++)
            {
                hints[i].LowIntensity = lightMultiplier * hintsBaseLow[i];
                hints[i].HighIntensity = lightMultiplier * hintsBaseHigh[i];
            }
            for (int i = 0; i < flickerings.Length; i++)
            {
                flickerings[i].BaseIntensity = lightMultiplier * flickBaseInten[i];
            }
            for (int i = 0; i < lightZones.Length; i++)
            {
                //lightZones[i].StartIntensity = lightMultiplier * zonesBaseOld[i];
                lightZones[i].EndIntensity = lightMultiplier * zonesBaseNew[i];
            }
            for (int i = 0; i < lightChangers.Length; i++)
            {
                lightChangers[i].OnActivateIntensity = lightMultiplier * lightChangersActivateIntensity[i];
                lightChangers[i].OriginalIntensity = lightMultiplier * lightChangersOriginalIntensity[i];
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = lightMultiplier * lightsBaseInten[i];
            }
        }
    }
    */
}
