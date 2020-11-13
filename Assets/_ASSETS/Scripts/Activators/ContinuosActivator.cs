﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State_ContinuosActivator { IDLE, ACTIVATING, ACTIVATED, DEACTIVATING, DEACTIVATED }
public class ContinuosActivator : Activator
{
    [SerializeField] private MiniwheelSegmentHandler wheel;
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
        lastData = data;
        if (CheckNotes(lastData))
        {
            if (state == State_ContinuosActivator.IDLE)
            {
                state = State_ContinuosActivator.ACTIVATING;
            }
            if (state == State_ContinuosActivator.ACTIVATING)
            {
                wheel.HighlightHint(new SongData { Notes = notes });
                if (timer >= activationDelay)
                {
                    state = State_ContinuosActivator.ACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
            }
            if (state == State_ContinuosActivator.ACTIVATED)
            {
                action.Activate();
                action.InputData(data);
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
#if UNITY_EDITOR
        if (orderedNotes != notes)
        {
            orderedNotes = new HashSet<Song_Note>(notes).ToList();
            orderedNotes.Sort();
        }
#endif
        if (state == State_ContinuosActivator.ACTIVATED)
        {
            wheel.HighlightHint(new SongData { Notes = notes });
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
            action.Deactivate();
            state = State_ContinuosActivator.IDLE;
            timer = 0.0f;

            wheel.ShowNextHint(new SongData { Notes = notes });
        }
        if(state == State_ContinuosActivator.IDLE)
        {
            if (timer > 0.0f) timer = 0.0f;
            wheel.ShowNextHint(new SongData { Notes = notes });
        }
        lastData.Notes = new List<Song_Note>();
    }

    private bool CheckNotes(SongData data)
    {
        if (minPressureValue > data.Volume || data.Volume > maxPressureValue) return false;
        if (orderedNotes.Count != data.Notes.Count) return false;
        for (int i = 0; i < data.Notes.Count; i++)
        {
            if (data.Notes[i] != orderedNotes[i]) return false;
        }
        return true;
    }

}