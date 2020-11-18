using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeLightAction : InteractableAction
{
    [SerializeField] private Light2D lightSource;
    [SerializeField] private float OnActivateIntensity;
    private float originalIntensity;

    // Start is called before the first frame update
    void Start()
    {
        originalIntensity = lightSource.intensity;
    }

    public override void Activate()
    {
        lightSource.intensity = OnActivateIntensity;
    }

    public override void Deactivate()
    {
        lightSource.intensity = originalIntensity;
    }

    public override void InputData(SongData data)
    {
       // dont care
    }

    public override void Reset()
    {
        lightSource.intensity = originalIntensity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
