using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State_SwitchActivator {ACTIVATING, ACTIVATED, DEACTIVATING, DEACTIVATED}

public class SwitchActivator : Activator
{

    [SerializeField] private State_SwitchActivator state = State_SwitchActivator.DEACTIVATED;

    [SerializeField] private MiniwheelSegmentHandler wheel;
    [SerializeField] private float activationDelay;
    [SerializeField] private float deactivationDelay;
    [SerializeField] private float minDurBetweenSwitch= 4f;
    [SerializeField] private List<Song_Note> notes = new List<Song_Note>();

    private List<Song_Note> orderedNotes;
    private SongData lastData;
    private float timer = 0.0f;
    private float swapTimer = 4.0f;


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

    public override void SongInput(SongData data)
    {
        //lastData = data;
        if (CheckNotes(data) && swapTimer > minDurBetweenSwitch)
        {
            lastData = data;
            if (state == State_SwitchActivator.ACTIVATED) state = State_SwitchActivator.DEACTIVATING;
            if(state == State_SwitchActivator.DEACTIVATED) state = State_SwitchActivator.ACTIVATING;
            swapTimer = 0.0f;
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
                wheel.HighlightHint(new SongData { Notes = notes });
                if (timer >= activationDelay)
                {
                    state = State_SwitchActivator.ACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
                break;

            case State_SwitchActivator.ACTIVATED:
                wheel.HighlightHint(new SongData { Notes = notes });
                action.Activate();
                action.InputData(lastData);
                break;

            case State_SwitchActivator.DEACTIVATING:
                wheel.ShowNextHint(new SongData { Notes = notes });

                if (timer >= activationDelay)
                {
                    state = State_SwitchActivator.DEACTIVATED;
                    timer = 0.0f;
                }
                else timer += Time.deltaTime;
                break;

            case State_SwitchActivator.DEACTIVATED:
                wheel.ShowNextHint(new SongData { Notes = notes });

                action.Deactivate();
                timer = 0.0f;
                break;

            default:
                break;
        }
    }

    //checks the inputed SongData if its notes matches with the 
    private bool CheckNotes(SongData data)
    {
        if (orderedNotes.Count != data.Notes.Count) return false;
        for (int i = 0; i < data.Notes.Count; i++)
        {
            if (data.Notes[i] != orderedNotes[i]) return false;
        }
        return true;
    }
}
