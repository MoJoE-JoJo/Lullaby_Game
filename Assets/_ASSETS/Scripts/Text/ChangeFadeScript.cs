using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFadeScript : MonoBehaviour
{
    SpriteRenderer rend;
    public FadeSpriteScript objectToFade;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.localPosition = new Vector3(-1, 0, 0);
            objectToFade.StartFading();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectToFade.StartFadingOut();

        }
    }
}