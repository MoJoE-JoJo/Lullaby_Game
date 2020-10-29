using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class NoteController : MonoBehaviour
{
    private EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance;

    [EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent;

    public KeyCode SingButton;

    public bool ANode, BNode, CNode, DNode, GNode;

    [SerializeField]
    [Range(0f, 2f)]
    private float End;

    private bool singing;

    private List<EventInstance> eventList;

    // Start is called before the first frame update
    void Start()
    {
        eventList = new List<EventInstance>();
        Ainstance = RuntimeManager.CreateInstance(AEvent);

        Binstance = RuntimeManager.CreateInstance(BEvent);

        Cinstance = RuntimeManager.CreateInstance(CEvent);

        Dinstance = RuntimeManager.CreateInstance(DEvent);

        Ginstance = RuntimeManager.CreateInstance(GEvent);

        eventList.Add(Ainstance);
        eventList.Add(Binstance);
        eventList.Add(Cinstance);
        eventList.Add(Dinstance);
        eventList.Add(Ginstance);

        singing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!singing && Input.GetKeyDown(SingButton))
        {
            singing = true;
            if (ANode) Ainstance.start();
            if (BNode) Binstance.start();
            if (CNode) Cinstance.start();
            if (DNode) Dinstance.start();
            if (GNode) Ginstance.start();
        }
        if(singing && Input.GetKeyUp(SingButton))
        {
            foreach (var item in eventList)
            {
                item.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            singing = false;
        }
    }
}