using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstSelectedButton : MonoBehaviour
{
    private EventSystem _eventSystem;

    void Start()
    {
        _eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(SelectFirstButtonLater());
    }

    IEnumerator SelectFirstButtonLater()
    {
        yield return null;
        _eventSystem.SetSelectedGameObject(null);
        _eventSystem.SetSelectedGameObject(GetComponent<Button>().gameObject);
    }
}
