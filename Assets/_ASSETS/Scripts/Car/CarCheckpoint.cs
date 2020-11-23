using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CarPlayerPositioning>() != null)
        {
            collision.GetComponent<MoveToPositionAction>().MovePositions[1] = transform;
        }
    }
}
