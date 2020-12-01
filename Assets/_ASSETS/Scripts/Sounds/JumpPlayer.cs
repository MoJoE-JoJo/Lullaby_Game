using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] public float minDuration = 0.3f;

    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }

    public void playSound() 
    {
        eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
