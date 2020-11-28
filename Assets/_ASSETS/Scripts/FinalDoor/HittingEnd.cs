using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class HittingEnd : MonoBehaviour
{
    //[SerializeField] private Light2D doorLight;
    [SerializeField] private GameObject whiteOverlay;
    private SpriteRenderer fadeSprite;
    //[SerializeField] private float newIntensity;
    [SerializeField] private float fadeTime;
    //private float originalIntensity;
    private float fadeTimer = 0.0f;
    private PlayerController pc;
    private bool end = false;
    [SerializeField] private bool endCredits = false;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        fadeSprite = whiteOverlay.GetComponent<SpriteRenderer>();
        Color tmp = fadeSprite.color;
        tmp.a = 0;
        fadeSprite.color = tmp;
        //originalIntensity = doorLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            whiteOverlay.transform.position = pc.transform.position;
            if(fadeTimer >= fadeTime)
            {
                SceneManager.LoadScene("End");
            }
            fadeTimer += Time.deltaTime;

            Color tmp = fadeSprite.color;
            tmp.a = fadeTimer / fadeTime;
            fadeSprite.color = tmp;
        }
        else if (endCredits)
        {

            whiteOverlay.transform.position = pc.transform.position;
            if (fadeTimer >= fadeTime)
            {
                gameObject.SetActive(false);
            }
            fadeTimer += Time.deltaTime;

            // fade to solid color
            Color tmp = fadeSprite.color;
            tmp.a = 1.0f - (fadeTimer / fadeTime);
            fadeSprite.color = tmp;
        }

        /*
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
         */
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
