using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RegisterActivators : MonoBehaviour
{
    public List<Activator> RegisteredActivators 
    {
        get 
        {
            return registeredActivators;
        }
        private set
        {
            registeredActivators = value;
        }
    }
    [SerializeField]
    private List<Activator> registeredActivators = new List<Activator>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator")) RegisteredActivators.Add(collision.GetComponent<Activator>());   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator")) RegisteredActivators.Remove(collision.GetComponent<Activator>());
    }

    public void SendSong(SongData song)
    {
        foreach (var activator in RegisteredActivators)
        {
            activator.SongInput(song);
        }
    }
}
