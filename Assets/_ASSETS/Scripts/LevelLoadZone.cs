using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadLevels;
    [SerializeField] private List<GameObject> unloadLevels;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject go in loadLevels)
            {
                go.SetActive(true);
            }
            foreach(GameObject go in unloadLevels)
            {
                go.SetActive(false);
            }
        }
    }
}
