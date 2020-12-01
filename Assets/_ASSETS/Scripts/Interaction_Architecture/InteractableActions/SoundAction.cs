using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_SoundAction { STOPPED, READY, PLAYING }
public class SoundAction : InteractableAction
{

    [EventRef]
    public string SoundEvent;

    [SerializeField] private bool playOnce;
    [SerializeField] private bool dontLoopOnActive = false;
    [SerializeField] private bool oneFrameLateStart = false;
    [SerializeField] private float volume = 1.0f;
    [SerializeField] private float stopLoopAfterDuration = 0;
    [SerializeField] private bool useProximity = false;
    [SerializeField] private float radius = 5;
    [SerializeField] private bool usingParameter = false;
    [SerializeField] private FModparameter FmodParameter;
    [SerializeField] private bool playFromStart = false;


    private bool played = false;
    private float timer = 0.0f;
    private Transform player;
    private Transform thisTransform;
    private EventInstance eventInstance;

    [Serializable]
    private class FModparameter
    {
        [SerializeField] public string parameter;
        [SerializeField] public float value;
        [SerializeField] public float timeBeforeChange; //the time for changing the parameter
        [SerializeField] public float startValue;
    }

    [SerializeField]private State_SoundAction state = State_SoundAction.READY;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();

        if (playFromStart)
        {
            eventInstance.setVolume(volume);
            if (!oneFrameLateStart) eventInstance.start();
            else StartCoroutine(OneFrameLateStart());
            state = State_SoundAction.PLAYING;

            played = true;
        }
    }
    public override void Activate()
    {
        if (state == State_SoundAction.STOPPED) return;
        if (playOnce && played) return;
        if (dontLoopOnActive && played) return;
        else if (!IsPlaying(eventInstance))
        {
            if (usingParameter) setParameter(FmodParameter.startValue);
            if (useProximity)
            {
                UpdateParamterByProximity();
            }

            eventInstance.setVolume(volume);

            if (!oneFrameLateStart) eventInstance.start();
            else StartCoroutine(OneFrameLateStart());
            state = State_SoundAction.PLAYING;
            played = true;
        }
    }

    public override void Deactivate()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        if (usingParameter) setParameter(FmodParameter.startValue);
        timer = 0.0f;
        played = false;
    }

    public override void InputData(SongData data)
    {
        return;
    }

    public override void Reset()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (useProximity) UpdateParamterByProximity();

        if (!playOnce)
        {
            if (state == State_SoundAction.PLAYING)
            {
                timer += Time.deltaTime;
                if (usingParameter)
                {
                    if (timer > FmodParameter.timeBeforeChange) setParameter(FmodParameter.value);
                }
                if (stopLoopAfterDuration == 0) return;
                else if (timer > stopLoopAfterDuration)
                {
                    eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    played = false;
                    if(playOnce) state = State_SoundAction.STOPPED;
                    else state = State_SoundAction.READY;
                }
            }
        }
    }

    private void setParameter(float value)
    {
        eventInstance.setParameterByName(FmodParameter.parameter, value);
    }

    private void UpdateParamterByProximity()
    {
        var dist = Vector3.Distance(player.position, thisTransform.position);
        if (dist < radius)
        {
            eventInstance.setVolume(1);
            var closeValue = 1 - (dist / radius);
            eventInstance.setParameterByName("closeToObject", closeValue);
        }
        else eventInstance.setVolume(0);
    }

    private bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }

    IEnumerator OneFrameLateStart()
    {
        yield return null;
        eventInstance.start();
    }

    public void PauseInstance(bool setPaused) 
    {
        eventInstance.setPaused(setPaused);
    }
}
