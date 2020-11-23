using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAction : InteractableAction
{
    // Start is called before the first frame update
    [SerializeField] private GameObject thing;

    public override void Activate()
    {
        thing.SetActive(false);
    }

    public override void Deactivate()
    {
        //thing.SetActive(true);
    }

    public override void InputData(SongData data)
    {
    }

    public override void Reset()
    {
        thing.SetActive(true);
    }
}
