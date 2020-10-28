using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSongInputter : MonoBehaviour
{
    public bool APressed = false;
    public bool BPressed = false;
    public bool CPressed = false;
    public bool DPressed = false;
    public bool EPressed = false;

    public List<Activator> activators;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) APressed = true;
        if (Input.GetKeyDown(KeyCode.B)) BPressed = true;
        if (Input.GetKeyDown(KeyCode.C)) CPressed = true;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SongData data = new SongData();
            if (APressed && BPressed && CPressed) data.NoteCoord = Song_NoteCoord.ABC;
            else if (APressed && BPressed) data.NoteCoord = Song_NoteCoord.AB;
            else if (APressed && CPressed) data.NoteCoord = Song_NoteCoord.AC;
            else if (BPressed && CPressed) data.NoteCoord = Song_NoteCoord.BC;
            else if (APressed) data.NoteCoord = Song_NoteCoord.A;
            else if (BPressed) data.NoteCoord = Song_NoteCoord.B;
            else if (CPressed) data.NoteCoord = Song_NoteCoord.C;
            

            APressed = false;
            BPressed = false;
            CPressed = false;
            DPressed = false;
            EPressed = false;

            foreach (Activator acti in activators)
            {
                acti.SongInput(data);
            }
        }
    }
}
