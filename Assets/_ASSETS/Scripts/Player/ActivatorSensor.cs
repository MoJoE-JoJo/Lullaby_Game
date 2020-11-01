using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ActivatorSensor : MonoBehaviour
{
    public List<Activator> RegisteredActivators 
    {
        get => registeredActivators;
        private set => registeredActivators = value;
    }
    [SerializeField]
    private List<Activator> registeredActivators = new List<Activator>();

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
