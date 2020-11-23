using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State_RotationAction { DEACTIVATED, ACTIVATED }
public enum RotationAction_Direction { COUNTERCLOCKWISE, CLOCKWISE }
public class RotationAction : InteractableAction
{
    [SerializeField]
    private State_RotationAction state = State_RotationAction.DEACTIVATED;
    [SerializeField] private RotationAction_Direction rotateDirection;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField, Tooltip("Usually you want this activated")] private bool fixedSpeed = true;
    [SerializeField] private float SpinDuration = 0.0f;
    [SerializeField, Tooltip("0 means no limit")] private float rotationLimit = 0.0f;
    [SerializeField] private bool spinBackOnDeactivate;
    [SerializeField, Tooltip("Usually you want this at 1")] private float margin = 1f;

    private int direction;
    private bool ongoing;
    private SongData songData;
    private float timer = 0.0f;
    private float originalRotation;
    private Rigidbody2D rigbod;
    private bool usingTimer;
    private bool isLimited = false;
    private float maxRotation;
    // Start is called before the first frame update
    void Start()
    {
        ongoing = false;
        if (rotateDirection == RotationAction_Direction.CLOCKWISE) direction = -1;
        else direction = 1;

        if (SpinDuration > 0.0f) usingTimer = true;

        if (usingTimer) fixedSpeed = true; //dont think i will work well with both pressure control and auto spin for a duration.

        rigbod = GetComponent<Rigidbody2D>();
        originalRotation = rigbod.rotation;
        if (rotationLimit > 0)
        {
            maxRotation = originalRotation + (rotationLimit * direction);
            isLimited = true;
        }
        
        Debug.Log(maxRotation);
    }

    public override void Activate()
    {
        state = State_RotationAction.ACTIVATED;
        timer = 0.0f;
    }

    public override void Deactivate()
    {
        if (usingTimer && timer < SpinDuration) return;
        state = State_RotationAction.DEACTIVATED;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State_RotationAction.ACTIVATED && usingTimer)
        {
            if (timer > SpinDuration) state = State_RotationAction.DEACTIVATED;
            timer += Time.deltaTime;
            ongoing = true;
        }

        else if (state == State_RotationAction.ACTIVATED && !ongoing)
        {
            ongoing = true;
            timer = 0.0f;
        }
        else if (state == State_RotationAction.DEACTIVATED)
        {
            ongoing = false;
        }
    }

    private void FixedUpdate()
    {
        if (ongoing)
        {
            if (isLimited)
            {
                if (rotateDirection == RotationAction_Direction.COUNTERCLOCKWISE && rigbod.rotation >= maxRotation) return;
                else if (rotateDirection == RotationAction_Direction.CLOCKWISE && rigbod.rotation <= maxRotation) return;
            }

            var pressure = songData.Volume;
            if (fixedSpeed) pressure = 1;
            rigbod.MoveRotation(rigbod.rotation + rotationSpeed * Time.fixedDeltaTime * direction * pressure);
        }

        else if (spinBackOnDeactivate)
        {
            if (rigbod.rotation >= originalRotation + margin || rigbod.rotation <= originalRotation - margin)
            {
                var tmpDir = direction * -1;
                rigbod.MoveRotation(rigbod.rotation + rotationSpeed * Time.fixedDeltaTime * tmpDir);
            }
        }
    }

    public override void InputData(SongData data)
    {
        songData = data;
    }

    public override void Reset()
    {
        rigbod.rotation = originalRotation;
    }

    //public void SetRotationLimit(float x) 
    //{
    //    maxRotation = originalRotation + x;
    //}

    //public float GetRotationLimit()
    //{
    //    return rotationLimit;
    //}
}
