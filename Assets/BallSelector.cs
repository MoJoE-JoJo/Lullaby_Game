using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelector : MonoBehaviour
{
    private NoteSelector _noteSelector;
    
    // Start is called before the first frame update
    void Start()
    {
        _noteSelector = transform.parent.gameObject.GetComponent<NoteSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ballselectortrigger");
        if (other.gameObject.CompareTag("NoteSelectorSegment"))
        {
            switch (other.gameObject.name)
            {
                case "SegmentA":
                    _noteSelector.AddImageToFill("SegmentA");
                    break;
                case "SegmentB":
                    _noteSelector.AddImageToFill("SegmentB");
                    break;
                case "SegmentC":
                    _noteSelector.AddImageToFill("SegmentC");
                    break;
                case "SegmentD":
                    _noteSelector.AddImageToFill("SegmentD");
                    break;
                case "SegmentE":
                    _noteSelector.AddImageToFill("SegmentE");
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NoteSelectorSegment"))
        {
            switch (other.gameObject.name)
            {
                case "SegmentA":
                    _noteSelector.AddImageToEmpty("SegmentA");
                    break;
                case "SegmentB":
                    _noteSelector.AddImageToEmpty("SegmentB");
                    break;
                case "SegmentC":
                    _noteSelector.AddImageToEmpty("SegmentC");
                    break;
                case "SegmentD":
                    _noteSelector.AddImageToEmpty("SegmentD");
                    break;
                case "SegmentE":
                    _noteSelector.AddImageToEmpty("SegmentE");
                    break;
            }
        }
    }
}
