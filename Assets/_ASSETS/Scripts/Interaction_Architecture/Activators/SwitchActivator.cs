using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State_SwitchActivator { ACTIVATING, ACTIVATED, DEACTIVATING, DEACTIVATED }

public class SwitchActivator : Activator
{

    [SerializeField] private State_SwitchActivator state = State_SwitchActivator.DEACTIVATED;

    [SerializeField] private MiniwheelSegmentHandler hintWheel;
    [SerializeField, Tooltip("If true then activation and deactivation delays are used for determining for how long the player must sing before input is registered")] private bool singForAmountOfTime = false;
    [SerializeField] private float activationDelay;
    [SerializeField] private float deactivationDelay;
    [SerializeField] private float minDurationBetweenSwitch = 4f;
    [SerializeField, Tooltip("If 0, then there is not auto turnoff")] private float autoTurnOffTime = 0;
    [SerializeField] private bool canDeactivate = true;
    [SerializeField] [Range(0.0f, 1.0f)] private float minPressureValue = 0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float maxPressureValue = 1.0f;
    [SerializeField] private List<Song_Note> notes = new List<Song_Note>();

    private List<Song_Note> orderedNotes;
    private SongData lastData;
    private float timer = 0.0f;
    private float swapTimer = float.PositiveInfinity;
    private float turnOffTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lastData = new SongData();
        orderedNotes = new HashSet<Song_Note>(notes).ToList(); //put in set to remove dublicates
        orderedNotes.Sort();
    }

    public override void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public override void SetEnabled(bool input)
    {
        base.SetEnabled(input);
        if (input) hintWheel.Show();
        else hintWheel.Hide();
    }

    public override void SongInput(SongData data)
    {
        if (!enabled)
        {
            hintWheel.Hide();
            return;
        }
        else hintWheel.Show();
        //lastData = data;
        if (CheckNotes(data) && swapTimer > minDurationBetweenSwitch)
        {
            lastData = data;
            if (state == State_SwitchActivator.ACTIVATED && canDeactivate)
            {
                if (singForAmountOfTime)
                {
                    if (timer >= deactivationDelay)
                    {
                        state = State_SwitchActivator.DEACTIVATED;
                        timer = 0.0f;
                        swapTimer = 0.0f;
                    }
                    else timer += Time.deltaTime;
                }
                else
                {
                    state = State_SwitchActivator.DEACTIVATING;
                }
            }
            if (state == State_SwitchActivator.DEACTIVATED)
            {
                if (singForAmountOfTime)
                {
                    if (timer >= activationDelay)
                    {
                        state = State_SwitchActivator.ACTIVATED;
                        timer = 0.0f;
                        swapTimer = 0.0f;
                    }
                    else timer += Time.deltaTime;
                }
                else
                {
                    state = State_SwitchActivator.ACTIVATING;
                    turnOffTimer = 0f;
                }
            }
            if (!singForAmountOfTime)
            {
                swapTimer = 0.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        swapTimer += Time.deltaTime;

        //so the notes are always sorted in the inspector
#if UNITY_EDITOR
        if (orderedNotes != notes)
        {
            orderedNotes = new HashSet<Song_Note>(notes).ToList();
            orderedNotes.Sort();
        }
#endif

        switch (state)
        {
            case State_SwitchActivator.ACTIVATING:
                hintWheel.HighlightHint(new SongData { Notes = notes });
                if (timer >= activationDelay)
                {
                    state = State_SwitchActivator.ACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
                break;

            case State_SwitchActivator.ACTIVATED:
                hintWheel.HighlightHint(new SongData { Notes = notes });
                foreach (InteractableAction action in actions)
                {
                    action.InputData(lastData);
                    action.Activate();
                }
                if (autoTurnOffTime != 0.0f && turnOffTimer > autoTurnOffTime)
                {
                    state = State_SwitchActivator.DEACTIVATING;
                }
                turnOffTimer += Time.deltaTime;
                break;

            case State_SwitchActivator.DEACTIVATING:
                hintWheel.ShowNextHint(new SongData { Notes = notes });

                if (timer >= deactivationDelay)
                {
                    state = State_SwitchActivator.DEACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
                break;

            case State_SwitchActivator.DEACTIVATED:
                hintWheel.ShowNextHint(new SongData { Notes = notes });

                foreach (InteractableAction action in actions)
                {
                    action.Deactivate();
                }
                //timer = 0.0f;
                break;

            default:
                break;
        }
    }

    //checks the inputed SongData if its notes matches with the 
    private bool CheckNotes(SongData data)
    {
        if (data.Notes == null) return false;
        if (minPressureValue > data.Volume || data.Volume > maxPressureValue) return false;
        if (orderedNotes.Count > 1 && orderedNotes.Count != data.Notes.Count) return false;
        /*
        if (orderedNotes.Count != data.Notes.Count) return false;
        for (int i = 0; i < data.Notes.Count; i++)
        {
            if (data.Notes[i] != orderedNotes[i]) return false;
        }
        return true;
        */
        return orderedNotes.All(i => data.Notes.Contains(i));
    }
}
