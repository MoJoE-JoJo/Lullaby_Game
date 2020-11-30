using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class initLevelLoading: MonoBehaviour
{

    [SerializeField] private List<GameObject> unloadLevels;
    [SerializeField] private List<GameObject> loadLevels;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            foreach (GameObject go in unloadLevels)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in loadLevels)
            {
                go.SetActive(true);
            }

            this.transform.parent.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
