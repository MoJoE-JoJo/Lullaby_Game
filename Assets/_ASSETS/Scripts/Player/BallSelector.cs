using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelector : MonoBehaviour
{
    private NoteSelectorNew _noteSelectorNew;
    
    // Start is called before the first frame update
    void Start()
    {
        _noteSelectorNew = transform.parent.gameObject.GetComponent<NoteSelectorNew>();
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
                    _noteSelectorNew.AddImageToFill("A");
                    break;
                case "SegmentB":
                    _noteSelectorNew.AddImageToFill("B");
                    break;
                case "SegmentC":
                    _noteSelectorNew.AddImageToFill("C");
                    break;
                case "SegmentD":
                    _noteSelectorNew.AddImageToFill("D");
                    break;
                case "SegmentE":
                    _noteSelectorNew.AddImageToFill("E");
                    break;
                case "SegmentF":
                    _noteSelectorNew.AddImageToFill("F");
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
                    _noteSelectorNew.AddImageToEmpty("A");
                    break;
                case "SegmentB":
                    _noteSelectorNew.AddImageToEmpty("B");
                    break;
                case "SegmentC":
                    _noteSelectorNew.AddImageToEmpty("C");
                    break;
                case "SegmentD":
                    _noteSelectorNew.AddImageToEmpty("D");
                    break;
                case "SegmentE":
                    _noteSelectorNew.AddImageToEmpty("E");
                    break;
                case "SegmentF":
                    _noteSelectorNew.AddImageToEmpty("F");
                    break;
            }
        }
    }
}
