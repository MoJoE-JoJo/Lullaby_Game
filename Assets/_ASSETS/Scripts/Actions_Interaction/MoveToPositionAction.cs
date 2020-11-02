using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum State_PrototypeMoveAction { DEACTIVATED, ACTIVATED, FINISHED }

public class MoveToPositionAction : InteractableAction
{
    [SerializeField] private State_PrototypeMoveAction state = State_PrototypeMoveAction.DEACTIVATED;
    private Transform target;
    private Transform last;
    private float moveFraction = 0f;
    private float positionWaitTimeCounter;
    private int moveToIndex = 1;
    private bool forward = true;
    [SerializeField] private float moveSpeed;
    [SerializeField, Tooltip("There must always be atleast two elements in the list for the script to behave properly.")] private List<Transform> movePositions;
    [SerializeField, HideInInspector] private bool twoWay;
    [SerializeField, HideInInspector] private bool cycle;
    [SerializeField, HideInInspector] private float positionWaitTime = 1f;


    override public void Activate()
    {
        state = State_PrototypeMoveAction.ACTIVATED;
    }
    override public void Deactivate()
    {
        state = State_PrototypeMoveAction.DEACTIVATED;
    }
    public override void InputData(SongData data)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        positionWaitTimeCounter = positionWaitTime;
        transform.position = movePositions[0].position;
        //startPosition = transform.TransformPoint(startPosition);
        //endPosition = transform.TransformPoint(endPosition);
        last = movePositions[0];
        target = movePositions[moveToIndex];

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State_PrototypeMoveAction.ACTIVATED)
        {
            if (cycle) CycleMovement();
            else if (twoWay) TwoWayMovement();
            else OneWayMovement();
        }
    }

    private void OneWayMovement()
    {
        if (moveFraction < 1)
        {
            moveFraction += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(movePositions[moveToIndex-1].position, movePositions[moveToIndex].position, moveFraction);
        }
        else if (moveFraction >= 1) state = State_PrototypeMoveAction.FINISHED;
        //transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
    }
    private void TwoWayMovement()
    {
        if (moveFraction < 1)
        {
            if (forward) 
            {
                last = movePositions[moveToIndex - 1];
                target = movePositions[moveToIndex];
            }
            if (!forward)
            {
                last = movePositions[moveToIndex + 1];
                target = movePositions[moveToIndex];
            }
            moveFraction += Time.deltaTime * moveSpeed / (last.position - target.position).magnitude;
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
                if (forward)
                {
                    if (moveToIndex == movePositions.Count - 1)
                    {
                        forward = false;
                        moveToIndex--;
                    }
                    else moveToIndex++;
                }
                else if (!forward)
                {
                    if (moveToIndex == 0)
                    {
                        forward = true;
                        moveToIndex++;
                    }
                    else moveToIndex--;
                }
                positionWaitTimeCounter = positionWaitTime;
                moveFraction = 0f;
            }
        }
    }

    private void CycleMovement()
    {
        if (moveFraction < 1)
        {
            int lastIndex = moveToIndex-1;
            if (moveToIndex == 0) lastIndex = movePositions.Count - 1; 
            last = movePositions[lastIndex];
            target = movePositions[moveToIndex];
            moveFraction += Time.deltaTime * moveSpeed / (last.position - target.position).magnitude;
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
                moveToIndex = (moveToIndex + 1) % (movePositions.Count);
                positionWaitTimeCounter = positionWaitTime;
                moveFraction = 0f;
            }
        }
    }

    [CustomEditor(typeof(MoveToPositionAction))]
    public class MyScriptEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var action = target as MoveToPositionAction;

            if (action.movePositions.Count == 2 && !action.twoWay)
            {
                action.twoWay = EditorGUILayout.Toggle(new GUIContent("Two Way"), action.twoWay, new GUILayoutOption[0]);
            }

            else if (action.movePositions.Count > 2 && !action.twoWay && !action.cycle)
            {
                action.twoWay = EditorGUILayout.Toggle(new GUIContent("Two Way"), action.twoWay, new GUILayoutOption[0]);
                action.cycle = EditorGUILayout.Toggle(new GUIContent("Cycle", "The object will move in the following pattern: 0->1->2->...->n->0. And the object will repeat this pattern."), action.cycle, new GUILayoutOption[0]);
            }

            else if (action.twoWay && !action.cycle)
            {
                action.twoWay = EditorGUILayout.Toggle(new GUIContent("Two Way"), action.twoWay, new GUILayoutOption[0]);
                action.positionWaitTime = EditorGUILayout.FloatField("Position Wait Time", action.positionWaitTime);
            }

            else if (action.movePositions.Count > 2 && !action.twoWay && action.cycle)
            {
                action.positionWaitTime = EditorGUILayout.FloatField("Position Wait Time", action.positionWaitTime);
                action.cycle = EditorGUILayout.Toggle(new GUIContent("Cycle", "The object will move in the following pattern: 0->1->2->...->n->0. And the object will repeat this pattern."), action.cycle, new GUILayoutOption[0]);
            }

        }
    }


}

