using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEditor;
using UnityEngine;


public enum State_MoveAction { DEACTIVATED, ACTIVATED, RESET }
public class MoveAction : InteractableAction
{
    [SerializeField] private State_MoveAction state = State_MoveAction.DEACTIVATED;
    public Vector2 moveVector;

    public bool pressureSensitive;
    [SerializeField]private float maxSpeed;
    [SerializeField]private float constantSpeed;

    private SongData _songData;
    
    private Vector3 _originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position;
        moveVector = moveVector.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if (pressureSensitive)
        {
            speed = maxSpeed * _songData.Volume;
        }
        else
        {
            speed = constantSpeed;
        }
        Vector2 newPosition = new Vector2(transform.localPosition.x, transform.localPosition.y) + (moveVector * (speed * Time.deltaTime));
        if (state == State_MoveAction.ACTIVATED)
        {
            transform.localPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);
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
