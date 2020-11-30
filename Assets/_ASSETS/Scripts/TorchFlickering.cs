using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchFlickering : MonoBehaviour
{
    public float BaseIntensity
    {
        get => baseIntensity;
        set => baseIntensity = value;
    }
    private Light2D lightSource;
    private float baseIntensity;
    private float baseInnerRadius;
    private float baseOuterRadius;
    [SerializeField] private float flickeringPercentage = 4;
    [SerializeField] private bool useIntensity = true;
    [SerializeField] private bool useRadius;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light2D>();
        baseIntensity = lightSource.intensity;
        baseInnerRadius = lightSource.pointLightInnerRadius;
        baseOuterRadius = lightSource.pointLightOuterRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if(useIntensity) lightSource.intensity = Random.Range(baseIntensity * (1.0f - flickeringPercentage * Time.deltaTime), baseIntensity * (1.0f + flickeringPercentage * Time.deltaTime));
        if (useRadius)
        {
            lightSource.pointLightInnerRadius = Random.Range(baseInnerRadius * (1.0f - flickeringPercentage * Time.deltaTime), baseInnerRadius * (1.0f + flickeringPercentage * Time.deltaTime));
            lightSource.pointLightOuterRadius = Random.Range(baseOuterRadius * (1.0f - flickeringPercentage * Time.deltaTime), baseOuterRadius * (1.0f + flickeringPercentage * Time.deltaTime));
        }
    }
}
