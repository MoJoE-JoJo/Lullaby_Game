using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_SequenceActivator {EMPTYBUFFER, PARTIALLYFULLBUFFER, FULLBUFFER, CORRECTSEQUENCE, WRONGSEQUENCE, FINISHED}

public class SequenceActivator : MonoBehaviour, IActivator
{
    public State_SequenceActivator state = State_SequenceActivator.EMPTYBUFFER;
    public InteractableAction action;
    public InteractableUIController interactableUI;
    public List<SongData> inputBuffer;
    public List<Song_NoteCoord> correctSequence;
    public bool canDeactivate = false;
    private bool nextIsDeactivate = false;


    public void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public void SongInput(SongData data)
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
                if (inputBuffer[i].NoteCoord != correctSequence[i]) state = State_SequenceActivator.WRONGSEQUENCE;
                else interactableUI.Activate(i, inputBuffer[i].NoteCoord);
            }
        }
        if(state == State_SequenceActivator.FULLBUFFER)
        {
            state = State_SequenceActivator.CORRECTSEQUENCE;
            for (int i = 0; i < inputBuffer.Count; i++)
            {
                if (inputBuffer[i].NoteCoord != correctSequence[i]) state = State_SequenceActivator.WRONGSEQUENCE;
                else interactableUI.Activate(i, inputBuffer[i].NoteCoord);
            }
        }
        if(state == State_SequenceActivator.WRONGSEQUENCE)
        {
            inputBuffer = new List<SongData>();
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
                //interactableUI.InputBufferCleared();
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
