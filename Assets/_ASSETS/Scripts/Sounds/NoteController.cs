using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using System.Linq;

public class NoteController : MonoBehaviour
{
    private EventInstance AinstanceVoice, BinstanceVoice, CinstanceVoice, DinstanceVoice, EinstanceVoice, FinstanceVoice;

    private EventInstance AinstanceSynth, BinstanceSynth, CinstanceSynth, DinstanceSynth, EinstanceSynth, FinstanceSynth;

    [EventRef]
    public string AEventVoice, BEventVoice, CEventVoice, DEventVoice, EEventVoice, FEventVoice;

    [EventRef]
    public string AEventSynth, BEventSynth, CEventSynth, DEventSynth, EEventSynth, FEventSynth;

    public SongData SongData
    {
        get => _songData;
        set => _songData = value;
    }

    public bool IsSinging
    {
        get => _isSinging;
        set => _isSinging = value;
    }
    private SongData _songData;
    private bool _isSinging = false;
    private bool _wasSingingBefore = false;

    private List<Song_Note> _currentlySingingNotes = new List<Song_Note>();


    private List<EventInstance> eventInstanceList;

    // Start is called before the first frame update
    void Start()
    {
        eventInstanceList = new List<EventInstance>();
        AinstanceVoice = RuntimeManager.CreateInstance(AEventVoice);
        BinstanceVoice = RuntimeManager.CreateInstance(BEventVoice);
        CinstanceVoice = RuntimeManager.CreateInstance(CEventVoice);
        DinstanceVoice = RuntimeManager.CreateInstance(DEventVoice);
        EinstanceVoice = RuntimeManager.CreateInstance(EEventVoice);
        FinstanceVoice = RuntimeManager.CreateInstance(FEventVoice);

        AinstanceSynth = RuntimeManager.CreateInstance(AEventSynth);
        BinstanceSynth = RuntimeManager.CreateInstance(BEventSynth);
        CinstanceSynth = RuntimeManager.CreateInstance(CEventSynth);
        DinstanceSynth = RuntimeManager.CreateInstance(DEventSynth);
        EinstanceSynth = RuntimeManager.CreateInstance(EEventSynth);
        FinstanceSynth = RuntimeManager.CreateInstance(FEventSynth);

        eventInstanceList.Add(AinstanceVoice);
        eventInstanceList.Add(BinstanceVoice);
        eventInstanceList.Add(CinstanceVoice);
        eventInstanceList.Add(DinstanceVoice);
        eventInstanceList.Add(EinstanceVoice);
        eventInstanceList.Add(FinstanceVoice);

        eventInstanceList.Add(AinstanceSynth);
        eventInstanceList.Add(BinstanceSynth);
        eventInstanceList.Add(CinstanceSynth);
        eventInstanceList.Add(DinstanceSynth);
        eventInstanceList.Add(EinstanceSynth);
        eventInstanceList.Add(FinstanceSynth);
    }
    private void FixedUpdate()
    {
        //Debug.Log("Is Singing: "+ _isSinging);
        
        if (_isSinging)
        {
            if (!_wasSingingBefore)
            {
                StartSinging();    
            }
            else
            {
                //Debug.Log(Time.deltaTime);
                UpdateSinging();
            }
        }
        else if (!_isSinging)
        {
            StopSinging();
        }
    }

    public void StartSinging()
    {
        _wasSingingBefore = true;
        
        // parse SongData enum

        var notes = _songData.Notes;

        
        // turn on notes based on the string
        foreach (Song_Note sn in notes)
        {
            if (sn == Song_Note.A)
            {
                StartInstance(AinstanceVoice, _songData.Volume);
                StartInstance(AinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }
            if (sn == Song_Note.B)
            {
                StartInstance(BinstanceVoice, _songData.Volume);
                StartInstance(BinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.C)
            {
                StartInstance(CinstanceVoice, _songData.Volume);
                StartInstance(CinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.D)
            {
                StartInstance(DinstanceVoice, _songData.Volume);
                StartInstance(DinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.E)
            {
                StartInstance(EinstanceVoice, _songData.Volume);
                StartInstance(EinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.F)
            {
                StartInstance(FinstanceVoice, _songData.Volume);
                StartInstance(FinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }
        }
        //alreadySingingNote = true;
    }

    public void StopSinging()
    {
        _wasSingingBefore = false;

        foreach (var eventInstance in eventInstanceList)
        {
            //eventInstance.setParameterByName("isSinging", 0);
            //eventInstance.setParameterByName("wasSingingOtherTone", 0);
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        _currentlySingingNotes.Clear();
        //alreadySingingNote = false;
    }

    private void UpdateSinging()
    {
        var notesToStart = _songData.Notes.Except(_currentlySingingNotes);
        //Debug.Log(notesToStart);
        
        foreach (var noteToStart in notesToStart)
        {
            if (noteToStart == Song_Note.A)
            {
                StartInstance(AinstanceVoice, _songData.Volume);
                StartInstance(AinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start A");
            }
            if (noteToStart == Song_Note.B)
            {
                StartInstance(BinstanceVoice, _songData.Volume);
                StartInstance(BinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start B");
            }

            if (noteToStart == Song_Note.C)
            {
                StartInstance(CinstanceVoice, _songData.Volume);
                StartInstance(CinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start C");
            }

            if (noteToStart == Song_Note.D)
            {
                StartInstance(DinstanceVoice, _songData.Volume);
                StartInstance(DinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start D");
            }

            if (noteToStart == Song_Note.E)
            {
                StartInstance(EinstanceVoice, _songData.Volume);
                StartInstance(EinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start E");
            }

            if (noteToStart == Song_Note.F)
            {
                StartInstance(FinstanceVoice, _songData.Volume);
                StartInstance(FinstanceSynth, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start F");
            }
        }
        
        var notesToStop = _currentlySingingNotes.Except(_songData.Notes).ToList();

        String notesStopping = "";

        foreach (Song_Note noteToStop in notesToStop)
        {
            if (noteToStop == Song_Note.A)
            {
                StopInstance(AinstanceVoice);
                StopInstance(AinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.B)
            {
                StopInstance(BinstanceVoice);
                StopInstance(BinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.C)
            {
                StopInstance(CinstanceVoice);
                StopInstance(CinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.D)
            {
                StopInstance(DinstanceVoice);
                StopInstance(DinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.E)
            {
                StopInstance(EinstanceVoice);
                StopInstance(EinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.F)
            {
                StopInstance(FinstanceVoice);
                StopInstance(FinstanceSynth);
                _currentlySingingNotes.Remove(noteToStop);
            }

            notesStopping += noteToStop.ToString();
        }
        //Debug.Log("Notes to stop: " + notesToStop);
        
        foreach (var instance in eventInstanceList)
        {
            instance.setVolume(_songData.Volume);
        }

    }


    private void StartInstance(EventInstance eventInstance, float volume)
    {
        eventInstance.setVolume(volume);
        eventInstance.start();
    }


    private void StopInstance(EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.setParameterByName("isSinging", 0);
    }
}