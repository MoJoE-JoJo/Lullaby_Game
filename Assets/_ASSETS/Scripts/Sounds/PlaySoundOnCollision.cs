using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [EventRef]
    public string SoundEvent;
    [SerializeField] private bool useProximity = false;
    [SerializeField] private float radius = 20;
    [SerializeField] private float minAirTime = 0.2f;
    [SerializeField] private bool usingParameter = false;
    [SerializeField] private FModparameter FmodParameter;

    [Serializable]
    private class FModparameter
    {
        [SerializeField] public string parameter;
        [SerializeField] public float value;
    }

    private float timer;
    private bool isGrounded;
    private Transform player;
    private Transform thisTransform;
    private EventInstance eventInstance;
    // Start is called before the first frame update
    void Awake()
    {
        eventInstance = RuntimeManager.CreateInstance(SoundEvent);
        eventInstance.setParameterByName("Box", 1);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();
        timer = 0.0f;

        if (usingParameter) eventInstance.setParameterByName(FmodParameter.parameter, FmodParameter.value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dist = Vector3.Distance(player.position, thisTransform.position);
        if (dist < radius)
        {
            if (!collision.gameObject.CompareTag("MainCamera") &&
             !collision.gameObject.CompareTag("LoadZone") && 
             !collision.gameObject.CompareTag("Player") &&
             !collision.gameObject.CompareTag("Activator") &&
             collision.gameObject.layer != LayerMask.NameToLayer("Camera"))
            {
                if (timer >= minAirTime) eventInstance.start();
                isGrounded = true;
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dist = Vector3.Distance(player.position, thisTransform.position);
        if (dist < radius)
        {
            if (!collision.gameObject.CompareTag("MainCamera") &&
             !collision.gameObject.CompareTag("LoadZone") &&
             !collision.gameObject.CompareTag("Player") &&
             !collision.gameObject.CompareTag("Activator") &&
             collision.gameObject.layer != LayerMask.NameToLayer("Camera"))
            {
                if (timer >= minAirTime) eventInstance.start();
                isGrounded = true;
                timer = 0.0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded) timer += Time.deltaTime;
    }
}
