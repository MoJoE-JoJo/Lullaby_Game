using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var name = PlayerPrefs.GetString("SpawnPoint");
        if (name != "")
        {
            transform.position = GameObject.Find(name).transform.position;
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = GameObject.Find(name).transform.position;
            Debug.Log("POPSAOPÅ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
