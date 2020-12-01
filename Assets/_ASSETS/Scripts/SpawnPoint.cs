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
            var pos = new Vector3();
            switch (name)
            {
                case ("TutorialSpawn"):
                    transform.position = initLoadLevelZones[0].transform.position;
                    pos = initLoadLevelZones[0].transform.position;
                    pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                    break;
                case ("TopSpawn"):
                    transform.position = initLoadLevelZones[1].transform.position;
                    pos = initLoadLevelZones[1].transform.position;
                    pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                    break;
                case ("MiddleSpawn"):
                    transform.position = initLoadLevelZones[2].transform.position;
                    pos = initLoadLevelZones[2].transform.position;
                    pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                    break;
                case ("BottomSpawn"):
                    transform.position = initLoadLevelZones[3].transform.position;
                    pos = initLoadLevelZones[3].transform.position;
                    pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                    break;
            }

            Debug.Log("POPSAOPÅ");
        }
    }
}
