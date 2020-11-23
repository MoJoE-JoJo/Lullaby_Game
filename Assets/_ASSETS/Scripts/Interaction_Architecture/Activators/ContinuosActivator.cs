using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State_ContinuosActivator { IDLE, ACTIVATING, ACTIVATED, DEACTIVATING, DEACTIVATED }
public class ContinuosActivator : Activator
{
    [SerializeField] private MiniwheelSegmentHandler hintWheel;
    [SerializeField] private State_ContinuosActivator state = State_ContinuosActivator.IDLE;
    [SerializeField] private float activationDelay;
    [SerializeField] private float deactivationDelay;
    [SerializeField] [Range(0.0f, 1.0f)] private float minPressureValue = 0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float maxPressureValue = 1.0f;
    [SerializeField] private List<Song_Note> notes = new List<Song_Note>();
    private List<Song_Note> orderedNotes;
    private SongData lastData;
    private float timer = 0.0f;

    public override void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public override void SongInput(SongData data)
    {
        if (!enabled)
        {
            hintWheel.Hide();
            return;
        }
        else hintWheel.Show();
        lastData = data;
        if (CheckNotes(lastData))
        {
            if (state == State_ContinuosActivator.IDLE)
            {
                state = State_ContinuosActivator.ACTIVATING;
            }
            if (state == State_ContinuosActivator.ACTIVATING)
            {
                hintWheel.HighlightHint(new SongData { Notes = orderedNotes });

                if (timer >= activationDelay)
                {
                    state = State_ContinuosActivator.ACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
            }
            if (state == State_ContinuosActivator.ACTIVATED)
            {
                foreach(InteractableAction action in actions)
                {
                    action.InputData(data);
                    action.Activate();
                }
            }
            if(state == State_ContinuosActivator.DEACTIVATING)
            {
                state = State_ContinuosActivator.ACTIVATED;
                timer = 0.0f;
            }
        }
        else
        {
            if (state == State_ContinuosActivator.ACTIVATING)
            {
                state = State_ContinuosActivator.DEACTIVATED;
                if (timer > 0.0f) timer = 0.0f;
            }
            if (state == State_ContinuosActivator.ACTIVATED)
            {
                state = State_ContinuosActivator.DEACTIVATING;
                if (timer > 0.0f) timer = 0.0f;
            }
        }
    }

    private void Start()
    {
        lastData = new SongData();
        orderedNotes = new HashSet<Song_Note>(notes).ToList();
        orderedNotes.Sort();
    }

    private void Update()
    {
//#if UNITY_EDITOR
        if (orderedNotes != notes)
        {
            orderedNotes = new HashSet<Song_Note>(notes).ToList();
            orderedNotes.Sort();
        }
        //#endif
        if (state == State_ContinuosActivator.IDLE)
        {
            if (timer > 0.0f) timer = 0.0f;
            hintWheel.ShowNextHint(new SongData { Notes = orderedNotes });
            return;
        }
        if (state == State_ContinuosActivator.ACTIVATED)
        {
            hintWheel.HighlightHint(new SongData { Notes = orderedNotes });
            if (!CheckNotes(lastData))
            {
                state = State_ContinuosActivator.DEACTIVATING;
                if (timer > 0.0f) timer = 0.0f;
            }
        }
        if(state == State_ContinuosActivator.DEACTIVATING)
        {
            if (timer >= deactivationDelay) state = State_ContinuosActivator.DEACTIVATED;
            else timer += Time.deltaTime;
        }
        if(state == State_ContinuosActivator.DEACTIVATED)
        {
            foreach (InteractableAction action in actions)
            {
                action.Deactivate();
            }
            state = State_ContinuosActivator.IDLE;
            timer = 0.0f;

            hintWheel.ShowNextHint(new SongData { Notes = orderedNotes });
        }

        lastData.Notes = new List<Song_Note>();
    }

    public override void SetEnabled(bool input)
    {
        enabled = input;

        if (enabled) hintWheel.Show();
        else hintWheel.Hide();
    }
    private bool CheckNotes(SongData data)
    {
        if (data.Notes == null) return false;
        if (minPressureValue > data.Volume || data.Volume > maxPressureValue) return false;
        if (orderedNotes.Count > 1 && orderedNotes.Count != data.Notes.Count) return false;
        /*
        for (int i = 0; i < data.Notes.Count; i++)
        {
            if (data.Notes[i] != orderedNotes[i]) return false;
        }
        return true;
        */
        return orderedNotes.All(i => data.Notes.Contains(i));
    }

}
