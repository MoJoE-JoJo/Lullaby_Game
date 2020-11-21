using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum State_WheelRotateActivator { IDLE, CLOCKWISE, COUNTERCLOCKWISE }


public class WheelRotateActivator : Activator
{
    [SerializeField] private Hint hintWheel;

    //[SerializeField] private bool repeatSequence = false;
    //[SerializeField, Tooltip("If true, will activate will in the transistion state, and if false, will only activate the Actions when the sequence is completed")] private bool activateWhileTransition = true;
    //[SerializeField, Tooltip("If true, it will deactivate the Actions when you need to input the next part of the sequence")] private bool deactivateMidsequence = false;
    //[SerializeField] private float transitionTime = 1.0f;
    //[SerializeField] [Range(0.0f, 1.0f)] private float minPressureValue = 0f;
    //[SerializeField] [Range(0.0f, 1.0f)] private float maxPressureValue = 1.0f;
    //[SerializeField] private List<List<Song_Note>> sequence = new List<List<Song_Note>>();

    //private bool singingCorrect = false;
    //private float swapTimer = 4.0f;
    //private float turnOffTimer = 0f;
    
    
    
    
    
    //FER SHIT
    private List<Song_Note> _notes;
    private Song_Note currentSongNote = Song_Note.A;
    private bool songStarted = false;
    private Song_Note nextSong = Song_Note.A;
    private Song_Note previousSong = Song_Note.A;
    private State_WheelRotateActivator state = State_WheelRotateActivator.IDLE;
    public List<InteractableAction> wheelRotateActions;
    private float timeSinceActionStarted;
    public float timeTheActionIsPlayed;
    private float timeSinceLastInput;

    // Start is called before the first frame update
    void Start()
    {
        _notes = new List<Song_Note>();
        _notes.Add(Song_Note.A);
        _notes.Add(Song_Note.B);
        _notes.Add(Song_Note.C);
        _notes.Add(Song_Note.D);
        _notes.Add(Song_Note.E);
        _notes.Add(Song_Note.F);
    }


    public override void ShowHint()
    {
        throw new System.NotImplementedException();
    }

    public override void SongInput(SongData data)
    {
        if (!enabled)
        {
            //hintWheel.Hide();
            return;
        }
        //else hintWheel.Show();

        CheckNotes(data);
        
        if (CheckNotes(data))
        {
            if (state == State_WheelRotateActivator.CLOCKWISE)
            {
                
            }
            if (state == State_WheelRotateActivator.COUNTERCLOCKWISE)
            {
                
            }
        }

        timeSinceLastInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastInput > 0.2f)
        {
            //reset data
            state = State_WheelRotateActivator.IDLE;
            songStarted = false;
        }
        timeSinceLastInput += Time.deltaTime;
        timeSinceActionStarted += Time.deltaTime;

        if (timeSinceActionStarted >= timeTheActionIsPlayed)
        {
            state = State_WheelRotateActivator.IDLE;
        }
        
        
        switch (state)
        {
            case State_WheelRotateActivator.IDLE:
                wheelRotateActions[0].Deactivate();
                wheelRotateActions[1].Deactivate();
                //do nothing
                break;

            case State_WheelRotateActivator.CLOCKWISE:
                wheelRotateActions[1].Deactivate();
                wheelRotateActions[0].Activate();
                break;

            case State_WheelRotateActivator.COUNTERCLOCKWISE:
                wheelRotateActions[0].Deactivate();
                wheelRotateActions[1].Activate();
                break;
        }
    }


    //checks the inputed SongData if its notes matches with the 
    private bool CheckNotes(SongData data)
    {
        if (!songStarted)
        {
            if (data.Notes.Count == 1)
            {
                songStarted = true;
                currentSongNote = data.Notes[0];
                setPreviousNext(currentSongNote);
            }
        }
        else
        {
            if (data.Notes.Count == 1)
            {
                var newCurrentSongNote = data.Notes[0];
                
                if (newCurrentSongNote == nextSong)
                {
                    state = State_WheelRotateActivator.CLOCKWISE;
                    setPreviousNext(newCurrentSongNote);
                    timeSinceActionStarted = 0;
                }
                else if (newCurrentSongNote == previousSong)
                {
                    state = State_WheelRotateActivator.COUNTERCLOCKWISE;
                    setPreviousNext(newCurrentSongNote);
                    timeSinceActionStarted = 0;
                }
                else if (newCurrentSongNote != currentSongNote)
                {
                    currentSongNote = newCurrentSongNote;
                    state = State_WheelRotateActivator.IDLE;
                }
                else if (newCurrentSongNote == currentSongNote)
                {
                }
            }
        }

        return true;
    }

    private void setPreviousNext(Song_Note current)
    {
        currentSongNote = current;
        
        if (current == Song_Note.A)
        {
            nextSong = Song_Note.B;
            previousSong = Song_Note.F;
        }else if (current == Song_Note.B)
        {
            nextSong = Song_Note.C;
            previousSong = Song_Note.A;
        }else if (current == Song_Note.C)
        {
            nextSong = Song_Note.D;
            previousSong = Song_Note.B;
        }else if (current == Song_Note.D)
        {
            nextSong = Song_Note.E;
            previousSong = Song_Note.C;
        }else if (current == Song_Note.E)
        {
            nextSong = Song_Note.F;
            previousSong = Song_Note.D;
        }else if (current == Song_Note.F)
        {
            nextSong = Song_Note.A;
            previousSong = Song_Note.E;
        }
    }
}
