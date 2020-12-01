using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{

    public void LoadScene(string name)
    {
        if (gameObject.name == "Top") PlayerPrefs.SetString("SpawnPoint", "TopSpawn");
        else if (gameObject.name == "Middle") PlayerPrefs.SetString("SpawnPoint", "MiddleSpawn");
        else if (gameObject.name == "Bottom") PlayerPrefs.SetString("SpawnPoint", "BottomSpawn");
        else if (gameObject.name == "Play Button" || gameObject.name == "Top") PlayerPrefs.SetString("SpawnPoint", "TutorialSpawn");
        SceneManager.LoadScene(name);
    }
}
