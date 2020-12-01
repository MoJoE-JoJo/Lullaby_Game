using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoundAction : InteractableAction
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] private bool usingParameter = false;
    [SerializeField] private FModparameter FmodParameter;

    [Serializable]
    private class FModparameter
    {
        [SerializeField] public string parameter;
        [SerializeField] public float value;
    }


    private EventInstance eventInstance;
    // Start is called before the first frame update


    void Start()
    {
        if (usingParameter) eventInstance.setParameterByName(FmodParameter.parameter, FmodParameter.value);
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Activate()
    {
        eventInstance.start();
    }

    public override void Deactivate()
    {
        return;
    }

    public override void InputData(SongData data)
    {
        return;
    }

    public override void Reset()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
