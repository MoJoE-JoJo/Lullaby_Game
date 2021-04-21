using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum Wagon_Pull{RIGHT, LEFT}

public class WagonPull : MonoBehaviour
{
    
    private Rigidbody2D _wagonRigidbody2D;
    [SerializeField] private Wagon_Pull pullDirection;
    [SerializeField] private float pullForce;
    [SerializeField] private bool _isPulling = true;
    [SerializeField] private string puzzle_name = "";
    private Light2D _lightRight;
    private Light2D _lightLeft;

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
        GameObject lights = this.transform.Find("Lights").gameObject;
        _lightRight = lights.transform.Find("LightRight").GetComponent<Light2D>();
        _lightLeft = lights.transform.Find("LightLeft").GetComponent<Light2D>();
        
        _wagonRigidbody2D = GetComponent<Rigidbody2D>();
        
        if(pullDirection == Wagon_Pull.RIGHT)
        {
            _lightRight.intensity = 0;
            _lightLeft.intensity = 4;
        }
        else
        {
            _lightRight.intensity = 4;
            _lightLeft.intensity = 0;
        }
        prevPos = transform.position;
    }

    private Vector2 prevPos;
    private void Update()
    {
        if (_isPulling)
        {
            GameManager gm = GameManager.Instance;
            switch (pullDirection)
            {
                case Wagon_Pull.RIGHT:
                    transform.position += Vector3.right * (pullForce * Time.deltaTime);

                    if (prevPos.x > transform.position.x && gm.puzzleCompletion.puzzle_name == puzzle_name) gm.puzzleCompletion.wagon_movement += Mathf.Abs(prevPos.x - transform.position.x);

                    break;
                case Wagon_Pull.LEFT:
                    transform.position += Vector3.left * (pullForce * Time.deltaTime);
                    if (prevPos.x < transform.position.x && gm.puzzleCompletion.puzzle_name == puzzle_name) gm.puzzleCompletion.wagon_movement += Mathf.Abs(prevPos.x - transform.position.x);
                    
                    break;
            }    
            prevPos = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftWagonDestination"))
        {
            _isPulling = false;
            pullDirection = Wagon_Pull.LEFT;
            DOTween.To(()=> _lightRight.intensity, x=> _lightRight.intensity = x, 4, 0.3f);
            DOTween.To(()=> _lightLeft.intensity, x=> _lightLeft.intensity = x, 0, 0.3f);
            //_lightRight.intensity = 1;
            //_lightLeft.intensity = 0;
        }
        else if (other.CompareTag("RightWagonDestination"))
        {
            _isPulling = false;
            pullDirection = Wagon_Pull.RIGHT;
            DOTween.To(()=> _lightRight.intensity, x=> _lightRight.intensity = x, 0, 0.3f);
            DOTween.To(()=> _lightLeft.intensity, x=> _lightLeft.intensity = x, 4, 0.3f);

            //_lightRight.intensity = 0;
            //_lightLeft.intensity = 1;
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
