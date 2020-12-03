using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Moving Part"))
        {
            go.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
