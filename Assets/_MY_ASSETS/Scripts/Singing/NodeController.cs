using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    private FMOD.Studio.EventInstance Ainstance, Binstance, Cinstance, Dinstance, Ginstance;

    [FMODUnity.EventRef]
    public string AEvent, BEvent, CEvent, DEvent, GEvent;

    public KeyCode SingButton;

    public bool ANode, BNode, CNode, DNode, GNode;

    [SerializeField]
    [Range(0f, 2f)]
    private float End;

    private bool singing;

    private List<FMOD.Studio.EventInstance> eventList;

    // Start is called before the first frame update
    void Start()
    {
        eventList = new List<FMOD.Studio.EventInstance>();
        Ainstance = FMODUnity.RuntimeManager.CreateInstance(AEvent);

        Binstance = FMODUnity.RuntimeManager.CreateInstance(BEvent);

        Cinstance = FMODUnity.RuntimeManager.CreateInstance(CEvent);

        Dinstance = FMODUnity.RuntimeManager.CreateInstance(DEvent);

        Ginstance = FMODUnity.RuntimeManager.CreateInstance(GEvent);

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