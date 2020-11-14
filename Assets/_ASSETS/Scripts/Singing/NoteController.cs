using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class NoteController : MonoBehaviour
{
    private EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance, Einstance;

    [EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent, EEvent;
    private bool alreadySingingNote = false;

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

    // Update is called once per frame
    void Update()
    {
        if (_isSinging && !_wasSingingBefore)
        {
            StartSinging();
        }
        else if (_isSinging)
        {
            UpdateSinging();
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
            if (sn == Song_Note.A) StartInstance(Ainstance, _songData.Volume);
            if (sn == Song_Note.B) StartInstance(Binstance, _songData.Volume);
            if (sn == Song_Note.C) StartInstance(Cinstance, _songData.Volume);
            if (sn == Song_Note.D) StartInstance(Dinstance, _songData.Volume);
            if (sn == Song_Note.E) StartInstance(Ginstance, _songData.Volume);
            if (sn == Song_Note.F) StartInstance(Einstance, _songData.Volume);

        }
        alreadySingingNote = true;
    }

    public void StopSinging()
    {
        _wasSingingBefore = false;

        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.setParameterByName("isSinging", 0);
            eventInstance.setParameterByName("wasSingingOtherTone", 0);
            //eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        alreadySingingNote = false;
    }

    private void UpdateSinging()
    {
        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.setVolume(_songData.Volume);
        }
    }


    private void StartInstance(EventInstance eventInstance, float volume)
    {
        eventInstance.setParameterByName("isSinging", 1);
        if (alreadySingingNote)
        {
            eventInstance.setParameterByName("wasSingingOtherTone", 1);
        }
        eventInstance.setVolume(volume);
        eventInstance.start();
    }
}