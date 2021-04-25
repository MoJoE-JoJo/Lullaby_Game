using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackCompletePuzzleAction : InteractableAction
{
    public string puzzle_name;
    public bool isLastPuzzle = false;
    private bool hasActivated = false;
    public override void Activate()
    {
        if (!hasActivated)
        {
            GameManager gm = GameManager.Instance;
            if (gm.puzzleCompletion.puzzle_name == puzzle_name) 
            {
                gm.SubmitPuzzleCompletion();
                hasActivated = true;

                if (isLastPuzzle)
                {
                    gm.SubmitOverallData();
                }
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
