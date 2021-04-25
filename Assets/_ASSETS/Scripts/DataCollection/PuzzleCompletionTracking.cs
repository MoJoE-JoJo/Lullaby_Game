using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletionTracking : MonoBehaviour
{
    public string puzzle_name = "";
    public bool isStart = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameManager.Instance;
        if (collision.CompareTag("Player"))
        {
            if (!collision.isTrigger) return;

            if (isStart && gm.puzzleCompletion.puzzle_name != puzzle_name)
            {
                gm.puzzleCompletion.startTime = gm.total_run_time;
                gm.puzzleCompletion.puzzle_name = puzzle_name;

                // reset the other values: other scripts will update these values.
                gm.puzzleCompletion.endTime = 0;
                gm.puzzleCompletion.note_lock_count = 0;
                gm.puzzleCompletion.note_unlock_count = 0;
                gm.puzzleCompletion.time_singing = 0;
                gm.puzzleCompletion.wagon_movement = 0;
            }
            else 
            {
                if (gm.puzzleCompletion.endTime == 0 && gm.puzzleCompletion.puzzle_name == puzzle_name)
                {
                    gm.SubmitPuzzleCompletion();
                }
            }
        }

    }
}
