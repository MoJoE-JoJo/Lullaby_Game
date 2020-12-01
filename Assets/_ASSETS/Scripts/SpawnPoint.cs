using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> initLoadLevelZones;
    [SerializeField] private Light2D global;
    private void Awake()
    {
        var name = PlayerPrefs.GetString("SpawnPoint");
        if (name != "")
        {
            if(initLoadLevelZones.Count > 0)
            {
                var pos = new Vector3();
                switch (name)
                {
                    case ("TutorialSpawn"):
                        global.intensity = 0.8f;
                        transform.position = initLoadLevelZones[0].transform.position;
                        pos = initLoadLevelZones[0].transform.position;
                        pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                        GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                        break;
                    case ("TopSpawn"):
                        global.intensity = 0.8f;
                        transform.position = initLoadLevelZones[1].transform.position;
                        pos = initLoadLevelZones[1].transform.position;
                        pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                        GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                        break;
                    case ("MiddleSpawn"):
                        global.intensity = 0.5f;
                        transform.position = initLoadLevelZones[2].transform.position;
                        pos = initLoadLevelZones[2].transform.position;
                        pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                        GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                        var ele1 = GameObject.Find("Elevator1");
                        ele1.transform.position = GameObject.Find("Elevator2").transform.position;
                        break;
                    case ("BottomSpawn"):
                        global.intensity = 0.2f;
                        transform.position = initLoadLevelZones[3].transform.position;
                        pos = initLoadLevelZones[3].transform.position;
                        pos.z = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
                        GameObject.FindGameObjectWithTag("MainCamera").transform.position = pos;
                        var ele2 = GameObject.Find("Elevator1");
                        var posBot = GameObject.Find("level 3").transform.position;
                        posBot.y += 0.15f;
                        ele2.transform.position = posBot;
                        break;
                }
            }
        }
    }
}
