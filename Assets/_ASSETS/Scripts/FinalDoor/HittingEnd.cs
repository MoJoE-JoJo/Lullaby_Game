using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class HittingEnd : MonoBehaviour
{
    [SerializeField] private Light2D doorLight;
    [SerializeField] private float newIntensity;
    [SerializeField] private float fadeTime;
    private float originalIntensity;
    private float fadeTimer = 0.0f;
    private PlayerController pc;
    private bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        originalIntensity = doorLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            if(fadeTimer >= fadeTime)
            {
                SceneManager.LoadScene("End");
            }
            fadeTimer += Time.deltaTime;
            var fraction = fadeTimer / fadeTime;
            doorLight.intensity = Mathf.SmoothStep(originalIntensity, newIntensity, fraction);
            //doorLight.intensity += intensityIncrease * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            end = true;
            pc.enabled = false;
            //SceneManager.LoadScene("End");
        }
    }
}
