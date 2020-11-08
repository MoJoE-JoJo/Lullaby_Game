using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class NoteController : MonoBehaviour
{
    private EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance;

    [EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent;

    //public KeyCode SingButton;

    //public bool ANote, BNote, CNote, DNote, GNote;

    public bool singing;
    private bool alreadySingingNote = false;
    public List<Song_Note> note = new List<Song_Note>();
    private bool isSinging = false;

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

        eventInstanceList.Add(Ainstance);
        eventInstanceList.Add(Binstance);
        eventInstanceList.Add(Cinstance);
        eventInstanceList.Add(Dinstance);
        eventInstanceList.Add(Ginstance);
    }


    public void StartSinging(SongData sd)
    {
        // parse SongData enum
        var notes = sd.Notes;

        // turn on notes based on the string
        foreach (Song_Note sn in notes)
        {
            if (sn == Song_Note.A) StartInstance(Ainstance);
            if (sn == Song_Note.B) StartInstance(Binstance);
            if (sn == Song_Note.C) StartInstance(Cinstance);
            if (sn == Song_Note.D) StartInstance(Dinstance);
            if (sn == Song_Note.E) StartInstance(Ginstance);
        }
        alreadySingingNote = true;
    }

    public void StopSinging()
    {

        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.setParameterByName("isSinging", 0);
            eventInstance.setParameterByName("wasSingingOtherTone", 0);
            //eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        alreadySingingNote = false;
    }


    private void StartInstance(EventInstance eventInstance)
    {
        eventInstance.setParameterByName("isSinging", 1);
        if (alreadySingingNote)
        {
            eventInstance.setParameterByName("wasSingingOtherTone", 1);
        }
        eventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (singing && !isSinging)
        {
            StartSinging(new SongData { Notes = note });
            isSinging = true;
        }
        else if (!singing && isSinging)
        {
            StopSinging();
            isSinging = false;
        }
    }
}