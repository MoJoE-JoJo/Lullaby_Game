using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public enum State_SequenceActivator { IDLE, RESETTING, TRANSISTION, MIDSEQUENCE, COMPLETE }

public class SequenceActivator : Activator
{

    [SerializeField] private State_SequenceActivator state = State_SequenceActivator.IDLE;

    [SerializeField] private Hint hintWheel;
    //[SerializeField] private float activationDelay;
    //[SerializeField] private float deactivationDelay;
    //[SerializeField] private float minDurBetweenSwitch = 4f;
    //[SerializeField] private float autoTurnOffTime = 0;
    //[SerializeField] private bool canDeactivate = true;
    [SerializeField] private bool activateOnComplete = false;
    [SerializeField] private float transitionTime = 1.0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float minPressureValue = 0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float maxPressureValue = 1.0f;
    //[SerializeField] private List<List<Song_Note>> sequence = new List<List<Song_Note>>();

    private SequencePart nextCorrectPart;
    private SongData lastData;
    private SongData lasterData;
    //private bool singingCorrect = false;
    private float playingCorrectTimer = 0.0f;
    private float recievingNoInputTimer = 0.0f;
    private float transitionTimer = 0.0f;
    private float timeSinceLastInput = 0.0f;

    private int sequenceIndex = 0;
    //private float swapTimer = 4.0f;
    //private float turnOffTimer = 0f;

    [SerializeField] private List<SequencePart> sequence = new List<SequencePart>();
    [System.Serializable]
    private class SequencePart
    {
        [SerializeField]
        public List<Song_Note> Chord;
        [SerializeField] public float Playtime; //amount of time the choord needs to be played before accepting
        [SerializeField] public float DeactivationDelay; //if the delayTime is passed without recieving the correct input  (or a input??) 

    }

    // Start is called before the first frame update
    void Start()
    {
        lastData = new SongData();
        foreach (var seqPart in sequence)
        {
            seqPart.Chord = seqPart.Chord.Distinct().OrderBy(x => x).ToList();
        }
        nextCorrectPart = sequence[0];
    }


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
        if (CheckNotes(data))
        {
            if (state == State_SequenceActivator.IDLE)
            {
                state = State_SequenceActivator.MIDSEQUENCE;
                playingCorrectTimer = 0.0f;
            }
            if (state == State_SequenceActivator.MIDSEQUENCE)
            {
                recievingNoInputTimer = 0.0f;
            }
        }
        //else
        //{
        //    if (state == State_SequenceActivator.MIDSEQUENCE)
        //    {
        //        singingCorrect = false;
        //    }
        //}
        timeSinceLastInput = 0;
        lastData = data;
        lasterData = data;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastInput > 0.2f)
        {
            //reset data
            lastData = new SongData{ Notes = new List<Song_Note>()};
            playingCorrectTimer = 0f;
        }
        timeSinceLastInput += Time.deltaTime;



        switch (state)
        {
            case State_SequenceActivator.MIDSEQUENCE:
                if (!activateOnComplete)
                {
                    //foreach (InteractableAction action in actions)
                    //{
                    //    action.Deactivate();
                    //}
                }
                if (playingCorrectTimer >= nextCorrectPart.Playtime)
                {
                    hintWheel.HighlightHint(new SongData { Notes = nextCorrectPart.Chord });
                    transitionTimer = 0.0f;
                    state = State_SequenceActivator.TRANSISTION;
                }
                else
                {
                    hintWheel.ShowNextHint(new SongData { Notes = nextCorrectPart.Chord });
                }
                if (CheckNotes(lastData)) playingCorrectTimer += Time.deltaTime;
                else if (lastData.Notes == null || lastData.Notes.Count == 0)
                {
                    recievingNoInputTimer += Time.deltaTime;

                }
                else state = State_SequenceActivator.RESETTING;

                if (recievingNoInputTimer >= nextCorrectPart.DeactivationDelay)
                {
                    state = State_SequenceActivator.RESETTING;
                }

                break;

            case State_SequenceActivator.TRANSISTION:
                if (transitionTimer >= transitionTime)
                {
                    state = State_SequenceActivator.MIDSEQUENCE;
                    NextPartOfSequence();
                    playingCorrectTimer = 0.0f;
                    recievingNoInputTimer = 0.0f;
                }
                else
                {
                    if (!activateOnComplete)
                    {
                        foreach (InteractableAction action in actions)
                        {
                            action.InputData(lasterData);
                            action.Activate();
                        }
                    }
                }
                transitionTimer += Time.deltaTime;
                break;

            case State_SequenceActivator.IDLE:
                hintWheel.ShowNextHint(new SongData { Notes = nextCorrectPart.Chord });
                break;

            case State_SequenceActivator.RESETTING:
                foreach (InteractableAction action in actions)
                {
                    action.Deactivate();
                }
                    ResetSequence();
                state = State_SequenceActivator.IDLE;
                break;

            case State_SequenceActivator.COMPLETE:
                if (activateOnComplete)
                {
                    foreach (InteractableAction action in actions)
                    {
                        action.InputData(lasterData);
                        action.Activate();
                    }
                    
                }
                    //action.InputData(lastData);

                break;
        }
    }


    private void NextPartOfSequence()
    {
        sequenceIndex++;
        if (sequenceIndex == sequence.Count)
        {
            state = State_SequenceActivator.COMPLETE;
            return;
        }
        nextCorrectPart = sequence[sequenceIndex];
    }

    private void ResetSequence()
    {
        sequenceIndex = 0;
        nextCorrectPart = sequence[sequenceIndex];
        hintWheel.ShowNextHint(new SongData { Notes = nextCorrectPart.Chord });
    }


    //checks the inputed SongData if its notes matches with the 
    private bool CheckNotes(SongData data)
    {
        if (data.Notes == null) return false;
        if (minPressureValue > data.Volume || data.Volume > maxPressureValue) return false;
        if (nextCorrectPart.Chord.Count > 1 && nextCorrectPart.Chord.Count != data.Notes.Count) return false;
        /*
        if (nextCorrectPart.Chord.Count != data.Notes.Count) return false;
        
        for (int i = 0; i < data.Notes.Count; i++)
        {
            if (data.Notes[i] != nextCorrectPart.Chord[i]) return false;
        }
        return true;
        */
        return nextCorrectPart.Chord.All(i => data.Notes.Contains(i));
    }
}
