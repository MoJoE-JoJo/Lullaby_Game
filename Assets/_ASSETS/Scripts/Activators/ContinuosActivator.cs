using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_ContinuosActivator { IDLE, ACTIVATE, RECEIVING, DEACTIVATE }
public class ContinuosActivator : Activator
{
    public override void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public override void SongInput(SongData data)
    {
        throw new System.NotImplementedException();
    }

    
}
