using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSpriteScript : MonoBehaviour
{
    SpriteRenderer rend;
    public bool hasFaded;
    public bool pressed;
    public float timeToFinish;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
        
    }

    private void Update()
    {
        if (pressed == true)
        {
            StartFading();
            pressed = false;
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.0f; f <= 1 + 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFading()
    {
        if (!hasFaded)
        {
            StartCoroutine("FadeIn");
            hasFaded = true;
        }


    }
}

