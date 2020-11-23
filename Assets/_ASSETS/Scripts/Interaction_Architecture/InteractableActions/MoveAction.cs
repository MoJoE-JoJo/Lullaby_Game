using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEditor;
using UnityEngine;


public enum State_MoveAction { DEACTIVATED, ACTIVATED, RESET }
public class MoveAction : InteractableAction
{
    [SerializeField] private State_MoveAction state = State_MoveAction.DEACTIVATED;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private bool useVectorX;
    [SerializeField] private bool useVectorY;

    [SerializeField] private bool pressureSensitive;
    [SerializeField] private float speed;

    private SongData _songData;
    private Vector3 _originalPosition;
    private Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position;
        moveVector = moveVector.normalized;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float pSpeed = speed;
        if (pressureSensitive)
        {
            pSpeed = speed * _songData.Volume;
        }
        //Vector2 newPosition = new Vector2(transform.localPosition.x, transform.localPosition.y) + (moveVector * (speed * Time.deltaTime));
        if (state == State_MoveAction.ACTIVATED)
        {
            var moveVec = rb2d.velocity;
            if (useVectorX) moveVec.x = moveVector.x * speed * Time.deltaTime;
            if (useVectorY) moveVec.y = moveVector.y * speed * Time.deltaTime;
            rb2d.velocity = moveVec;
            //transform.localPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        else if (state == State_MoveAction.RESET)
        {
            Reset();
        }
    }
    
    public override void Activate()
    {
        state = State_MoveAction.ACTIVATED;
    }
    public override void Deactivate()
    {
        state = State_MoveAction.DEACTIVATED;
    }
    
    public override void InputData(SongData data)
    {
        _songData = data;
    }

    public override void Reset()
    {
        transform.position = _originalPosition;
        state = State_MoveAction.DEACTIVATED;
    }
    
}
