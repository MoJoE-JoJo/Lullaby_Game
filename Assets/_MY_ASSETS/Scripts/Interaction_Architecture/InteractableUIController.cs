using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUIController : MonoBehaviour
{
    public List<SpriteRenderer> uiNotes;
    public Sprite unactivated;
    //public Dictionary<Song_NoteCoord, Sprite> uiNoteSprites;
    [Serializable]
    public struct SongToSprite
    {
        public Song_NoteCoord noteCoord;
        public Sprite sprite;
    }
    public SongToSprite[] uiNoteSprites;
    private Dictionary<Song_NoteCoord, Sprite> uiNoteSpriteDictionary;

    public void InputBufferCleared()
    {
        foreach(SpriteRenderer sp in uiNotes)
        {
            sp.sprite = unactivated;
        }
    }

    public void ShowHint()
    {

    }

    public void Activate(int uiIndex, Song_NoteCoord color)
    {
        uiNotes[uiIndex].sprite = uiNoteSpriteDictionary[color];
    }
}
