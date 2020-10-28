using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivator
{
    void SongInput(SongData data);
    void ShowHint();
}