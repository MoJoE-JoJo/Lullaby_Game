using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSound : MonoBehaviour, ISelectHandler
{
    private EventInstance PercEvent;

    [EventRef]
    public string Perc;
    [SerializeField] private float PercVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PercEvent = RuntimeManager.CreateInstance(Perc);
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartInstance(PercEvent, PercVolume);
    }

    private void StartInstance(EventInstance eventInstance, float volume)
    {
        eventInstance.setVolume(volume);
        eventInstance.start();
    }


}
