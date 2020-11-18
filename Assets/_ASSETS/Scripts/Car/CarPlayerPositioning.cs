﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerPositioning : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Rigidbody2D carBody;
    [SerializeField] private PhysicsAction moveAction;
    [SerializeField] private MoveToPositionAction resetAction;
    [SerializeField] private List<RotationAction> wheels;
    [SerializeField] private bool touching;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        carBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(touching && (moveAction.State == State_PhysicsAction.ACTIVATED || resetAction.State == State_MoveToPositionAction.ACTIVATED))
        {
            playerBody.velocity = carBody.velocity;
        }
        if(resetAction.State == State_MoveToPositionAction.FINISHED)
        {
            foreach(RotationAction ro in wheels)
            {
                ro.Deactivate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CarDestination"))
        {
            moveAction.Deactivate();
            resetAction.Deactivate();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CarDestination"))
        {
            moveAction.Deactivate();
            resetAction.Deactivate();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) touching = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) touching = false;
    }
}
