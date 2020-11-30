using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSpriteScript : MonoBehaviour
{
    SpriteRenderer rend;
    public bool hasFaded;
    public bool pressedFadeIn;
    public bool pressedFadeout;
    public bool isInside;
    public bool isOutside;
    public float fadeInSpeed = .02f;
    public float fadeOutSpeed = .02f;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        
        //rend.material.color = c;
        rend.material.color = new Color(1, 1, 1, 0);
        //StartCoroutine("FadeOut");
    }

    private void Update()
    {
 
    }

    IEnumerator FadeIn()
    {
        if (isInside) { 
        for (float f = 0.0f; f <= 1 + 1; f += fadeInSpeed)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
            hasFaded = true;
        }
            rend.material.color = new Color(1, 1, 1, 1);
            yield return null;
        }
        else {
            rend.material.color = new Color(1, 1, 1, 0);
        }
    }


    IEnumerator FadeOut()
    {
        if (isOutside)
        {
            for (float g = 1.0f; g >= -0.05f; g -= fadeOutSpeed)
        {
            Color d = rend.material.color;
            d.a = g;
            rend.material.color = d;
            yield return new WaitForSeconds(0.05f);
            hasFaded = false;
        }
            rend.material.color = new Color(1, 1, 1, 0);
            yield return null;
        }
    }

       public void StartFading()
       {
        isInside = true;
        isOutside = false;
        if (!hasFaded)
           {
               StartCoroutine("FadeIn");
               
           }
       }

       public void StartFadingOut()
       {
        isInside = false;
        isOutside = true;
           if (hasFaded)
           {
               StartCoroutine("FadeOut");
               
           }
           else
        {
            rend.material.color = new Color(1, 1, 1, 0);
        }
       }
    
}



