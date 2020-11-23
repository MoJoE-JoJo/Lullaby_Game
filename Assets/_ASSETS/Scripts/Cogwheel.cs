using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Cogwheel : MonoBehaviour
{
    [SerializeField] private List<InteractableAction> _doors;

    private int _currentDoor;
    private bool _finishedPuzzle;
    private Collider2D _currentPosition;

    [SerializeField] private List<GameObject> correctPositions;
    [SerializeField] private List<GameObject> incorrectPositions;

    [SerializeField] private List<Light2D> lights;

    
    [SerializeField] private float timeTakenToReset;

    // Start is called before the first frame update
    void Start()
    {
        _currentDoor = 0;
        _currentPosition = this.transform.Find("CurrentPosition").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WheelCorrectPosition"))
        {
            if (!_finishedPuzzle)
            {
                _doors[_currentDoor].Activate();
                //Debug.Log(lights[_currentDoor].intensity);
                //Debug.Log(_currentDoor);
                int lightToTween = _currentDoor;
                DOTween.To(()=> lights[lightToTween].intensity, x=> lights[lightToTween].intensity = x, 1, 0.3f);
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
                    incorrectPositions[i].SetActive(false);

                    var i1 = i;
                    DOTween.To(()=> lights[i1].intensity, x=> lights[i1].intensity = x, 0, 0.3f);
                }
                _currentDoor = 0;
                correctPositions[_currentDoor].SetActive(true);
                incorrectPositions[_currentDoor].SetActive(true);
                StartCoroutine(ResetCor());
            }
        }
    }

    private IEnumerator ResetCor()
    {
        _currentPosition.enabled = false;
        transform.DORotate(Vector3.zero, timeTakenToReset, RotateMode.Fast);
        yield return new WaitForSeconds(timeTakenToReset);
        _currentPosition.enabled = true;
    }
    
}
