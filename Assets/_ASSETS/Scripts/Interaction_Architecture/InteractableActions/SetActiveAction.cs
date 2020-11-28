using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAction : InteractableAction
{
    // Start is called before the first frame update
    [SerializeField] private GameObject thing;
    [SerializeField] private bool onActivationSetActive;
    [SerializeField] private bool canDeactivate = false;

    public override void Activate()
    {
        thing.SetActive(onActivationSetActive);
    }

    public override void Deactivate()
    {
       if (canDeactivate) thing.SetActive(!onActivationSetActive);
    }

    public override void InputData(SongData data)
    {
    }

    public override void Reset()
    {
        thing.SetActive(!onActivationSetActive);
    }
}
