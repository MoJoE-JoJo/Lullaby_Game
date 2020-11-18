using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State_PhysicsAction { DEACTIVATED, ACTIVATED, IDLE, ACTIVATING }
public enum Activated_Gravity_Direction { UP, DOWN, LEFT, RIGHT }
public class PhysicsAction : InteractableAction
{
    [SerializeField] private State_PhysicsAction state = State_PhysicsAction.DEACTIVATED;
    [SerializeField] private Activated_Gravity_Direction flyDirection;
    [SerializeField, Tooltip("9.8 corresponds to gravity speed")] private float flySpeed = 9.8f;
    [SerializeField, Tooltip("Adjusts the scale of gravity, 1 is for normal gravity, and 0 is for no gravity")] private float gravityScale = 0.0f;
    [SerializeField] private bool debug_reset;
    private float originalGravityScale;
    private Vector2 directionVector;
    //private bool ongoing;
    private SongData songData;
    private Vector3 orgPos;
    private Quaternion orgRot;
    // Start is called before the first frame update
    void Start()
    {
        originalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
        //ongoing = false;
        orgPos = transform.position;
        orgRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (debug_reset)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        var rigbod = GetComponent<Rigidbody2D>();
        if (state == State_PhysicsAction.ACTIVATING)
        {
            ChangeGravity(rigbod, flyDirection);
            //ongoing = true;
            state = State_PhysicsAction.ACTIVATED;
        }
        else if (state == State_PhysicsAction.ACTIVATED)
        {
            rigbod.gravityScale = gravityScale;
            rigbod.AddForce(directionVector * flySpeed * songData.Volume);
        }
        else if (state == State_PhysicsAction.DEACTIVATED)
        {
            rigbod.gravityScale = originalGravityScale;
            //ongoing = false;
            state = State_PhysicsAction.IDLE;
        }
    }

    public override void Activate()
    {
        if (state != State_PhysicsAction.ACTIVATED) state = State_PhysicsAction.ACTIVATING;
    }

    public override void Deactivate()
    {
        state = State_PhysicsAction.DEACTIVATED;
    }

    public override void InputData(SongData data)
    {
        songData = data;
    }

    public override void Reset()
    {
        //ongoing = false;
        transform.position = orgPos;
        transform.rotation = orgRot;
        debug_reset = false;
    }

    private void ChangeGravity(Rigidbody2D rigbod, Activated_Gravity_Direction direction)
    {
        switch (direction)
        {
            case Activated_Gravity_Direction.UP:
                directionVector = Vector2.up;
                break;
            case Activated_Gravity_Direction.DOWN:
                directionVector = Vector2.down;
                break;
            case Activated_Gravity_Direction.LEFT:
                directionVector = Vector2.left;
                break;
            case Activated_Gravity_Direction.RIGHT:
                directionVector = Vector2.right;
                break;
        }
        rigbod.gravityScale = gravityScale;
    }
}
