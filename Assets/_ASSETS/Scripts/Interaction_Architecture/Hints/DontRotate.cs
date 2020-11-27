using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour
{
    private Transform transform;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = this.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().rotation = originalRotation;

    }
}
