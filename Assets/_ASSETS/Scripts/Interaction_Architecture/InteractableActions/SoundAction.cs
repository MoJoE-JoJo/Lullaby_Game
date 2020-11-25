using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAction : InteractableAction
{

    [EventRef]
    public string SoundEvent;

    [SerializeField] private bool playOnce;
    [SerializeField] private bool oneFrameLateStart = false;
    [SerializeField] private float volume = 1.0f;

    private bool played = false;
    private EventInstance eventInstance;
    private SongData songData;


    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }
    public override void Activate()
    {

        if (playOnce && played) return;
        else if (!IsPlaying(eventInstance))
        {
            eventInstance.setVolume(volume);
            if (!oneFrameLateStart) eventInstance.start();
            else StartCoroutine(OneFrameLateStart());
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
        songData = data;
    }

    public override void Reset()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }

    IEnumerator OneFrameLateStart()
    {
        yield return null;
        eventInstance.start();
    }
}
