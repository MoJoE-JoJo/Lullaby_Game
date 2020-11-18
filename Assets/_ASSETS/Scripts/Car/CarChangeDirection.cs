using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChangeDirection : MonoBehaviour
{
    [SerializeField] private Activated_Gravity_Direction direction = Activated_Gravity_Direction.LEFT;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CarPlayerPositioning>() != null)
        {
            collision.GetComponent<PhysicsAction>().FlyDirection = direction;
        }
    }
}
