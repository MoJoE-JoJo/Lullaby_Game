using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_PrototypeBehaviour { DEACTIVATED, ACTIVATED, FINISHED}

public class PrototypeBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private State_PrototypeBehaviour state = State_PrototypeBehaviour.DEACTIVATED;
    [SerializeField] private bool oneWay;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float moveSpeed;
    private Vector3 target;
    private Vector3 last;
    private float moveFraction = 0f;
    private float positionWaitTimeCounter;
    [SerializeField] private float positionWaitTime = 1f;


    public void Activate()
    {
        state = State_PrototypeBehaviour.ACTIVATED;
    }
    public void Deactivate()
    {
        state = State_PrototypeBehaviour.DEACTIVATED;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionWaitTimeCounter = positionWaitTime;
        startPosition = transform.TransformPoint(startPosition);
        endPosition = transform.TransformPoint(endPosition);
        transform.position = startPosition;
        last = startPosition;
        target = endPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if(state == State_PrototypeBehaviour.ACTIVATED)
        {
            if (oneWay) OneWayMovement();
            else TwoWayMovement();
        }
    }

    private void OneWayMovement()
    {
        if (moveFraction < 1)
        {
            moveFraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, endPosition, moveFraction);
        }
        else if (moveFraction >= 1) state = State_PrototypeBehaviour.FINISHED;
        //transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
    }
    private void TwoWayMovement()
    {
        if (moveFraction < 1)
        {
            moveFraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(last, target, moveFraction);
        }
        else if (moveFraction >= 1)
        {
            if (positionWaitTimeCounter > 0f)
            {
                positionWaitTimeCounter -= Time.deltaTime;
            }
            else if(positionWaitTimeCounter <= 0f)
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
