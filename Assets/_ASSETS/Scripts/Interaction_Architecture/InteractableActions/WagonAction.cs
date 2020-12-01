using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WagonAction : InteractableAction
{

    private WagonPull _wagonPull;
    private Wagon_Pull _pullDirection;
    private Rigidbody2D _wagonRigidbody2D;
    private BoxCollider2D _wagonCollider;
    [SerializeField] private float pumpForce;
    [SerializeField] private float timeWagonIsMoving;

    [SerializeField]private Ease wagonEase;
    [SerializeField]private WagonSwap swapSoundPlayer;
    //private Mesh _mesh;
    private float _wagonSize;

    private Vector3 _rightDestination;
    private Vector3 _leftDestination;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _wagonPull = GetComponent<WagonPull>();
        _wagonRigidbody2D = _wagonPull.WagonRigidbody2D;
        //_mesh = GetComponent<Mesh>();
        _wagonCollider = GetComponent<BoxCollider2D>();
        _wagonSize = _wagonCollider.bounds.size.x;
        //Debug.Log("Wagon Size: " + _wagonSize);
        _rightDestination = this.transform.parent.Find("RightWagonDestination").position- Vector3.right*(_wagonSize/2);
        _leftDestination = this.transform.parent.Find("LeftWagonDestination").position + Vector3.right*(_wagonSize/2);
        //Debug.Log("Left destination: " + _leftDestination);
        //Debug.Log("Right destination: " + _rightDestination);
        
    }

    // Update is called once per frame
    void Update()
    {
        _pullDirection = _wagonPull.PullDirection;
    }

    public override void Activate()
    {
        Vector3 finalLocation;
        float timeWagon;
        
        switch (_pullDirection)
        {
            case Wagon_Pull.RIGHT:
                finalLocation = transform.position + Vector3.left * pumpForce;
                timeWagon = timeWagonIsMoving;
                
                //Debug.Log("Final location: " + finalLocation);

                if (finalLocation.x < _leftDestination.x)
                {
                    //play swap
                    timeWagon = Mathf.Abs((_leftDestination.x - transform.position.x) / pumpForce)*timeWagonIsMoving;
                    finalLocation = _leftDestination;
                    swapSoundPlayer.playSound();
                    //Debug.Log("Fixed final location: " + finalLocation);
                }

                
                
                transform.DOMove(finalLocation, timeWagon, false)
                    .SetEase(wagonEase);
                //Debug.Log("Activate");
                //transform.position += Vector3.left * (pumpForce * Time.deltaTime);
                //AddForce();
                //_wagonRigidbody2D.AddForce(Vector2.left*pumpForce, ForceMode2D.Impulse );
                break;
            case Wagon_Pull.LEFT:
                //Debug.Log("Activate");
                finalLocation = transform.position + Vector3.right * pumpForce;
                timeWagon = timeWagonIsMoving;

                //Debug.Log("Final location: " + finalLocation);
                if (finalLocation.x > _rightDestination.x)
                {
                    timeWagon = Mathf.Abs((_rightDestination.x - transform.position.x) / pumpForce)*timeWagonIsMoving;
                    finalLocation = _rightDestination;
                    swapSoundPlayer.playSound();
                    //new Vector3(_rightDestination.x +0.000001f, _rightDestination.y, _rightDestination.z);
                    //Debug.Log("Fixed final location: " + finalLocation);
                }
                
                transform.DOMove(finalLocation, timeWagon, false)
                    .SetEase(wagonEase);
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
