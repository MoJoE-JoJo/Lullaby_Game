using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cogwheel : MonoBehaviour
{
    [SerializeField] private List<InteractableAction> _doors;

    private int _currentDoor;
    private bool _finishedPuzzle;

    [SerializeField] private List<GameObject> correctPositions;
    [SerializeField] private List<GameObject> incorrectPositions;

    // Start is called before the first frame update
    void Start()
    {
        _currentDoor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reset()
    {
        transform.DORotate(Vector3.zero, 1f, RotateMode.Fast);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WheelCorrectPosition"))
        {
            if (!_finishedPuzzle)
            {
                _doors[_currentDoor].Activate();
                correctPositions[_currentDoor].SetActive(false);
                incorrectPositions[_currentDoor].SetActive(false);
                
                _currentDoor++;

                if (_currentDoor >= _doors.Count)
                {
                    _finishedPuzzle = true;
                }
                else
                {
                    correctPositions[_currentDoor].SetActive(true);
                    incorrectPositions[_currentDoor].SetActive(true);
                }
            }
        }

        if (other.CompareTag("WheelIncorrectPosition"))
        {
            if (!_finishedPuzzle)
            {
                for (int i = 0; i < _doors.Count; i++)
                {
                    _doors[i].Reset();
                    correctPositions[i].SetActive(false);
                    incorrectPositions[i].SetActive(false);
                }
                _currentDoor = 0;
                correctPositions[_currentDoor].SetActive(true);
                incorrectPositions[_currentDoor].SetActive(true);
                Reset();
            }
        }
    }
}
