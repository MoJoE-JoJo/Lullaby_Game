using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class initLevelLoading: MonoBehaviour
{
    [SerializeField] private List<GameObject> loadLevels;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject go in loadLevels)
            {
                go.SetActive(true);
            }

            this.gameObject.SetActive(false);
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
