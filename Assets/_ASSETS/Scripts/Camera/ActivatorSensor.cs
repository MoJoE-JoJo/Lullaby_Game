﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ActivatorSensor : MonoBehaviour
{
    public Vector2 screenSize;
    public Collider2D boxCollider;

    public void Start()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        boxCollider = GetComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().size = screenSize * 2 / transform.lossyScale;
    }
    public List<Activator> RegisteredActivators 
    {
        get => registeredActivators;
        private set => registeredActivators = value;
    }
    [SerializeField]
    private List<Activator> registeredActivators = new List<Activator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            var activators = collision.GetComponents<Activator>();
            foreach (Activator acti in activators)
            {
                RegisteredActivators.Add(acti);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            var activators = collision.GetComponents<Activator>();
            foreach (Activator acti in activators)
            {
                RegisteredActivators.Remove(acti);
            }
        }
    }

    public void SendSong(SongData song)
    {
        foreach (var activator in RegisteredActivators)
        {
            activator.SongInput(song);
        }
    }
}
