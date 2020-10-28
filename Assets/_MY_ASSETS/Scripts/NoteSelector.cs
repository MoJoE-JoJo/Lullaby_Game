using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSelector : MonoBehaviour
{
    private PlayerControls _controls;
    
    public GameObject player;

    public float fillRatio;

    //the collider of the selector ball
    private CircleCollider2D _circleCollider2D;

    [SerializeField] private float startingImageFill = 0.7f;
    public Image fillA;
    public Image fillB;
    public Image fillC;
    public Image fillD;
    public Image fillE;

    public Image backgroundA;
    public Image backgroundB;
    public Image backgroundC;
    public Image backgroundD;
    public Image backgroundE;

    private Dictionary<String, Image> _backgrounds = new Dictionary<string, Image>();

    public float initialBackgroundAlpha;
    public float hoverBackgroundAlpha;

    public GameObject selector;

    private Dictionary<String, Image> _imagesToFill = new Dictionary<string, Image>();
    private Dictionary<String, Image> _imagesToEmpty = new Dictionary<string, Image>();
    private Dictionary<String, Image> _imagesLocked = new Dictionary<string, Image>();

    private Vector2 _selectorMove;

    private bool _rightTriggerDown = false;
    
    void Awake()
    {
        _controls = new PlayerControls();

        _controls.NoteSelector.Move.performed += context => _selectorMove = context.ReadValue<Vector2>();
        _controls.NoteSelector.Move.canceled += context => _selectorMove = Vector2.zero;

        _controls.NoteSelector.Sing.started += context => RTpressed();
        _controls.NoteSelector.Sing.canceled += context => RTreleased();

        _controls.NoteSelector.LockNote.performed += context => LockUnlockNote();
    }

    void Start()
    {
        //fill background images
        _backgrounds.Add("SegmentA", backgroundA);
        _backgrounds.Add("SegmentB", backgroundB);
        _backgrounds.Add("SegmentC", backgroundC);
        _backgrounds.Add("SegmentD", backgroundD);
        _backgrounds.Add("SegmentE", backgroundE);
    }

    void Update()
    {
        CenterNoteSelector();
        MoveSelector();
        FillImages();
        EmptyImages();
    }

    public void CenterNoteSelector()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(player.transform.localPosition);
    }

    void FillImages()
    {
        //first fill (or empty images locked)
        foreach (KeyValuePair<String,Image> entry in _imagesLocked)
        {
            var tempColor = _backgrounds[entry.Key].color;
            tempColor.a = hoverBackgroundAlpha;
            _backgrounds[entry.Key].color = tempColor;

            float currentImgFill = entry.Value.fillAmount;

            if (_rightTriggerDown)
            {
                if (currentImgFill < 1.0f)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, 1.0f, fillRatio);
                }
            }
            else
            {
                if (currentImgFill > startingImageFill)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, startingImageFill, fillRatio);
                }
            }
        }
        
        //then if there are images selected that are not locked, fill or empty them
        foreach (KeyValuePair<String,Image> entry in _imagesToFill)
        {
            var tempColor = _backgrounds[entry.Key].color;
            tempColor.a = hoverBackgroundAlpha;
            _backgrounds[entry.Key].color = tempColor;

            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                float currentImgFill = entry.Value.fillAmount;

                if (_rightTriggerDown)
                {
                    if (currentImgFill < 1.0f)
                    {
                        entry.Value.fillAmount = Mathf.Lerp(currentImgFill, 1.0f, fillRatio);
                    }
                }
                else
                {
                    if (currentImgFill > startingImageFill)
                    {
                        entry.Value.fillAmount = Mathf.Lerp(currentImgFill, startingImageFill, fillRatio);
                    }
                }
            }
        }
        
    }

    void EmptyImages()
    {
        foreach (KeyValuePair<String,Image> entry in _imagesToEmpty)
        {
            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                var tempColor = _backgrounds[entry.Key].color;
                tempColor.a = initialBackgroundAlpha;
                _backgrounds[entry.Key].color = tempColor;
            
                float currentImgFill = entry.Value.fillAmount;
                if (currentImgFill > startingImageFill)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, startingImageFill, fillRatio);
                }
            }
        }
    }

    void MoveSelector()
    {
        Vector2 m = new Vector2(_selectorMove.x, _selectorMove.y);
        selector.transform.localPosition = -_selectorMove*35;
    }
    
    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
        fillA.fillAmount = startingImageFill;
        fillB.fillAmount = startingImageFill;
        fillC.fillAmount = startingImageFill;
        fillD.fillAmount = startingImageFill;
        fillE.fillAmount = startingImageFill;
        _imagesLocked = new Dictionary<string, Image>();
        _imagesToFill = new Dictionary<string, Image>();
        _imagesToEmpty = new Dictionary<string, Image>();
    }

    public void AddImageToFill(String segment)
    {
        if (!_imagesToFill.ContainsKey(segment))
        {
            Image selected = null;
            
            switch (segment)
            {
                case "SegmentA":
                    selected = fillA;
                    break;
                case "SegmentB":
                    selected = fillB;
                    break;
                case "SegmentC":
                    selected = fillC;
                    break;
                case "SegmentD":
                    selected = fillD;
                    break;
                case "SegmentE":
                    selected = fillE;
                    break;
            }
            
            _imagesToFill.Add(segment, selected);
        }

        if (_imagesToEmpty.ContainsKey(segment))
        {
            _imagesToEmpty.Remove(segment);
        }
    }
    
    public void AddImageToEmpty(String segment)
    {
        if (!_imagesToEmpty.ContainsKey(segment))
        {
            Image selected = null;
            
            switch (segment)
            {
                case "SegmentA":
                    selected = fillA;
                    break;
                case "SegmentB":
                    selected = fillB;
                    break;
                case "SegmentC":
                    selected = fillC;
                    break;
                case "SegmentD":
                    selected = fillD;
                    break;
                case "SegmentE":
                    selected = fillE;
                    break;
            }
            
            _imagesToEmpty.Add(segment, selected);
        }

        if (_imagesToFill.ContainsKey(segment))
        {
            _imagesToFill.Remove(segment);
            Debug.Log("Removed " + segment);
        }
    }

    private void RTpressed()
    {
        _rightTriggerDown = true;
    }
    private void RTreleased()
    {
        _rightTriggerDown = false;
    }

    private void LockUnlockNote()
    {
        foreach (KeyValuePair<String,Image> entry in _imagesToFill)
        {
            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                _imagesLocked.Add(entry.Key, entry.Value);
            }
            else
            {
                _imagesLocked.Remove(entry.Key);
            }
            
        }
    }
    
}
