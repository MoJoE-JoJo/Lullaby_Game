using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : Activator
{
    public GameObject lantern;

    void Start()
    {
        lantern.SetActive(true);
    }

    public override void SongInput(SongData data)
    {
        //throw new System.NotImplementedException();
    }

    public override void ShowHint()
    {
        //throw new System.NotImplementedException();
    }


}