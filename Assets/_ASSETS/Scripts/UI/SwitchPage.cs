using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPage : MonoBehaviour
{
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject thisPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch()
    {
        nextPage.SetActive(true);
        thisPage.SetActive(false);
    }
}
