using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalDoorStep : MonoBehaviour
{
    [SerializeField] private GameObject currentHint;
    [SerializeField] private GameObject nextStep;
    [SerializeField] private GameObject nextHint;
    //[SerializeField] private MoveToPositionAction currentStepAction;
    [SerializeField] private GameObject doorPart;
    [SerializeField] private int doorStepNumber = 1;
    public int doorStepIndex = 0;
    public List<MoveToPositionAction> actions;
    [SerializeField] private CameraShakeAction shakeSmall;
    [SerializeField] private CameraShakeAction shakeBig;
    [SerializeField] private bool lastStep;
    [SerializeField] private List<GameObject> lights;
    [SerializeField] private GameObject lightCircle;
    [SerializeField] private GameObject invisibleWallOnDoor;
    [SerializeField] private RumbleAction smallRumble;
    [SerializeField] private RumbleAction bigRumble;



    private void Start()
    {
        //smallRumble.Deactivate();
        doorStepIndex = doorStepNumber - 1;
        actions = doorPart.GetComponents<MoveToPositionAction>().ToList();
    }

    void Update()
    {
        if(actions[doorStepIndex].State == State_MoveToPositionAction.FINISHED)
        {

            smallRumble.Deactivate();
            bigRumble.Deactivate();
            bigRumble.Activate();
            nextStep.SetActive(true);
            //nextHint.SetActive(true);
            //currentHint.SetActive(false);
            actions[doorStepIndex].enabled = false;
            if (lastStep)
            {
                invisibleWallOnDoor.SetActive(false);
                lightCircle.SetActive(false);
                foreach (GameObject go in lights)
                {
                    go.SetActive(false);
                }
            }
            shakeSmall.Reset();
            shakeSmall.Deactivate();
            shakeSmall.enabled = false;
            shakeBig.Activate();
            gameObject.SetActive(false);
        }
    }
}
