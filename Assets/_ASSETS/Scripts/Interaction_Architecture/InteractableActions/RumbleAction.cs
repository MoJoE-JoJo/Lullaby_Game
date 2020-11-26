using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleAction : InteractableAction
{
    [SerializeField] private float rumbleForce;
    [SerializeField] private float duration;
    private bool _done = false;
    
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
        if (!_done)
        {
            StartCoroutine(Rumble());
        }
        
    }

    public override void Deactivate()
    {
        Gamepad.current.SetMotorSpeeds(0f,0f);
    }

    public override void InputData(SongData data)
    {
    }

    public override void Reset()
    {
    }

    private IEnumerator Rumble()
    {
        Gamepad.current.SetMotorSpeeds(rumbleForce, rumbleForce);
        _done = true;
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
