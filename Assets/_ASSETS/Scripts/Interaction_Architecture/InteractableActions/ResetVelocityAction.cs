using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVelocityAction : InteractableAction
{
    [SerializeField] private Rigidbody2D rb2d;
    private bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        if (!reset)
        {
            rb2d.velocity = new Vector2(0, 0);
            reset = true;
        }
    }

    public override void Deactivate()
    {
        reset = false;
    }

    public override void InputData(SongData data)
    {
        //Do nothing
    }

    public override void Reset()
    {
        //do nothing
    }
}
