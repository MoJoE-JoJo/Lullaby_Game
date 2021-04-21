﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<InteractableAction> actions;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.transform.position = spawnPoint.position;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            foreach (var action in actions)
            {
                action.Reset();
            }
            //track death 
            GameManager.Instance.SubmitPlayerDeath();
        }
    }

    private void Update()
    {
        
    }
}
