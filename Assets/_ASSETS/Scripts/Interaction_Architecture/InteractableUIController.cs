using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUIController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> uiNotes;
    [SerializeField] private Sprite unactivated;
    [SerializeField] private NoteSpriteLoader noteSpriteLoader;
    //public Dictionary<Song_NoteCoord, Sprite> uiNoteSprites;
    /*
    [Serializable]
    public struct SongToSprite
    {
        public Song_NoteCoord noteCoord;
        public Sprite sprite;
    }
    public SongToSprite[] uiNoteSprites;
    private Dictionary<Song_NoteCoord, Sprite> uiNoteSpriteDictionary;
    */

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

    public void ActivateAt(int uiIndex, Song_Note note)
    {
        if (uiIndex < uiNotes.Count)
        {
            uiNotes[uiIndex].sprite = noteSpriteLoader.NoteSpriteDictionary[note];
        }
    }

    private void Start()
    {
        noteSpriteLoader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NoteSpriteLoader>();
        unactivated = noteSpriteLoader.Unactivated;
        //uiNotes[0].sprite = unactivated;
    }
}
