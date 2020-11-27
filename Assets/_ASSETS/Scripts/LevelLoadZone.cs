using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoadZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> unloadLevels;
    [SerializeField] private List<GameObject> loadLevels;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*
            foreach (GameObject go in unloadLevels)
            {
                var rg2d = go.GetComponentInChildWithTag<TilemapCollider2D>("Level");
                if (rg2d != null)
                {
                    rg2d.enabled = false;
                }
            }*/
            foreach (GameObject go in unloadLevels)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in loadLevels)
            {
                go.SetActive(true);
            }
            /*
            foreach (GameObject go in loadLevels)
            {
                var rg2d = go.GetComponentInChildWithTag<TilemapCollider2D>("Level");
                if (rg2d != null)
                {
                    rg2d.enabled = true;
                }
            }
            */
        }
    }
}
