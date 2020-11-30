using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeLightAction : InteractableAction
{
    public float OnActivateIntensity
    {
        get => onActivateIntensity;
        set => onActivateIntensity = value;
    }
    public float OriginalIntensity
    {
        get => originalIntensity;
        set => originalIntensity = value;
    }
    [SerializeField] private Light2D lightSource;
    [SerializeField] private float onActivateIntensity;
    [SerializeField] private bool changeLightColor;
    [SerializeField] private Color lightColor;
    [SerializeField] private bool usePressure = false;
    [SerializeField] private bool changeLightColorBack = false;
    private float originalIntensity;
    private Color originalColor;
    private SongData lastData;

    private void Awake()
    {
        originalIntensity = lightSource.intensity;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalColor = lightSource.color;
    }

    public override void Activate()
    {
        if (changeLightColor) lightSource.color = lightColor;
        if (usePressure) lightSource.intensity = onActivateIntensity * lastData.Volume;
        else lightSource.intensity = onActivateIntensity;
    }

    public override void Deactivate()
    {
        lightSource.intensity = originalIntensity;
        if(changeLightColorBack) lightSource.color = originalColor;
    }

    public override void InputData(SongData data)
    {
        lastData = data;
        if (usePressure) lightSource.intensity = onActivateIntensity * data.Volume;
    }

    public override void Reset()
    {
        //lightSource.intensity = originalIntensity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
