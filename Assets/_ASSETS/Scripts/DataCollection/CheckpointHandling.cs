using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandling : MonoBehaviour
{
    public enum CheckpointsEnum
    {
        TUTORIAL, 
    }

    public CheckpointsEnum checkpoint;
    public bool isStart = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // if the start of collecting, just 
            if (isStart)
            {
                GameManager.Instance.levelData.starttime = DateTime.Now.TimeOfDay;
                GameManager.Instance.levelData.checkpoint = checkpoint.ToString();
            }
            else // end of collecting
            {
                if (GameManager.Instance.levelData.checkpoint == checkpoint.ToString()) // ensure start and end line up
                {
                    GameManager.Instance.levelData.endtime = DateTime.Now.TimeOfDay;
                    GameManager.Instance.levelData.test = "Hello there";
                    GameManager.Instance.SubmitLevelData();
                }
            }
        }
    }
}
