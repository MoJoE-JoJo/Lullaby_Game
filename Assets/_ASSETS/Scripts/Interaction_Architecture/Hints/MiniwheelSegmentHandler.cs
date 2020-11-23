using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MiniwheelSegmentHandler: Hint
{
    [SerializeField] private GameObject A,B,C,D,E,F;
    [SerializeField] private float lowIntensity = 0.5f;
    [SerializeField] private float highIntensity = 1.0f;

    private Dictionary<Song_Note, ObjectWithBool> activeSegments;

    //public bool test = true;
    //public int indexTest = 0;
    private class ObjectWithBool 
    {
        public GameObject GameObject { get; set; }
        public bool Show { get; set; }

        public bool Highlight { get; set; }
    }

    void Start()
    {
        activeSegments = new Dictionary<Song_Note, ObjectWithBool>
        {
            [Song_Note.A] = new ObjectWithBool { Show = false, GameObject = A, Highlight = false},
            [Song_Note.B] = new ObjectWithBool { Show = false, GameObject = B, Highlight = false},
            [Song_Note.C] = new ObjectWithBool { Show = false, GameObject = C, Highlight = false},
            [Song_Note.D] = new ObjectWithBool { Show = false, GameObject = D, Highlight = false},
            [Song_Note.E] = new ObjectWithBool { Show = false, GameObject = E, Highlight = false},
            [Song_Note.F] = new ObjectWithBool { Show = false, GameObject = F, Highlight = false}
        };
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var entry in activeSegments.Values)
        {
            entry.GameObject.SetActive(entry.Show);
            if (entry.Highlight)
            {
                entry.GameObject.GetComponent<Light2D>().intensity = highIntensity;
            }
            else entry.GameObject.GetComponent<Light2D>().intensity = lowIntensity;
        }
    }


    /// <summary>
    ///  Takes a SongData struct and marks the notes to be shown based on that songdata.
    /// </summary>
    public override void ShowNextHint(SongData sd)
    {
        ResetSegments();
        foreach (var songNote in sd.Notes)
        {
            activeSegments[songNote].Show = true;
            activeSegments[songNote].Highlight = false;
        }
    }

    private void ResetSegments()
    {
        foreach (var entry in activeSegments.Values)
        {
            entry.Show = false;
        }
    }

    /// <summary>
    ///  Takes a SongData struct and marks the notes to be shown AND highlighted based on songdata.
    /// </summary>
    public override void HighlightHint(SongData sd)
    {
        ResetSegments();
        foreach (var songNote in sd.Notes)
        {
            activeSegments[songNote].Show = true;
            activeSegments[songNote].Highlight = true;
        }
    }
    public override void NoLightHint(SongData sd)
    {
        foreach (var entry in activeSegments.Values)
        {
                entry.GameObject.GetComponent<Light2D>().intensity = 0.1f;

        }
    }

}
