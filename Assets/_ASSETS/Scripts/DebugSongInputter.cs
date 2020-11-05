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
            /*
            if (APressed && BPressed && CPressed) data.Notes = Song_Note.ABC;
            else if (APressed && BPressed) data.Notes = Song_Note.AB;
            else if (APressed && CPressed) data.Notes = Song_Note.AC;
            else if (BPressed && CPressed) data.Notes = Song_Note.BC;
            else if (APressed) data.Notes = Song_Note.A;
            else if (BPressed) data.Notes = Song_Note.B;
            else if (CPressed) data.Notes = Song_Note.C;
            */

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
