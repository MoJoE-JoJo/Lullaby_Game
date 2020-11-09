﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableAction : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void InputData(SongData data);
}
