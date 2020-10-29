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

        singing = false;
    }


    void StartSinging(SongData sd)
    {
        // parse SongData enum
        var notes = sd.NoteCoord.ToString();

        // turn on notes based on the string
        foreach (char c in notes)
        {
            if (c == 'A') Ainstance.start();
            if (c == 'B') Binstance.start();
            if (c == 'D') Cinstance.start();
            if (c == 'E') Dinstance.start();
            if (c == 'F') Ginstance.start();
        }
    }

    public void StopSinging()
    {

        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    // Update is called once per frame, used for testing at the time
    void Update()
    {
        if (!singing)
        {
            singing = true;
            StartSinging(new SongData { NoteCoord = Song_NoteCoord.ABC });
        }
    }
}