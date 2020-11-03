using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private bool thresholdMovement;
    [SerializeField, Tooltip("Only relevant when thresholdMovement")] private float xMoveThreshold;
    [SerializeField, Tooltip("Only relevant when thresholdMovement")] private float yMoveThreshold;
    private GameObject player;
    private Vector3 newPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector2 playerMovement;
    private Vector2 cameraPosition;
    private bool moveCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = Vector2.zero;
        cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (thresholdMovement) MoveWithThreshold();
        else MoveCamera();
        
    }

    private void MoveCamera()
    {
        newPosition = player.transform.position;
        newPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void MoveWithThreshold()
    {
        cameraPosition = transform.position;
        playerMovement = player.transform.position;
        var playerPositionDif = cameraPosition - playerMovement;
        Debug.Log(playerPositionDif);
        if (Math.Abs(playerPositionDif.x) >= xMoveThreshold) moveCamera = true;
        if (Math.Abs(playerPositionDif.y) >= yMoveThreshold) moveCamera = true;
        if (Math.Abs(playerPositionDif.x) <= 0.05 && Math.Abs(playerPositionDif.y) <= 0.05)
        {
            moveCamera = false;
        }
        if (moveCamera) MoveCamera();
    }
}
