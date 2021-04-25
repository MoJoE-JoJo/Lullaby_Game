using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger) return;
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == GameManager.Instance.playerMovement.checkpoint) return;
            GameManager.Instance.playerMovement.timestamp = TimeSpan.Zero.Add(TimeSpan.FromSeconds(GameManager.Instance.total_run_time));
            GameManager.Instance.playerMovement.checkpoint = gameObject.name;
            GameManager.Instance.SubmitPlayerMovement();
        }
    }
}
