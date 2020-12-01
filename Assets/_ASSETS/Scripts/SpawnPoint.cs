using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> initLoadLevelZones;
    private void Awake()
    {
        var name = PlayerPrefs.GetString("SpawnPoint");
        if (name != "")
        {
            switch (name)
            {
                case ("TutorialSpawn"):
                    transform.position = initLoadLevelZones[0].transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = initLoadLevelZones[0].transform.position;
                    break;
                case ("TopSpawn"):
                    transform.position = initLoadLevelZones[1].transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = initLoadLevelZones[1].transform.position;
                    break;
                case ("MiddleSpawn"):
                    transform.position = initLoadLevelZones[2].transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = initLoadLevelZones[2].transform.position;
                    break;
                case ("BottomSpawn"):
                    transform.position = initLoadLevelZones[3].transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = initLoadLevelZones[3].transform.position;
                    break;
            }

            Debug.Log("POPSAOPÅ");
        }
    }
}
