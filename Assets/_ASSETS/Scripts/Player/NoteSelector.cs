using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSelector : MonoBehaviour
{
    private PlayerControls _controls;
    public NoteController noteController;
    public GameObject player;
    private PlayerController _playerController;
    private bool _startedSinging;

    public float fillRatio;
    public float fillCompletedThreshold;

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
    }

    void Start()
    {
        //fill background images
        _backgrounds.Add("A", backgroundA);
        _backgrounds.Add("B", backgroundB);
        _backgrounds.Add("C", backgroundC);
        _backgrounds.Add("D", backgroundD);
        _backgrounds.Add("E", backgroundE);

        _playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        CenterNoteSelector();
        MoveSelector();
        FillNotes();
        EmptyNotes();
        GetNote();
        UpdatePlayerNote();
    }

    public void CenterNoteSelector()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(player.transform.position);
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
                if (currentImgFill < 1.0f)
                {
                    entry.Value.fillAmount = Mathf.Lerp(currentImgFill, 1.0f, fillRatio);
                    
                    if(currentImgFill > fillCompletedThreshold && !_currentSongString.Contains(entry.Key))
                    {
                        _currentSongString += entry.Key;
                    }
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
                    if (currentImgFill < 1.0f)
                    {
                        entry.Value.fillAmount = Mathf.Lerp(currentImgFill, 1.0f, fillRatio);
                        
                        if(currentImgFill > fillCompletedThreshold && !_currentSongString.Contains(entry.Key))
                        {
                            _currentSongString += entry.Key;
                        }
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
        _playerController.IsSinging = false;
    }

    public void AddImageToFill(String segment)
    {
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
            }
            else
            {
                _imagesLocked.Remove(entry.Key);
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
