using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpriteLoader : MonoBehaviour
{
    public Dictionary<Song_Note, Sprite> NoteSpriteDictionary { get; private set; }
    public Sprite Unactivated { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        NoteSpriteDictionary = new Dictionary<Song_Note, Sprite>();
        Unactivated = Resources.Load<Sprite>("Sprites/Note_Sprites/note_unactivated");
        var values = Enum.GetValues(typeof(Song_Note));
        Debug.Log(values.GetValue(0));
        for (int i=0; i<values.Length; i++)
        {
            Song_Note note = (Song_Note)values.GetValue(i);
            NoteSpriteDictionary.Add(note, Resources.Load<Sprite>($"Sprites/Note_Sprites/note_{values.GetValue(i).ToString()}"));
        }
    }
}
