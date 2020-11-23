using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeAction : InteractableAction
{
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;
    public CameraShake shaker;

    private float originalShakeDuration;
    private float originalShakeAmount;
    private float originalDecreaseFactor;
    
    public override void Activate()
    {
        shaker.ShakeAmount = shakeAmount;
        shaker.ShakeDuration = shakeDuration;
        shaker.DecreaseFactor = decreaseFactor;
    }

    public override void Deactivate()
    {
        shaker.ShakeDuration = 0.0f;
    }

    public override void InputData(SongData data)
    {
        //DOes nothing
    }

    public override void Reset()
    {
        shaker.ShakeDuration = originalShakeDuration;
        shaker.ShakeAmount = originalShakeAmount;
        shaker.DecreaseFactor = originalDecreaseFactor;
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
