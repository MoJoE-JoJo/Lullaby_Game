using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSelectorNew : MonoBehaviour
{
    [SerializeField] private bool fillWheel; //rotates between our 2 implemented wheel modes;

    private Camera _mainCamera;

    private PlayerControls _controls;
    private PlayerController _playerController;
    private bool _startedSinging;

    [SerializeField] private float fillRatio;
    [SerializeField] private float fillCompletedThreshold;

    //the collider of the selector ball
    private CircleCollider2D _circleCollider2D;

    [SerializeField] private float startingImageFill;
    private Image _fillA;
    private Image _fillB;
    private Image _fillC;
    private Image _fillD;
    private Image _fillE;
    private Image _fillF;

    private Image _backgroundA;
    private Image _backgroundB;
    private Image _backgroundC;
    private Image _backgroundD;
    private Image _backgroundE;
    private Image _backgroundF;

    private GameObject _segmentA;
    private GameObject _segmentB;
    private GameObject _segmentC;
    private GameObject _segmentD;
    private GameObject _segmentE;
    private GameObject _segmentF;

    private GameObject _maskA;
    private GameObject _maskB;
    private GameObject _maskC;
    private GameObject _maskD;
    private GameObject _maskE;
    private GameObject _maskF;


    [SerializeField] private float lockOffsetAmount;

    private Dictionary<string, Image> _backgrounds = new Dictionary<string, Image>();
    private Dictionary<string, Image> _fills = new Dictionary<string, Image>();
    private Vector3 _initialPosition = new Vector3(0, -88, 1);

    [SerializeField] private float initialBackgroundAlpha;
    [SerializeField] private float hoverBackgroundAlpha;

    private GameObject _selector;

    private Dictionary<string, Image> _imagesToFill = new Dictionary<string, Image>();
    private Dictionary<string, Image> _imagesToEmpty = new Dictionary<string, Image>();
    private Dictionary<string, Image> _imagesLocked = new Dictionary<string, Image>();
    private Dictionary<string, GameObject> _segments = new Dictionary<string, GameObject>();

    private SongData _currentSong; //check if anySongPlaying before giving the currentSong to an Activator
    private string _currentSongString = "";
    private SongData _lastSong;
    private bool _anySongPlaying;

    private Vector2 _selectorMove;

    private bool _singButtonDown;
    private float _singVolume;

    void Awake()
    {
        _currentSong = new SongData("A")
        {
            Volume = 1f
        };

        _lastSong = _currentSong;

        SetupControls();

        _segmentA = this.transform.Find("SegmentA").gameObject;
        _segmentB = this.transform.Find("SegmentB").gameObject;
        _segmentC = this.transform.Find("SegmentC").gameObject;
        _segmentD = this.transform.Find("SegmentD").gameObject;
        _segmentE = this.transform.Find("SegmentE").gameObject;
        _segmentF = this.transform.Find("SegmentF").gameObject;

        _maskA = _segmentA.transform.Find("Mask").gameObject;
        _maskB = _segmentB.transform.Find("Mask").gameObject;
        _maskC = _segmentC.transform.Find("Mask").gameObject;
        _maskD = _segmentD.transform.Find("Mask").gameObject;
        _maskE = _segmentE.transform.Find("Mask").gameObject;
        _maskF = _segmentF.transform.Find("Mask").gameObject;

        _backgroundA = _segmentA.transform.Find("Background").GetComponent<Image>();
        _backgroundB = _segmentB.transform.Find("Background").GetComponent<Image>();
        _backgroundC = _segmentC.transform.Find("Background").GetComponent<Image>();
        _backgroundD = _segmentD.transform.Find("Background").GetComponent<Image>();
        _backgroundE = _segmentE.transform.Find("Background").GetComponent<Image>();
        _backgroundF = _segmentF.transform.Find("Background").GetComponent<Image>();

        _fillA = _maskA.transform.Find("Filling").GetComponent<Image>();
        _fillB = _maskB.transform.Find("Filling").GetComponent<Image>();
        _fillC = _maskC.transform.Find("Filling").GetComponent<Image>();
        _fillD = _maskD.transform.Find("Filling").GetComponent<Image>();
        _fillE = _maskE.transform.Find("Filling").GetComponent<Image>();
        _fillF = _maskF.transform.Find("Filling").GetComponent<Image>();

        _selector = this.transform.Find("Selector").gameObject;

        //fill background images
        _backgrounds.Add("A", _backgroundA);
        _backgrounds.Add("B", _backgroundB);
        _backgrounds.Add("C", _backgroundC);
        _backgrounds.Add("D", _backgroundD);
        _backgrounds.Add("E", _backgroundE);
        _backgrounds.Add("F", _backgroundF);


        //fill segments
        _segments.Add("A", _segmentA);
        _segments.Add("B", _segmentB);
        _segments.Add("C", _segmentC);
        _segments.Add("D", _segmentD);
        _segments.Add("E", _segmentE);
        _segments.Add("F", _segmentF);

        //fill fills
        _fills.Add("A", _fillA);
        _fills.Add("B", _fillB);
        _fills.Add("C", _fillC);
        _fills.Add("D", _fillD);
        _fills.Add("E", _fillE);
        _fills.Add("F", _fillF);

        _mainCamera = Camera.main;
    }

    void Start()
    {

    }

    private bool wasSingButtonDown = false;

    void Update()
    {
        var gm = GameManager.Instance;
        if (_singButtonDown)
        {
            gm.puzzleCompletion.time_singing += Time.deltaTime;
            if (gm.puzzleCompletion.puzzle_name == "")
            {
                //update time outside puzzle
                gm.overallstats.total_time_singing_outside_puzzle += Time.deltaTime;
            }
            //update time doing puzzle
            else gm.overallstats.total_time_singing_doing_puzzle += Time.deltaTime;
        }

        if (!wasSingButtonDown && _singButtonDown)
        {
            if (gm.puzzleCompletion.puzzle_name == "")
            {
                //update activation time
                gm.overallstats.note_activation_outside_puzzle += 1;
            }
            //update time doing puzzle
            else gm.overallstats.note_activation_in_puzzle += 1;
        }

        wasSingButtonDown = _singButtonDown;

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
        _currentSongString = "";
        _currentSong = new SongData();

        _controls.Enable();
    }

    private void OnDisable()
    {
        SingReleased();
        //_anySongPlaying = false;
        //_playerController.IsSinging = false;
        _playerController.SongBeingSung = _lastSong;
        //UpdatePlayerNote();
        _controls.Disable();
        foreach (var fill in _fills)
        {
            fill.Value.gameObject.transform.localPosition = _initialPosition;
        }

        foreach (var background in _backgrounds)
        {
            var tempColor = _backgrounds[background.Key].color;
            tempColor.a = initialBackgroundAlpha;
            _backgrounds[background.Key].color = tempColor;
        }

        foreach (var entry in _imagesLocked)
        {
            //_segments[entry.Key].transform.localPosition /= lockOffsetAmount;
        }
        //_imagesLocked = new Dictionary<string, Image>();
        _imagesToFill = new Dictionary<string, Image>();
        _imagesToEmpty = new Dictionary<string, Image>();
    }

    public void CenterNoteSelector()
    {
        this.transform.position = _mainCamera.WorldToScreenPoint(_playerController.gameObject.transform.position);
    }

    //first method for filling the wheel, from the bottom up
    void FillNotes()
    {
        //first fill (or empty images locked)
        foreach (KeyValuePair<String, Image> entry in _imagesLocked)
        {
            //increase background alpha
            var tempColor = _backgrounds[entry.Key].color;
            tempColor.a = hoverBackgroundAlpha;
            _backgrounds[entry.Key].color = tempColor;

            float currentImgFill = entry.Value.fillAmount;

            //if is singing, fill wheel
            if (_singButtonDown)
            {
                entry.Value.gameObject.transform.localPosition = new Vector3(
                    0,
                    Mathf.Lerp(entry.Value.gameObject.transform.localPosition.y, _initialPosition.y - (_initialPosition.y * _singVolume), fillRatio));
                if (!_currentSongString.Contains(entry.Key))
                {
                    _currentSongString += entry.Key;
                }
            }
            else //else empty it
            {
                entry.Value.gameObject.transform.localPosition = new Vector3(
                    0, Mathf.Lerp(entry.Value.gameObject.transform.localPosition.y, _initialPosition.y, fillRatio));
            }
        }

        //then if there are images selected that are not locked, fill or empty them
        foreach (KeyValuePair<String, Image> entry in _imagesToFill)
        {
            var tempColor = _backgrounds[entry.Key].color;
            tempColor.a = hoverBackgroundAlpha;
            _backgrounds[entry.Key].color = tempColor;

            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                float currentImgFill = entry.Value.fillAmount;

                if (_singButtonDown)
                {
                    entry.Value.gameObject.transform.localPosition = new Vector3(
                        0,
                        Mathf.Lerp(entry.Value.gameObject.transform.localPosition.y, _initialPosition.y - (_initialPosition.y * _singVolume), fillRatio));
                    if (!_currentSongString.Contains(entry.Key))
                    {
                        _currentSongString += entry.Key;
                    }
                }
                else
                {
                    entry.Value.gameObject.transform.localPosition = new Vector3(
                            0, Mathf.Lerp(entry.Value.gameObject.transform.localPosition.y, _initialPosition.y, fillRatio));
                    _currentSongString = _currentSongString.Replace(entry.Key, "");
                }
            }
        }

    }

    //method for filling the wheel with alpha color
    void FillNotes2()
    {
        //first fill (or empty images locked)
        foreach (KeyValuePair<String, Image> entry in _imagesLocked)
        {
            var tempColor = _backgrounds[entry.Key].color;

            float currentAlpha = tempColor.a;
            float futureColor;

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

        //then if there are images selected that are not locked, fill or empty them
        foreach (KeyValuePair<String, Image> entry in _imagesToFill)
        {
            var tempColor = _backgrounds[entry.Key].color;

            float currentAlpha = tempColor.a;

            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                float futureColor;
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

    void EmptyNotes() //only relevant if first FillWheel method is used
    {
        foreach (KeyValuePair<String, Image> entry in _imagesToEmpty)
        {
            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                var tempColor = _backgrounds[entry.Key].color;
                tempColor.a = initialBackgroundAlpha;
                _backgrounds[entry.Key].color = tempColor;
                float currentImgFill = entry.Value.fillAmount;
                float currentYpositionFill = entry.Value.gameObject.transform.localPosition.y;

                if (Mathf.Abs(currentYpositionFill) > startingImageFill)
                {
                    entry.Value.gameObject.transform.localPosition = new Vector3(
                        0, Mathf.Lerp(entry.Value.gameObject.transform.localPosition.y, _initialPosition.y, fillRatio));
                    currentYpositionFill = entry.Value.gameObject.transform.localPosition.y;
                }

                if (currentYpositionFill < (1 - fillCompletedThreshold) * _initialPosition.y)
                {
                    _currentSongString = _currentSongString.Replace(entry.Key, "");
                }
            }
        }
    }

    //move the wheel selector (the one you control with joystick)
    private void MoveSelector()
    {
        _selector.transform.localPosition = _selectorMove * 65;
    }

    public void AddImageToFill(String segment)
    {
        //Debug.Log("Added image to fill");
        if (!_imagesToFill.ContainsKey(segment))
        {
            Image selected = null;

            switch (segment)
            {
                case "A":
                    selected = _fillA;
                    break;
                case "B":
                    selected = _fillB;
                    break;
                case "C":
                    selected = _fillC;
                    break;
                case "D":
                    selected = _fillD;
                    break;
                case "E":
                    selected = _fillE;
                    break;
                case "F":
                    selected = _fillF;
                    break;
            }

            _imagesToFill.Add(segment, selected);
        }

        if (_imagesToEmpty.ContainsKey(segment))
        {
            _imagesToEmpty.Remove(segment);
        }
    }

    public void AddImageToEmpty(String segment) //called from a different script
    {
        if (!_imagesToEmpty.ContainsKey(segment))
        {
            Image selected = null;

            switch (segment)
            {
                case "A":
                    selected = _fillA;
                    break;
                case "B":
                    selected = _fillB;
                    break;
                case "C":
                    selected = _fillC;
                    break;
                case "D":
                    selected = _fillD;
                    break;
                case "E":
                    selected = _fillE;
                    break;
                case "F":
                    selected = _fillF;
                    break;
            }

            _imagesToEmpty.Add(segment, selected);
        }

        if (_imagesToFill.ContainsKey(segment))
        {
            _imagesToFill.Remove(segment);
        }
    }

    //control methods
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
        foreach (KeyValuePair<String, Image> entry in _imagesToFill)
        {
            if (!_imagesLocked.ContainsKey(entry.Key))
            {
                //note locked in
                GameManager.Instance.puzzleCompletion.note_lock_count += 1;
                _imagesLocked.Add(entry.Key, entry.Value);
                _segments[entry.Key].transform.localPosition *= lockOffsetAmount;
                if (!_currentSongString.Contains(entry.Key))
                {
                    _currentSongString += entry.Key;
                }
            }
            else
            {
                //note unlocked
                GameManager.Instance.puzzleCompletion.note_unlock_count += 1;
                _imagesLocked.Remove(entry.Key);
                _segments[entry.Key].transform.localPosition /= lockOffsetAmount;
            }

        }
    }

    private void GetNote() //get current SongData with the wheel selections currently in place
    {
        if (_currentSongString != "")
        {
            _anySongPlaying = true;
            _currentSong = new SongData(_currentSongString) { Volume = _singVolume };
            _lastSong = _currentSong;
            //Debug.Log("Current song volume: " + _currentSong.Volume);
        }

        else
        {
            _anySongPlaying = false;
            _currentSong = new SongData { };
        }
        //Debug.Log("Current string: " + _currentSongString);
        //Debug.Log("Current enum: " + _currentSong.NoteCoord.ToString());
    }

    void UpdatePlayerNote() //give the Song information to the player script
    {
        if (_anySongPlaying)
        {
            _playerController.SongBeingSung = _currentSong;
        }
        //_playerController.IsSinging = _anySongPlaying;
    }

    void SetupControls() //method to setup the controls through the new input system
    {
        _controls = new PlayerControls();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _controls.NoteSelector.Move.performed += context => _selectorMove = context.ReadValue<Vector2>();
        _controls.NoteSelector.Move.canceled += context => _selectorMove = Vector2.zero;

        _controls.NoteSelector.Sing.started += context => SingPressed();
        _controls.NoteSelector.Sing.performed += context => EaseVolume(context.ReadValue<float>());
        _controls.NoteSelector.Sing.canceled += context => SingReleased();

        _controls.NoteSelector.LockNote.started += context => LockUnlockNote();

        _controls.NoteSelector.SwitchWheelType.performed += context => fillWheel = (!fillWheel);
    }


    void EaseVolume(float volume)
    {
        _singVolume = 1.1f - Mathf.Pow(1 - volume, 3);
        if (_singVolume >= 1.0f)
        {
            _singVolume = 1.0f;
        }
    }

}
