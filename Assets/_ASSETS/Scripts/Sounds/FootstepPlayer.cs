using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FootstepPlayer : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
        eventInstance.setParameterByName("Footsteps", 1);
    }

    public void PlaySound() {
        eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
