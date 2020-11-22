using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchStepper : MonoBehaviour
{
    public int stepIndex = 0;
    public int lastActiIndex = 0;
    public int currentActiIndex = 0;
    [SerializeField] private int correctStep;
    [SerializeField] private List<Color> colors;
    [SerializeField] private SequenceActivator sequenceActivator;
    [SerializeField] private Light2D lightSource;
    [SerializeField] private InteractableAction moveDoor;
    [SerializeField] private InteractableAction moveDoorBack;

    // Update is called once per frame
    void Update()
    {
        currentActiIndex = sequenceActivator.SequenceIndex;
        if (currentActiIndex > lastActiIndex)
        {
            stepIndex = (stepIndex + 1) % colors.Count;
        }
        lightSource.color = colors[stepIndex];

        if(stepIndex == correctStep)
        {
            moveDoor.Activate();
            moveDoorBack.Deactivate();
        }
        else
        {
            moveDoor.Deactivate();
            moveDoorBack.Activate();
        }

        lastActiIndex = currentActiIndex;
    }
}
