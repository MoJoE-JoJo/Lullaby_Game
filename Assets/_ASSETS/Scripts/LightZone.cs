using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightZone : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float newIntensity;
    [SerializeField] private float changeTime;
    private float originalIntensity;
    private bool changeIntensity = false;
    private float timer = 0.0f;

    private void Start()
    {
        if (globalLight == null) globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeIntensity)
        {
            timer += Time.deltaTime;
            var fraction = timer / changeTime;
            globalLight.intensity = Mathf.Lerp(originalIntensity, newIntensity, fraction);
        }
        if (timer > changeTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"));
        originalIntensity = globalLight.intensity;
        changeIntensity = true;
    }
}
