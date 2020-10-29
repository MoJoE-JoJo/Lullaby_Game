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
                    _noteSelector.AddImageToFill("A");
                    break;
                case "SegmentB":
                    _noteSelector.AddImageToFill("B");
                    break;
                case "SegmentC":
                    _noteSelector.AddImageToFill("C");
                    break;
                case "SegmentD":
                    _noteSelector.AddImageToFill("D");
                    break;
                case "SegmentE":
                    _noteSelector.AddImageToFill("E");
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
                    _noteSelector.AddImageToEmpty("A");
                    break;
                case "SegmentB":
                    _noteSelector.AddImageToEmpty("B");
                    break;
                case "SegmentC":
                    _noteSelector.AddImageToEmpty("C");
                    break;
                case "SegmentD":
                    _noteSelector.AddImageToEmpty("D");
                    break;
                case "SegmentE":
                    _noteSelector.AddImageToEmpty("E");
                    break;
            }
        }
    }
}
