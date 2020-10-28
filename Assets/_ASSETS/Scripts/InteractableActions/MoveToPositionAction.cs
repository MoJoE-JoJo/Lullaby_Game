using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_PrototypeMoveAction { DEACTIVATED, ACTIVATED, FINISHED }

public class MoveToPositionAction : InteractableAction
{
    [SerializeField] private State_PrototypeMoveAction state = State_PrototypeMoveAction.DEACTIVATED;
    [SerializeField] private bool twoWay;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float moveSpeed;
    private Transform target;
    private Transform last;
    private float moveFraction = 0f;
    private float positionWaitTimeCounter;
    [SerializeField] private float positionWaitTime = 1f;


    override public void Activate()
    {
        state = State_PrototypeMoveAction.ACTIVATED;
    }
    override public void Deactivate()
    {
        state = State_PrototypeMoveAction.DEACTIVATED;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionWaitTimeCounter = positionWaitTime;
        transform.position = startPosition.position;
        //startPosition = transform.TransformPoint(startPosition);
        //endPosition = transform.TransformPoint(endPosition);
        last = startPosition;
        target = endPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State_PrototypeMoveAction.ACTIVATED)
        {
            if (twoWay) TwoWayMovement();
            else OneWayMovement();
        }
    }

    private void OneWayMovement()
    {
        if (moveFraction < 1)
        {
            moveFraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, moveFraction);
        }
        else if (moveFraction >= 1) state = State_PrototypeMoveAction.FINISHED;
        //transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
    }
    private void TwoWayMovement()
    {
        if (moveFraction < 1)
        {
            moveFraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(last.position, target.position, moveFraction);
        }
        else if (moveFraction >= 1)
        {
            if (positionWaitTimeCounter > 0f)
            {
                positionWaitTimeCounter -= Time.deltaTime;
            }
            else if (positionWaitTimeCounter <= 0f)
            {
                positionWaitTimeCounter = positionWaitTime;
                moveFraction = 0f;
                var temp = last;
                last = target;
                target = temp;
            }
        }

    }
}
