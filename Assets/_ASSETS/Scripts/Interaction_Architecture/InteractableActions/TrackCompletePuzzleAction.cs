using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCompletePuzzleAction : InteractableAction
{
    public string puzzle_name;
    private bool hasActivated = false;
    public override void Activate()
    {
        GameManager gm = GameManager.Instance;
        if (!hasActivated)
        {
            if (gm.puzzleCompletion.puzzle_name == puzzle_name) //not sure this check is needed, but doing it anyway
            {
                gm.SubmitPuzzleCompletion();
                hasActivated = true;
            }
        }
    }

    public override void Deactivate()
    {
        return;
    }

    public override void InputData(SongData data)
    {
        return;
    }

    public override void Reset()
    {
        return;
    }

}
