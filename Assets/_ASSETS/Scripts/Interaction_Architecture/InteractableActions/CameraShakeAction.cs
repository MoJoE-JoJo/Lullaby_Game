using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeAction : InteractableAction
{
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;
    [SerializeField] private bool shakeOnce;
    public CameraShake shaker;

    private bool hasShaken = false;
    private float originalShakeDuration;
    private float originalShakeAmount;
    private float originalDecreaseFactor;
    
    public override void Activate()
    {
        if (shakeOnce)
        {
            if (hasShaken) return;

            shaker.ShakeAmount = shakeAmount;
            shaker.ShakeDuration = shakeDuration;
            shaker.DecreaseFactor = decreaseFactor;
            hasShaken = true;
        }
        else { 
             shaker.ShakeAmount = shakeAmount;
            shaker.ShakeDuration = shakeDuration;
            shaker.DecreaseFactor = decreaseFactor;
        }


    }

    public override void Deactivate()
    {
        shaker.ShakeDuration = 0.0f;
    }

    public override void InputData(SongData data)
    {
        //Does nothing
    }

    public override void Reset()
    {
        shaker.ShakeDuration = originalShakeDuration;
        shaker.ShakeAmount = originalShakeAmount;
        shaker.DecreaseFactor = originalDecreaseFactor;
        hasShaken = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        originalShakeDuration = shaker.ShakeDuration;
        originalShakeAmount = shaker.ShakeAmount;
        originalDecreaseFactor = shaker.DecreaseFactor;
    }

}
