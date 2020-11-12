using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Song_Note {A,B,C,D,E,F}
[Serializable]
public struct SongData
{
    public SongData(String sequence)
    {
        //order string
        sequence = String.Concat(sequence.OrderBy(c => c));
        //convert string to chord 
        _notes = new List<Song_Note>();
        foreach (char c in sequence)
        {
            Enum.TryParse(c+"", out Song_Note note);
            _notes.Add(note);
        }
        _notes = new HashSet<Song_Note>(_notes).ToList();
        _notes.Sort();

        _volume = 1.0f;
        _duration = 0.0f;
    }
    public List<Song_Note> Notes
    {
        get => _notes;
        set
        {
            _notes = new HashSet<Song_Note>(value).ToList();
            _notes.Sort();
        }
    }
    [SerializeField] private List<Song_Note> _notes;
    public float Volume
    {
        get => _volume;
        set => _volume = Mathf.Clamp(value, 0.0f, 1.0f);
    }
    [SerializeField] private float _volume;

    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }
    [SerializeField] private float _duration;
}
