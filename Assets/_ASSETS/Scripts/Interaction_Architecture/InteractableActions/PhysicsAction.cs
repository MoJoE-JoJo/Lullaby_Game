using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  enum State_PhysicsAction {DEACTIVATED, ACTIVATED}
public enum Activated_Gravity_Direction { UP, LEFT, RIGHT }
public class PhysicsAction : InteractableAction
{
    [SerializeField] private State_PhysicsAction state = State_PhysicsAction.DEACTIVATED;
    [SerializeField] private Activated_Gravity_Direction flyDirection;
    [SerializeField, Tooltip("9.8 corresponds to gravity speed")] private float flySpeed = 9.8f;
    [SerializeField] private bool debug_reset;
    private float originalGravityScale;
    private Vector2 directionVector;
    private bool ongoing;
    private SongData songData;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
        ongoing = false;
        originalPosition = transform.position;
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
        if (state == State_PhysicsAction.ACTIVATED && !ongoing)
        {
            ChangeGravity(rigbod, flyDirection);
            ongoing = true;
        }
        else if (state == State_PhysicsAction.DEACTIVATED)
        {
            rigbod.gravityScale = originalGravityScale;
            ongoing = false;
        }
        if (ongoing) rigbod.AddForce(directionVector * flySpeed * songData.Volume);
    }

    public override void Activate()
    {
        state = State_PhysicsAction.ACTIVATED;
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
        ongoing = false;
        transform.position = originalPosition;
        debug_reset = false;
    }

    private void ChangeGravity(Rigidbody2D rigbod, Activated_Gravity_Direction direction) 
    {
        switch (direction)
        {
            case Activated_Gravity_Direction.UP:
                directionVector = Vector2.up;
                break;
            case Activated_Gravity_Direction.LEFT:
                directionVector = Vector2.left;
                break;
            case Activated_Gravity_Direction.RIGHT:
                directionVector = Vector2.right;
                break;
        }
        rigbod.gravityScale = 0;
    }
}
