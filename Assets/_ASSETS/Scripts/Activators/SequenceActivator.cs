using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum State_SequenceActivator {EMPTYBUFFER, PARTIALLYFULLBUFFER, FULLBUFFER, CORRECTSEQUENCE, WRONGSEQUENCE, FINISHED}
/*
public class SequenceActivator : Activator
{
    private State_SequenceActivator state = State_SequenceActivator.EMPTYBUFFER;
    [SerializeField] private InteractableAction action;
    [SerializeField] private InteractableUIController interactableUI;
    private List<SongData> inputBuffer = new List<SongData>();
    [SerializeField] private List<Song_Note> correctSequence;
    [SerializeField] private bool canDeactivate = false;
    private bool nextIsDeactivate = false;


    override public void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    override public void SongInput(SongData data)
    {
        if (state == State_SequenceActivator.EMPTYBUFFER || state == State_SequenceActivator.PARTIALLYFULLBUFFER)
        {
            state = State_SequenceActivator.PARTIALLYFULLBUFFER;
            inputBuffer.Add(data);
            if (inputBuffer.Count == correctSequence.Count) state = State_SequenceActivator.FULLBUFFER;
        }
    }
    //This version of a SequenceActivator will stop the user if it registers that the user has inputted data that cannot lead to a correct sequence
    void Update()
    {
        if (state == State_SequenceActivator.PARTIALLYFULLBUFFER)
        {
            for (int i = 0; i < inputBuffer.Count; i++)
            {
                if (inputBuffer[i].Notes != correctSequence[i]) state = State_SequenceActivator.WRONGSEQUENCE;
                else interactableUI.ActivateAt(i, inputBuffer[i].Notes);
            }
        }
        if(state == State_SequenceActivator.FULLBUFFER)
        {
            state = State_SequenceActivator.CORRECTSEQUENCE;
            for (int i = 0; i < inputBuffer.Count; i++)
            {
                if (inputBuffer[i].Notes != correctSequence[i]) state = State_SequenceActivator.WRONGSEQUENCE;
                else interactableUI.ActivateAt(i, inputBuffer[i].Notes);
            }
        }
        if(state == State_SequenceActivator.WRONGSEQUENCE)
        {
            inputBuffer.Clear();
            interactableUI.InputBufferCleared();
            state = State_SequenceActivator.EMPTYBUFFER;
        }
        if (state == State_SequenceActivator.CORRECTSEQUENCE)
        {
            if (canDeactivate)
            {
                if (nextIsDeactivate)
                {
                    action.Deactivate();
                    nextIsDeactivate = false;
                }
                else
                {
                    action.Activate();
                    nextIsDeactivate = true;
                }
                inputBuffer.Clear();
                interactableUI.InputBufferCleared();
                state = State_SequenceActivator.EMPTYBUFFER;

            }
            else
            {
                action.Activate();
                state = State_SequenceActivator.FINISHED;
            }
        }
    }
}
*/