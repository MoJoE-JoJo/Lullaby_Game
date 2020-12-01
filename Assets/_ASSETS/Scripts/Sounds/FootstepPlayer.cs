using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FootstepPlayer : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] private PlayerController playerController;
    private EventInstance eventInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
    }

    public void PlaySound() {
        var value = playerController.getGround();
        eventInstance.setParameterByName("Footsteps", value);
        eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
