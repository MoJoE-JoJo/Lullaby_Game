using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_PressurePlateActivator { ACTIVATED, DEACTIVATED }

public class PressurePlateActivator : Activator
{
    [SerializeField] private State_PressurePlateActivator state = State_PressurePlateActivator.DEACTIVATED;
    //[SerializeField] private BoxCollider2D plate;
    [SerializeField] private Color activatedColor;
    [SerializeField] private Color deactivatedColor;
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        //plate = gameObject.GetComponent<BoxCollider2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State_PressurePlateActivator.ACTIVATED)
        {
            sprite.color = activatedColor;
            foreach (InteractableAction action in actions)
            {
                action.Activate();
                //action.InputData(data);
            }
        }

        if(state == State_PressurePlateActivator.DEACTIVATED)
        {
            sprite.color = deactivatedColor;
            foreach (InteractableAction action in actions)
            {
                action.Deactivate();
                //action.InputData(data);
            }
        }
    }

    public override void ShowHint()
    {
        //doesn't use this one, not needed to be implemented
    }

    public override void SongInput(SongData data)
    {
        //doesn't use this one, not needed to be implemented
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Interactable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = State_PressurePlateActivator.ACTIVATED;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = State_PressurePlateActivator.DEACTIVATED;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = State_PressurePlateActivator.ACTIVATED;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = State_PressurePlateActivator.DEACTIVATED;
        }
    }

}
