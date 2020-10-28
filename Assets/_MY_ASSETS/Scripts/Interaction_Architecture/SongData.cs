using System.Collections.Generic;
using System.Linq;

public enum Song_NoteCoord {A,B,C,AB,BC,ABC}
public enum Song_Attribute {High_Pitch};

public struct SongData
{
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
    public Song_NoteCoord NoteCoord{get; set;}
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
