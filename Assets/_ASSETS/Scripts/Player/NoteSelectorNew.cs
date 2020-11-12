using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSelectorNew : MonoBehaviour
{
    [SerializeField] private bool fillWheel;
    
    private PlayerControls _controls;
    private PlayerController _playerController;
    private bool _startedSinging;

    [SerializeField] private float fillRatio;
    [SerializeField] private float fillCompletedThreshold;

    //the collider of the selector ball
    private CircleCollider2D _circleCollider2D;

    [SerializeField] private float startingImageFill;
    private Image fillA;
    private Image fillB;
    private Image fillC;
    private Image fillD;
    private Image fillE;
    private Image fillF;

    private Image backgroundA;
    private Image backgroundB;
    private Image backgroundC;
    private Image backgroundD;
    private Image backgroundE;
    private Image backgroundF;
    
    private GameObject _segmentA;
    private GameObject _segmentB;
    private GameObject _segmentC;
    private GameObject _segmentD;
    private GameObject _segmentE;
    private GameObject _segmentF;

    [SerializeField] private float lockOffsetAmount;

    private Dictionary<String, Image> _backgrounds = new Dictionary<string, Image>();

    [SerializeField] private float initialBackgroundAlpha;
    [SerializeField] private float hoverBackgroundAlpha;

    private GameObject selector;

    private Dictionary<String, Image> _imagesToFill = new Dictionary<string, Image>();
    private Dictionary<String, Image> _imagesToEmpty = new Dictionary<string, Image>();
    private Dictionary<String, Image> _imagesLocked = new Dictionary<string, Image>();
    private Dictionary<String, GameObject> _segments = new Dictionary<String, GameObject>();

    private SongData _currentSong; //check if anySongPlaying before giving the currentSong to an Activator
    private String _currentSongString = "";
    private bool _anySongPlaying;

    private Vector2 _selectorMove;

    private bool _singButtonDown;
    private float _singVolume;
    
    void Awake()
    {
        _controls = new PlayerControls();

        _controls.NoteSelector.Move.performed += context => _selectorMove = context.ReadValue<Vector2>();
        _controls.NoteSelector.Move.canceled += context => _selectorMove = Vector2.zero;

        _controls.NoteSelector.Sing.started += context => SingPressed();
        _controls.NoteSelector.Sing.performed += context => _singVolume = context.ReadValue<float>();
        _controls.NoteSelector.Sing.canceled += context => SingReleased();

        _controls.NoteSelector.LockNote.started += context => LockUnlockNote();

        _controls.NoteSelector.SwitchWheelType.performed += context => fillWheel = (!fillWheel);
    }

    void Start()
    {
        _segmentA = this.transform.Find("SegmentA").gameObject;
        _segmentB = this.transform.Find("SegmentB").gameObject;
        _segmentC = this.transform.Find("SegmentC").gameObject;
        _segmentD = this.transform.Find("SegmentD").gameObject;
        _segmentE = this.transform.Find("SegmentE").gameObject;
        _segmentF = this.transform.Find("SegmentF").gameObject;

        backgroundA = _segmentA.transform.Find("Background").GetComponent<Image>();
        backgroundB = _segmentB.transform.Find("Background").GetComponent<Image>();
        backgroundC = _segmentC.transform.Find("Background").GetComponent<Image>();
        backgroundD = _segmentD.transform.Find("Background").GetComponent<Image>();
        backgroundE = _segmentE.transform.Find("Background").GetComponent<Image>();
        backgroundF = _segmentF.transform.Find("Background").GetComponent<Image>();
        
        fillA = _segmentA.transform.Find("Filling").GetComponent<Image>();
        fillB = _segmentB.transform.Find("Filling").GetComponent<Image>();
        fillC = _segmentC.transform.Find("Filling").GetComponent<Image>();
        fillD = _segmentD.transform.Find("Filling").GetComponent<Image>();
        fillE = _segmentE.transform.Find("Filling").GetComponent<Image>();
        fillF = _segmentF.transform.Find("Filling").GetComponent<Image>();

        selector = this.transform.Find("Selector").gameObject;
        
        //fill background images
        _backgrounds.Add("A", backgroundA);
        _backgrounds.Add("B", backgroundB);
        _backgrounds.Add("C", backgroundC);
        _backgrounds.Add("D", backgroundD);
        _backgrounds.Add("E", backgroundE);
        _backgrounds.Add("F", backgroundF);
        
        //fill segments
        _segments.Add("A", _segmentA);
        _segments.Add("B", _segmentB);
        _segments.Add("C", _segmentC);
        _segments.Add("D", _segmentD);
        _segments.Add("E", _segmentE);
        _segments.Add("F", _segmentF);

        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        CenterNoteSelector();
        MoveSelector();
        if (fillWheel)
        {
            FillNotes();
        }
        else
        {
            FillNotes2();    
        }
        EmptyNotes();
        GetNote();
        UpdatePlayerNote();
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
        foreach (var entry in _imagesLocked)
        {
            _segments[entry.Key].transform.localPosition /= lockOffsetAmount;
        }
        _imagesLocked = new Dictionary<string, Image>();
        _imagesToFill = new Dictionary<string, Image>();
        _imagesToEmpty = new Dictionary<string, Image>();
        _playerController.IsSinging = false;
    }

    public void CenterNoteSelector()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(_playerController.transform.position);
    }

    void FillNotes()
    {
        //first fill (or empty images locked)
        foreach (KeyValuePair<String,Image> entry in _imagesLocked)
        {
            var tempColor = _backgrounds[entry.Key].color;
            tempColor.a = hoverBackgroundAlpha;
            _backgrounds[entry.Key].color = tempColor;

            float currentImgFill = entry.Value.fillAmount;

            if (_singButtonDown)
            {
                entry.Value.fillAmount = Mathf.Lerp(currentImgFill, _singVolume, fillRatio);
                if(!_currentSongString.Contains(entry.Key))
                {
                    _currentSongString += entry.Key;
                }
            }
            else
            {
                if (currentImgFill > startingImageFill)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, startingImageFill, fillRatio);
                }

                _currentSongString = "";
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

                if (_singButtonDown)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, _singVolume, fillRatio);
                    if(!_currentSongString.Contains(entry.Key))
                    {
                        _currentSongString += entry.Key;
                    }
                }
                else
                {
                    if (currentImgFill > startingImageFill)
                    {
                        entry.Value.fillAmount = Mathf.Lerp(currentImgFill, startingImageFill, fillRatio);
                    }

                    _currentSongString = "";
                }
            }
        }
        
    }
    
    void FillNotes2()
    {
        //first fill (or empty images locked)
        foreach (KeyValuePair<String,Image> entry in _imagesLocked)
        {
            var tempColor = _backgrounds[entry.Key].color;

            float currentAlpha = tempColor.a;
            float futureColor;
            
            if (_singButtonDown)
            {
                futureColor = initialBackgroundAlpha + (1 - initialBackgroundAlpha) * _singVolume;
                tempColor.a = futureColor;
                _backgrounds[entry.Key].color = tempColor;
                
                if(!_currentSongString.Contains(entry.Key))
                {
                    _currentSongString += entry.Key;
                }
            }
            else
            {
                if (currentAlpha > initialBackgroundAlpha)
                {
                    futureColor = Mathf.Lerp(currentAlpha, initialBackgroundAlpha, fillRatio);
                    tempColor.a = futureColor;
                    _backgrounds[entry.Key].color = tempColor;
                }
                _currentSongString = "";
            }
        }
        
        //then if there are images selected that are not locked, fill or empty them
        foreach (KeyValuePair<String,Image> entry in _imagesToFill)
        {
            var tempColor = _backgrounds[entry.Key].color;

            float currentAlpha = tempColor.a;
            float futureColor;

            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                if (_singButtonDown)
                {
                    futureColor = initialBackgroundAlpha + (1 - initialBackgroundAlpha) * _singVolume;
                    tempColor.a = futureColor;
                    _backgrounds[entry.Key].color = tempColor;

                    if (!_currentSongString.Contains(entry.Key))
                    {
                        _currentSongString += entry.Key;
                    }
                }
                else
                {
                    if (currentAlpha > initialBackgroundAlpha)
                    {
                        futureColor = Mathf.Lerp(currentAlpha, initialBackgroundAlpha, fillRatio);
                        tempColor.a = futureColor;
                        _backgrounds[entry.Key].color = tempColor;
                    }

                    _currentSongString = "";
                }
            }
        }
        
    }

    void EmptyNotes()
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
                if (currentImgFill < fillCompletedThreshold)
                {
                    _currentSongString = _currentSongString.Replace(entry.Key, "");
                }
            }
        }
    }

    void MoveSelector()
    {
        Vector2 m = new Vector2(_selectorMove.x, _selectorMove.y);
        selector.transform.localPosition = _selectorMove*65;
    }
    
    public void AddImageToFill(String segment)
    {
        Debug.Log("Added image to fill");
        if (!_imagesToFill.ContainsKey(segment))
        {
            Image selected = null;
            
            switch (segment)
            {
                case "A":
                    selected = fillA;
                    break;
                case "B":
                    selected = fillB;
                    break;
                case "C":
                    selected = fillC;
                    break;
                case "D":
                    selected = fillD;
                    break;
                case "E":
                    selected = fillE;
                    break;
                case "F":
                    selected = fillF;
                    break;;
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
                case "A":
                    selected = fillA;
                    break;
                case "B":
                    selected = fillB;
                    break;
                case "C":
                    selected = fillC;
                    break;
                case "D":
                    selected = fillD;
                    break;
                case "E":
                    selected = fillE;
                    break;
                case "F":
                    selected = fillF;
                    break;
            }
            
            _imagesToEmpty.Add(segment, selected);
        }

        if (_imagesToFill.ContainsKey(segment))
        {
            _imagesToFill.Remove(segment);
        }
    }

    private void SingPressed()
    {
        _singButtonDown = true;
    }
    private void SingReleased()
    {
        _singButtonDown = false;
    }

    private void LockUnlockNote()
    {
        foreach (KeyValuePair<String,Image> entry in _imagesToFill)
        {
            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                _imagesLocked.Add(entry.Key, entry.Value);
                _segments[entry.Key].transform.localPosition *= lockOffsetAmount;
            }
            else
            {
                _imagesLocked.Remove(entry.Key);
                _segments[entry.Key].transform.localPosition /= lockOffsetAmount;
            }
            
        }
    }

    private SongData GetNote()
    {
        SongData songData = new SongData();
        
        if (_currentSongString != "")
        {
            _anySongPlaying = true;
            _currentSong = new SongData(_currentSongString);
            _currentSong.Volume = _singVolume;
            //Debug.Log("Current song volume: " + _currentSong.Volume);
        }
        else
        {
            _anySongPlaying = false;
            _currentSong = new SongData();
            _currentSong.Volume = 0.0f;
        }

        return songData;
        //Debug.Log("Current string: " + _currentSongString);
        //Debug.Log("Current enum: " + _currentSong.NoteCoord.ToString());
    }

    void UpdatePlayerNote()
    {
        _playerController.SongBeingSung = _currentSong;
        _playerController.IsSinging = _anySongPlaying;
    }

}
