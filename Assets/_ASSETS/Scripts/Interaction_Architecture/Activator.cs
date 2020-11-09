using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Activator : MonoBehaviour
{
    [SerializeField] protected InteractableAction action;
    public abstract void SongInput(SongData data);
    public abstract void ShowHint();
}