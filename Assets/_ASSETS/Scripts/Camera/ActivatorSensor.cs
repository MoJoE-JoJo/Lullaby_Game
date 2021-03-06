using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ActivatorSensor : MonoBehaviour
{
    [SerializeField] private Vector2 screenSize;
    public bool ScreenSizeChanged
    {
        get;
        set;
    }
    //private bool screenSizeChanged = true;
    public List<Activator> RegisteredActivators 
    {
        get => registeredActivators;
        private set => registeredActivators = value;
    }
    [SerializeField] private List<Activator> registeredActivators = new List<Activator>();
    private float collisionTimer = 0.0f;
    private bool collisionPossible = false;

    public void Start()
    {
        //screenSizeChanged = true;
    }
    private void Update()
    {
        SetColliderSizeToScreen();
    }

    /*
    private void FixedUpdate()
    {
        if (collisionTimer < 0.1f)
        {
            collisionTimer += Time.fixedDeltaTime;
            collisionPossible = false;
        }
        if (collisionTimer >= 0.5f)
        {
            collisionTimer = 0.f;
            collisionPossible = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            var activators = collision.GetComponents<Activator>();
            foreach (Activator acti in activators)
            {
                //acti.enabled = true;
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
                //acti.enabled = false;
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

    public void SetColliderSizeToScreen()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        GetComponent<BoxCollider2D>().size = screenSize * 2 / transform.lossyScale;
        ScreenSizeChanged = false;
    }
}
