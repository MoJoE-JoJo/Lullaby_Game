using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Cogwheel : MonoBehaviour
{
    [SerializeField] private List<InteractableAction> _doors;
    [SerializeField] private TrackCompletePuzzleAction puzzleCompleteAction;
    private int _currentDoor;
    private bool _finishedPuzzle;
    private Collider2D _currentPosition;

    [SerializeField] private List<GameObject> correctPositions;
    [SerializeField] private List<GameObject> incorrectPositions;

    [SerializeField] private List<Light2D> lights;

    
    [SerializeField] private float timeTakenToReset;
    [SerializeField] private SoundAction currectPositionSound;

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

    private int lockState = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WheelCorrectPosition"))
        {
            currectPositionSound.Activate();
            if (!_finishedPuzzle)
            {
                _doors[_currentDoor].Activate();
                int lightToTween = _currentDoor;
                DOTween.To(()=> lights[lightToTween].intensity, x=> lights[lightToTween].intensity = x, 1, 0.3f);
                correctPositions[_currentDoor].SetActive(false);
                incorrectPositions[_currentDoor].SetActive(false);
                
                _currentDoor++;

                GameManager.Instance.SubmitLockPuzzleStat(lockState, true);
                lockState++;

                if (_currentDoor >= _doors.Count)
                {
                    _finishedPuzzle = true;
                    puzzleCompleteAction.Activate();
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

                GameManager.Instance.SubmitLockPuzzleStat(lockState, false);
                lockState = 0;

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
