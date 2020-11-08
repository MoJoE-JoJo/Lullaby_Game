using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHighlighter : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> segments;
    private InteractableAction linkedAction;

    private Dictionary<GameObject, bool> activeSegments;

    public bool test = true;
    public int indexTest = 0;

    void Start()
    {
        activeSegments = new Dictionary<GameObject, bool>();
        foreach (var item in segments)
        {
            activeSegments.Add(item, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<GameObject, bool> entry in activeSegments)
        {
            entry.Key.SetActive(entry.Value);
        }
        segments[indexTest].SetActive(test);
    }
}
