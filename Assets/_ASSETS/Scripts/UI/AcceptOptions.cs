using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptOptions : MonoBehaviour
{
    private LightManager lm;
    [SerializeField] private Slider brightnessSlider;
    // Start is called before the first frame update
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LightManager").GetComponent<LightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyOptions()
    {
        PlayerPrefs.SetFloat("brightness", brightnessSlider.value/10.0f);
        PlayerPrefs.Save();
        //lm.UpdateLights(brightnessSlider.value/10.0f);
    }
}
