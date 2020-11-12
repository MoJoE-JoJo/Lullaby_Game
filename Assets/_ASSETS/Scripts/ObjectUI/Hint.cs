using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Hint : MonoBehaviour
{
    //set active on UI gameobject to false
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    //set active on UI gameobject to true
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public abstract void ShowNextHint(SongData sd); //show the next chord/note to be played

    public abstract void HighlightHint(SongData sd); // highlight a hint (hopefully in the form of a glow), for example when you are continuously activating or just got done with a sequence 

    public abstract void NoLightHint(SongData sd);
}
