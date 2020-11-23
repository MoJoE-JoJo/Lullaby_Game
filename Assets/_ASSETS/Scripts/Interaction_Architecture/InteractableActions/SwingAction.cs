using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAction : InteractableAction
{
    [SerializeField] private GameObject target;
    [SerializeField] private float swingTime = 2;
    [SerializeField] private float speed = 100;
    [SerializeField] private bool once;

    private Transform orgTransform;
    private float timer;
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
        //transform.RotateAround(target.transform.position, new Vector3 (0,0,1), 200 * Time.deltaTime);
        //Debug.Log(transform.rotation);
    }

    public override void Activate()
    {
        if (done) return;
        active = true;
    }

    public override void Deactivate()
    {
        active = false;
        var trans = GetComponent<Transform>();
        trans.position = orgTransform.position;
        trans.rotation = orgTransform.rotation;
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
}
