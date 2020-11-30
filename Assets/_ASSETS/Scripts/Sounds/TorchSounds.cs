using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchSounds : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] float radius = 3;
    private Transform player;
    private Transform torch;
    private EventInstance eventInstance;
    private Light2D torchLight;
    // Start is called before the first frame update
    void Start()
    {
        torchLight = GetComponent<Light2D>();
        //find player and save ref to transform
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        torch = GetComponent<Transform>();
        //init fmod event.
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }

    // Update is called once per frame
    void Update()
    {
        //if transform is within distance, start playing sounds,
        //set value closeToTorch" base on the percent the player is close to the torch

        var dist = Vector3.Distance(player.position, torch.position);
        
        if (dist < radius && torchLight.intensity > 0.5)
        {
            if (!IsPlaying(eventInstance))
            {
                eventInstance.start();
            }
            var closeValue = 1 - (dist / radius);
            eventInstance.setParameterByName("closeToObject", closeValue);
        }
        else
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out PLAYBACK_STATE state);
        return state != PLAYBACK_STATE.STOPPED;
    }
}
