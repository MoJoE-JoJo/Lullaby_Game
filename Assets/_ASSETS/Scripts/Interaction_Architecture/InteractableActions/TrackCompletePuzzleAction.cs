using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (gm.puzzleCompletion.puzzle_name == puzzle_name) //not sure this check is needed, but doing it anyway
            {
                gm.SubmitPuzzleCompletion();
                hasActivated = true;

                if (isLastPuzzle)
                {
                    gm.SubmitOverallData();
                    // Open After game survey

                    var run_id_form = "entry.1273800886=";
                    var GUID = Telemetry.GUIDToShortString(Telemetry.runID);
                    var url = "https://docs.google.com/forms/d/e/1FAIpQLSfYhX6JvfvEBTGafToqyTS94WI8X7O3Hxwm8wLaHA0G6MQXjg/viewform?" + run_id_form + GUID;

                    Application.OpenURL(url);

                    Application.Quit();
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
