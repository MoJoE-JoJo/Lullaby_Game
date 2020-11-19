using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChangeSpeed : MonoBehaviour
{
    [SerializeField] private float newSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CarPlayerPositioning>() != null)
        {
            collision.GetComponent<PhysicsAction>().FlySpeed = newSpeed;
        }
    }
}
