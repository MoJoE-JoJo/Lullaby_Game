using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_InverterAction { DEACTIVATED, ACTIVATED}

public class InverterAction : InteractableAction
{
    [SerializeField] private State_InverterAction state = State_InverterAction.DEACTIVATED;
    [SerializeField, Tooltip("If true, will Deactivate itself and send Deactivate to all of its actions, when it gets an Activate")] private bool useActivate;
    [SerializeField, Tooltip("If true, will Activate itself and send Activate to all of its actions, when it gets a Deactivate")] private bool useDeactivate;
    [SerializeField] private List<InteractableAction> actions = new List<InteractableAction>();
    private SongData lastSongData = new SongData();

    private void Update()
    {
        if (state == State_InverterAction.ACTIVATED)
        {
            foreach (InteractableAction action in actions)
            {
                action.InputData(lastSongData);
            }
        }
    }
    public override void Activate()
    {
        if (useActivate)
        {
            state = State_InverterAction.DEACTIVATED;
            foreach (InteractableAction action in actions)
            {
                action.Deactivate();
            }
        }
    }

    public override void Deactivate()
    {
        if (useDeactivate)
        {
            state = State_InverterAction.ACTIVATED;
            foreach (InteractableAction action in actions)
            {
                action.Activate();
            }
        }
    }

    public override void InputData(SongData data)
    {
        lastSongData = data;
    }

    public override void Reset()
    {
        foreach (InteractableAction action in actions)
        {
            action.Reset();
        }
    }
}
