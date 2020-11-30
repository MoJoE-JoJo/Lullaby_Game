using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChangeNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("brightness", 1.0f)*10.0f;
    }

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", 1.0f) * 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChanged()
    {
        numberText.text = (slider.value/10.0f).ToString();
    }
}
