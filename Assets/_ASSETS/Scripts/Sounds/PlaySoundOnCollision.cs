using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
        eventInstance.setParameterByName("Box", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("MainCamera") &&
         !collision.gameObject.CompareTag("LoadZone") &&
         collision.gameObject.layer != LayerMask.NameToLayer("Camera")) eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
