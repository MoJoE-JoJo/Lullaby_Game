using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WagonAction : InteractableAction
{

    private WagonPull _wagonPull;
    private Wagon_Pull _pullDirection;
    private Rigidbody2D _wagonRigidbody2D;
    [SerializeField] private float pumpForce;
    [SerializeField] private float timeWagonIsMoving;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _wagonPull = GetComponent<WagonPull>();
        _wagonRigidbody2D = _wagonPull.WagonRigidbody2D;
        Debug.Log(_wagonRigidbody2D);
    }

    // Update is called once per frame
    void Update()
    {
        _pullDirection = _wagonPull.PullDirection;
    }

    public override void Activate()
    {
        switch (_pullDirection)
        {
            case Wagon_Pull.RIGHT:

                transform.DOMove(transform.position + Vector3.left * (pumpForce), timeWagonIsMoving, false)
                    .SetEase(Ease.InOutQuint);
                //Debug.Log("Activate");
                //transform.position += Vector3.left * (pumpForce * Time.deltaTime);
                //AddForce();
                //_wagonRigidbody2D.AddForce(Vector2.left*pumpForce, ForceMode2D.Impulse );
                break;
            case Wagon_Pull.LEFT:
                //Debug.Log("Activate");
                transform.DOMove(transform.position + Vector3.right * (pumpForce), timeWagonIsMoving, false)
                    .SetEase(Ease.InOutQuint);
                //transform.position += Vector3.right * (pumpForce * Time.deltaTime);

                //AddForce();
                //_wagonRigidbody2D.AddForce(Vector2.right*pumpForce, ForceMode2D.Impulse );
                break;
        }
        //Debug.Log("Activated");
    }

    public override void Deactivate()
    {
        //throw new System.NotImplementedException();
    }

    public override void InputData(SongData data)
    {
        
    }

    public override void Reset()
    {
        //throw new System.NotImplementedException();
    }
    
}
