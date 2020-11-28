using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbiance : MonoBehaviour
{
    [SerializeField] private GameObject fmodEmitter;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) fmodEmitter.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("inTheDeep", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
