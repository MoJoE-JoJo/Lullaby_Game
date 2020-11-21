using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wagon_Pull{RIGHT, LEFT}

public class WagonPull : MonoBehaviour
{
    
    private Rigidbody2D _wagonRigidbody2D;
    [SerializeField] private Wagon_Pull pullDirection;
    [SerializeField] private float pullForce;
    [SerializeField] private bool _isPulling = true;

    public Wagon_Pull PullDirection
    {
        get => pullDirection;
        set => pullDirection = value;
    }

    public Rigidbody2D WagonRigidbody2D
    {
        get => _wagonRigidbody2D;
        set => _wagonRigidbody2D = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _wagonRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //_wagonRigidbody2D.AddForce(Vector2.right*pullForce);
        
        

        //Debug.Log(pullDirection);
    }

    private void FixedUpdate()
    {
        if (_isPulling)
        {
            switch (pullDirection)
            {
                case Wagon_Pull.RIGHT:
                    transform.position += Vector3.right * (pullForce * Time.deltaTime);
                    //_wagonRigidbody2D.AddForce(Vector2.right*pullForce);
                    //_wagonRigidbody2D.MovePosition(_wagonRigidbody2D.position + Vector2.right * (pullForce * Time.fixedDeltaTime));
                    //_wagonRigidbody2D.velocity =Vector2.right*pullForce;
                    break;
                case Wagon_Pull.LEFT:
                    transform.position += Vector3.left * (pullForce * Time.deltaTime);
                    //_wagonRigidbody2D.AddForce(Vector2.left*pullForce);
                    //_wagonRigidbody2D.MovePosition(_wagonRigidbody2D.position + Vector2.left * (pullForce * Time.fixedDeltaTime));
                    //_wagonRigidbody2D.velocity =Vector2.left*pullForce;
                    break;
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftWagonDestination"))
        {
            _isPulling = false;
            pullDirection = Wagon_Pull.LEFT;
        }
        else if (other.CompareTag("RightWagonDestination"))
        {
            _isPulling = false;
            pullDirection = Wagon_Pull.RIGHT;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("RightWagonDestination") || other.CompareTag("LeftWagonDestination"))
        {
            _isPulling = true;
        }
    }

    private void StopWagon()
    {
        _wagonRigidbody2D.velocity = Vector2.zero;
    }
    
}
