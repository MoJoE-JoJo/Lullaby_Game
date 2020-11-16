using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using System.Linq;

public class NoteController : MonoBehaviour
{
    private EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance, Einstance;

    [EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent, EEvent;
    //private bool alreadySingingNote = false;

    //public KeyCode SingButton;

    //public bool ANote, BNote, CNote, DNote, GNote;
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
        Ainstance = RuntimeManager.CreateInstance(AEvent);
        Binstance = RuntimeManager.CreateInstance(BEvent);
        Cinstance = RuntimeManager.CreateInstance(CEvent);
        Dinstance = RuntimeManager.CreateInstance(DEvent);
        Ginstance = RuntimeManager.CreateInstance(GEvent);
        Einstance = RuntimeManager.CreateInstance(EEvent);

        eventInstanceList.Add(Ainstance);
        eventInstanceList.Add(Binstance);
        eventInstanceList.Add(Cinstance);
        eventInstanceList.Add(Dinstance);
        eventInstanceList.Add(Ginstance);
        eventInstanceList.Add(Einstance);
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
                StartInstance(Ainstance, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }
            if (sn == Song_Note.B)
            {
                StartInstance(Binstance, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.C)
            {
                StartInstance(Cinstance, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.D)
            {
                StartInstance(Dinstance, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.E)
            {
                StartInstance(Ginstance, _songData.Volume);
                _currentlySingingNotes.Add(sn);
            }

            if (sn == Song_Note.F)
            {
                StartInstance(Einstance, _songData.Volume);
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
            eventInstance.setParameterByName("isSinging", 0);
            eventInstance.setParameterByName("wasSingingOtherTone", 0);
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
                StartInstance(Ainstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start A");
            }
            if (noteToStart == Song_Note.B)
            {
                StartInstance(Binstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start B");
            }

            if (noteToStart == Song_Note.C)
            {
                StartInstance(Cinstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start C");
            }

            if (noteToStart == Song_Note.D)
            {
                StartInstance(Dinstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start D");
            }

            if (noteToStart == Song_Note.E)
            {
                StartInstance(Ginstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start E");
            }

            if (noteToStart == Song_Note.F)
            {
                StartInstance(Einstance, _songData.Volume);
                _currentlySingingNotes.Add(noteToStart);
                //Debug.Log(Time.deltaTime);
                //Debug.Log("Start F");
            }
        }
        
        
        List<Song_Note> notesToStop = new List<Song_Note>();
        
        notesToStop = _currentlySingingNotes.Except(_songData.Notes).ToList();

        String notesStopping = "";

        foreach (Song_Note noteToStop in notesToStop)
        {
            if (noteToStop == Song_Note.A)
            {
                StopInstance(Ainstance);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.B)
            {
                StopInstance(Binstance);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.C)
            {
                StopInstance(Cinstance);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.D)
            {
                StopInstance(Dinstance);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.E)
            {
                StopInstance(Ginstance);
                _currentlySingingNotes.Remove(noteToStop);
            }
            else if (noteToStop == Song_Note.F)
            {
                StopInstance(Einstance);
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
        //eventInstance.setParameterByName("isSinging", 1);
        eventInstance.setVolume(volume);
        eventInstance.start();

        //foreach (var eventInstances in eventInstanceList)
        //{
        //    eventInstances.setParameterByName("wasSingingOtherTone", 1);
        //}

        //eventInstance.setParameterByName("wasSingingOtherTone", 0);
    }


    private void StopInstance(EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.setParameterByName("isSinging", 0);
    }
}