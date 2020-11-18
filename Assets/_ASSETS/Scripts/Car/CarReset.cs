using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CarPlayerPositioning>() != null)
        {
            collision.transform.rotation = collision.GetComponent<MoveToPositionAction>().MovePositions[1].rotation;
            collision.transform.position = collision.GetComponent<MoveToPositionAction>().MovePositions[1].position;
        }
    }
}
