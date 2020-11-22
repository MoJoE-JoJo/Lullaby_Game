using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeLightAction : InteractableAction
{
    [SerializeField] private Light2D lightSource;
    [SerializeField] private float OnActivateIntensity;
    [SerializeField] private bool changeLightColor;
    [SerializeField] private Color lightColor;
    [SerializeField] private bool usePressure = false;
    [SerializeField] private bool changeLightColorBack = false;
    private float originalIntensity;
    private Color originalColor;
    private SongData lastData;

    // Start is called before the first frame update
    void Start()
    {
        originalIntensity = lightSource.intensity;
        originalColor = lightSource.color;
    }

    public override void Activate()
    {
        if (changeLightColor) lightSource.color = lightColor;
        if (usePressure) lightSource.intensity = OnActivateIntensity * lastData.Volume;
        else lightSource.intensity = OnActivateIntensity;
    }

    public override void Deactivate()
    {
        lightSource.intensity = originalIntensity;
        if(changeLightColorBack) lightSource.color = originalColor;
    }

    public override void InputData(SongData data)
    {
        lastData = data;
        lightSource.intensity = OnActivateIntensity * data.Volume;
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
