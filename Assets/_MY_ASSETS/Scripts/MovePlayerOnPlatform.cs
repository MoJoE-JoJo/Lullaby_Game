using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnPlatform : MonoBehaviour
{
    private Transform originalParent;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") /*&& collision.transform.position.y > transform.position.y*/)
        {
            originalParent = collision.gameObject.transform.parent;
            collision.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.transform.parent = originalParent;
    }
}
