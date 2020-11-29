using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] private bool useProximity = false;
    [SerializeField] private float radius = 20;
    private Transform player;
    private Transform thisTransform;
    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Start()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
        eventInstance.setParameterByName("Box", 1);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dist = Vector3.Distance(player.position, thisTransform.position);
        if (dist < radius)
        {
            if (!collision.gameObject.CompareTag("MainCamera") &&
             !collision.gameObject.CompareTag("LoadZone") &&
             collision.gameObject.layer != LayerMask.NameToLayer("Camera")) eventInstance.start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
