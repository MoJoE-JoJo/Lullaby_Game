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


    private SongData _songData;
    public Song_NoteCoord note;
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

        eventInstanceList.Add(Ainstance);
        eventInstanceList.Add(Binstance);
        eventInstanceList.Add(Cinstance);
        eventInstanceList.Add(Dinstance);
        eventInstanceList.Add(Ginstance);
    }


    public void StartSinging()
    {
        _wasSingingBefore = true;
        
        // parse SongData enum
        var notes = _songData.NoteCoord.ToString();

        // turn on notes based on the string
        foreach (char c in notes)
        {
            if (c == 'A') StartInstance(Ainstance, _songData.Volume);
            if (c == 'B') StartInstance(Binstance, _songData.Volume);
            if (c == 'C') StartInstance(Cinstance, _songData.Volume);
            if (c == 'D') StartInstance(Dinstance, _songData.Volume);
            if (c == 'E') StartInstance(Ginstance, _songData.Volume);
        }
    }

    public void StopSinging()
    {
        _wasSingingBefore = false;

        foreach (var eventInstance in eventInstanceList)
        {
            eventInstance.setParameterByName("isSinging", 0);
            //eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
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
        eventInstance.setVolume(volume);
        eventInstance.start();
    }

    // Update is called once per frame, used for testing at the time
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
}