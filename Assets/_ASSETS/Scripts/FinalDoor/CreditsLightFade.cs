using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CreditsLightFade : MonoBehaviour
{
    [SerializeField] private Light2D fadeLight;
    [SerializeField] private Light2D roomLight;
    private float endIntensity;
    [SerializeField] private float fadeTime;
    [SerializeField] private float startIntensity;
    [SerializeField] private float startScale;
    private float endScale;
    private float fadeTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        endIntensity = fadeLight.intensity;
        endScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer += Time.deltaTime;
        var fraction = fadeTimer / fadeTime;
        fadeLight.intensity = Mathf.SmoothStep(startIntensity, endIntensity, fraction);
        var newLocalScale = new Vector3(Mathf.SmoothStep(startScale, endScale, fraction), Mathf.SmoothStep(startScale, endScale, fraction), 1.0f);
        transform.localScale = newLocalScale;
        if(fadeTimer >= fadeTime)
        {
            roomLight.enabled = true;
            fadeLight.enabled = false;
        }
    }
}
