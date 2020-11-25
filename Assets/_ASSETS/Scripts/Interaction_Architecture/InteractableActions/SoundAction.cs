using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_SoundAction { STOPPED, ACTIVE, PLAYING }
public class SoundAction : InteractableAction
{

    [EventRef]
    public string SoundEvent;

    [SerializeField] private bool playOnce;
    [SerializeField] float volume = 1;
    [SerializeField] float stopLoopAfterDuration = 0;
    private bool played = false;
    private float timer = 0.0f;
    private EventInstance eventInstance;
    private State_SoundAction state = State_SoundAction.ACTIVE;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }
    public override void Activate()
    {
        if (state == State_SoundAction.STOPPED) return;
        if (playOnce && played) return;
        else if (!IsPlaying(eventInstance))
        {
            eventInstance.setVolume(volume);
            eventInstance.start();
            state = State_SoundAction.PLAYING;
            played = true;
        }
    }

    public override void Deactivate()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        played = false;
    }

    public override void InputData(SongData data)
    {
    }

    public override void Reset()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playOnce)
        {
            if (state == State_SoundAction.PLAYING)
            {
                timer += Time.deltaTime;
                if (timer > stopLoopAfterDuration)
                {
                    eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    played = false;
                    state = State_SoundAction.STOPPED;
                }
            }
        }
    }

    bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }
}
