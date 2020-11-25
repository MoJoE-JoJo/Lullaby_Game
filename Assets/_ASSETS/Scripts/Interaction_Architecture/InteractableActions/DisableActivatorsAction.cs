﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableActivatorsAction : InteractableAction
{
    [SerializeField] private List<Activator> activators;

    public override void Activate()
    {
        foreach(Activator acti in activators)
        {
            acti.SetEnabled(false);
        }
    }

    public override void Deactivate()
    {
        foreach (Activator acti in activators)
        {
            acti.SetEnabled(true);
        }
    }

    public override void InputData(SongData data)
    {
        //Nothing is supposed to happen here, it is not supposed to react to any input
    }

    public override void Reset()
    {
        Deactivate();
    }

}
