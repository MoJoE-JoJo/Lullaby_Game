using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SwingAction : InteractableAction
{
    [SerializeField] private GameObject target;
    [SerializeField] private float swingTime;
    [SerializeField] private float speed;
    [SerializeField] private bool once;

    private Coroutine _activateSwing;

    private Transform orgTransform;
    private float timer = 0f;
    private bool active;
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        orgTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (active)
        {
            transform.Rotate(new Vector3(0,0,(Mathf.Cos(timer*2)*speed)));
            
            timer += Time.deltaTime;

            if (speed > 0.035)
            {
                speed -=Time.deltaTime/10f;
            }
            else
            {
                active = false;
                transform.DORotate(Vector3.zero, 3f, RotateMode.Fast);
                done = true;
            }
            

            /*
            if (once)
            {
                transform.position = orgTransform.position;
                transform.rotation = orgTransform.rotation;
                done = true;
                active = false;
            }
            */
            
        }
        
        /*
        if (active)
        {

            if (timer < swingTime || timer > swingTime * 3) transform.RotateAround(target.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);

            
            else transform.RotateAround(target.transform.position, new Vector3(0, 0, -1), speed * Time.deltaTime);

            timer += Time.deltaTime;
           if (timer > swingTime * 4)
            {
                timer = 0.0f;
                if (once) 
                {
                    var trans = GetComponent<Transform>();
                    trans.position = orgTransform.position;
                    trans.rotation = orgTransform.rotation;
                    done = true;
                    active = false;
                }
            }
        }
        */
        //transform.RotateAround(target.transform.position, new Vector3 (0,0,1), 200 * Time.deltaTime);
        //Debug.Log(transform.rotation);
    }

    public override void Activate()
    {
        if (done) return;

        if (this.gameObject.CompareTag("Bell"))
        {
            if (_activateSwing == null)
            {
                _activateSwing = StartCoroutine(ActivateSwing());
            }
        }
        else
        {
            active = true;
        }
    }

    public override void Deactivate()
    {
        active = false;
        /*transform.DORotate(Vector3.zero, 1f, RotateMode.Fast);
        transform.position = orgTransform.position;
        transform.rotation = orgTransform.rotation;
        */
    }

    public override void InputData(SongData data)
    {
    }

    public override void Reset()
    {
        var trans = GetComponent<Transform>();
        trans.position = orgTransform.position;
        trans.rotation = orgTransform.rotation;
    }

    private IEnumerator ActivateSwing()
    {
        yield return new WaitForSeconds(0.35f);
        active = true;
    }
}
