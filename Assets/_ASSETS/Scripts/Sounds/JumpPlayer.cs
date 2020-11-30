using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }

    public void playLandSound() 
    {
        eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
