using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class NoteController : MonoBehaviour
{
    private EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance;

    [EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent;

    //public KeyCode SingButton;

    //public bool ANote, BNote, CNote, DNote, GNote;

    public bool singing;
    public Song_NoteCoord note;
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
        var notes = sd.NoteCoord.ToString();

        // turn on notes based on the string
        foreach (char c in notes)
        {
            if (c == 'A') StartInstance(Ainstance);
            if (c == 'B') StartInstance(Binstance);
            if (c == 'C') StartInstance(Cinstance);
            if (c == 'D') StartInstance(Dinstance);
            if (c == 'G') StartInstance(Ginstance);
        }
    }

    public void StopSinging()
    {

        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.setParameterByName("isSinging", 0);
            //eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }


    private void StartInstance(EventInstance eventInstance)
    {
        eventInstance.setParameterByName("isSinging", 1);
        eventInstance.start();
    }

    // Update is called once per frame, used for testing at the time
    void Update()
    {
        if (singing && !isSinging)
        {
            StartSinging(new SongData { NoteCoord = note });
            isSinging = true;
        }
        else if (!singing && isSinging)
        {
            StopSinging();
            isSinging = false;
        }
    }
}