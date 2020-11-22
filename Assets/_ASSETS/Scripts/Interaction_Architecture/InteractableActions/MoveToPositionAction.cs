﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum State_MoveToPositionAction { DEACTIVATED, ACTIVATED, FINISHED }

public class MoveToPositionAction : InteractableAction
{
    public State_MoveToPositionAction State
    {
        get => state;
    }
    public List<Transform> MovePositions
    {
        get => movePositions;
        set => movePositions = value;
    }
    [SerializeField] private State_MoveToPositionAction state = State_MoveToPositionAction.DEACTIVATED;
    [SerializeField] private float moveSpeed;
    [SerializeField, Tooltip("There must always be atleast two elements in the list for the script to behave properly.")] private List<Transform> movePositions;
    [SerializeField, HideInInspector] private bool twoWay;
    [SerializeField, HideInInspector] private bool cycle;
    [SerializeField, HideInInspector] private float positionWaitTime = 1f;
    [SerializeField] private bool resetMoveOnDeactivate = false;
    [SerializeField, Tooltip("Primarily used for the moving car, and os only meant to be used with objects that have rigidbodies")] private float reachedLocationMargin = 0.0f;
    [SerializeField] private bool debug_reset;
    [SerializeField] private bool useLinearMovementOnOneWay = false;
    [SerializeField] private bool disableActivateOnFinish = false;
    [SerializeField] private bool resetSlowyWhenDeactivated = false;
    private Transform target;
    private Transform last;
    private float moveFraction = 0.0f;
    private float positionWaitTimeCounter;
    private int moveToIndex = 1;
    private bool forward = true;
    private Vector3 orgPos;

    // Start is called before the first frame update
    void Start()
    {
        positionWaitTimeCounter = positionWaitTime;
        transform.position = movePositions[0].position;
        last = movePositions[0];
        target = movePositions[moveToIndex];

        orgPos = this.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (debug_reset)
        {
            Reset();
        }

        // quick and dirty, does dont think this works well with more than 2 points of movement
        //if state deactivated and bool true, move back towards starting pos
        if (state == State_MoveToPositionAction.DEACTIVATED && resetSlowyWhenDeactivated)
        {
            transform.position = Vector3.Lerp(this.transform.position, orgPos, Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (state == State_MoveToPositionAction.ACTIVATED)
        {
            if (cycle) CycleMovement();
            else if (twoWay) TwoWayMovement();
            else OneWayMovement();
        }
    }

    override public void Activate()
    {
        if (disableActivateOnFinish && state == State_MoveToPositionAction.FINISHED) return;
        else state = State_MoveToPositionAction.ACTIVATED;
    }
    override public void Deactivate()
    {
        if (resetMoveOnDeactivate) moveFraction = 0;
        state = State_MoveToPositionAction.DEACTIVATED;
    }
    public override void InputData(SongData data)
    {
        //throw new NotImplementedException();
    }

    public override void Reset()
    {
        Start();
        moveFraction = 0.0f;
        moveToIndex = 1;
        forward = true;
        state = State_MoveToPositionAction.DEACTIVATED;
        debug_reset = false;
    }

    private void OneWayMovement()
    {
        last = movePositions[moveToIndex - 1];
        target = movePositions[moveToIndex];
        if (moveFraction < 1)
        {
            moveFraction += Time.fixedDeltaTime * moveSpeed / (last.position - target.position).magnitude;
            if (useLinearMovementOnOneWay)
            {
                transform.position = Vector3.Lerp(last.position, target.position, moveFraction);
            }
            else
            {
                transform.position = new Vector3
                (
                Mathf.SmoothStep(last.position.x, target.position.x, moveFraction),
                Mathf.SmoothStep(last.position.y, target.position.y, moveFraction),
                moveFraction
                );
            }
            
        }
        if (Math.Abs((transform.position - target.position).magnitude) < reachedLocationMargin)
        {
            if (moveToIndex == movePositions.Count - 1)
            {
                state = State_MoveToPositionAction.FINISHED;
            }
            else
            {
                moveToIndex++;
                moveFraction = 0;
            }
        }
        else if (moveFraction >= 1)
        {
            if (moveToIndex == movePositions.Count - 1)
            {
                state = State_MoveToPositionAction.FINISHED;
            }
            else
            {
                moveToIndex++;
                moveFraction = 0;
            }
        }

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
            moveFraction += Time.fixedDeltaTime * moveSpeed / (last.position - target.position).magnitude;
            transform.position = new Vector3
                (
                Mathf.SmoothStep(last.position.x, target.position.x, moveFraction), 
                Mathf.SmoothStep(last.position.y, target.position.y, moveFraction), 
                moveFraction
                );


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
            moveFraction += Time.fixedDeltaTime * moveSpeed / (last.position - target.position).magnitude;
            transform.position = new Vector3
                (
                Mathf.SmoothStep(last.position.x, target.position.x, moveFraction),
                Mathf.SmoothStep(last.position.y, target.position.y, moveFraction),
                moveFraction
                );
        }
        else if (moveFraction >= 1)
        {
            if (positionWaitTimeCounter > 0f)
            {
                positionWaitTimeCounter -= Time.fixedDeltaTime;
            }
            else if (positionWaitTimeCounter <= 0f)
            {
                moveToIndex = (moveToIndex + 1) % (movePositions.Count);
                positionWaitTimeCounter = positionWaitTime;
                moveFraction = 0f;
            }
        }
    }

#if UNITY_EDITOR
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
#endif


}

