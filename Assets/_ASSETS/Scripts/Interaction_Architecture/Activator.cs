using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Activator : MonoBehaviour
{
    [SerializeField] protected List<InteractableAction> actions;
    public abstract void SongInput(SongData data);
    public abstract void ShowHint();

    public virtual void SetEnabled(bool input) 
    {
        enabled = input;
    }
}