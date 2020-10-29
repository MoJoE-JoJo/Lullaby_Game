using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Song_NoteCoord {A,B,C,AB,AC,BC,ABC}
public enum Song_Attribute {High_Pitch};

[Serializable]
public struct SongData
{
    public SongData(String sequence)
    {
        //order string
        String.Concat(sequence.OrderBy(c => c));
        //convert string to chord 
        Enum.TryParse(sequence, out Song_NoteCoord noteChord);
        _noteCoord = noteChord;
        
        _volume = 1.0f;
        Attributes = new HashSet<Song_Attribute>();
    }
    
    /*
    public List<Song_Note> Notes {
        get
        {
            var list = _notes.ToList();
            list.Sort();
            return list;
        }
        set
        {
            _notes = new HashSet<Song_Note>(value);
        } 
    }
        private HashSet<Song_Note> _notes;
    */
    public Song_NoteCoord NoteCoord
    {
        get
        {
            return _noteCoord;
        }
        set
        {
            _noteCoord = value;
        }
    }
    [SerializeField]
    private Song_NoteCoord _noteCoord;
    public HashSet<Song_Attribute> Attributes { get; set; }
    public float Volume
    {
        get
        {
            return _volume;
        }
        set
        {
            if (value < 0.0f) _volume = 0.0f; 
            if (value > 1.0f) _volume = 1.0f; 
            else _volume = value;
        }
    }
    private float _volume;
}
