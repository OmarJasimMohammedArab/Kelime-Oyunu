using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private AlphabetData.LetterData _normalLetterData;
    private AlphabetData.LetterData _selectedLetterData;
    private AlphabetData.LetterData _correctletterData;

    private SpriteRenderer _disPlayImage;

    private bool _selected;
    private bool _clicked;
    private bool _currect;
    private int _index = -1;

    private AudioSource _source;

    public void SetIndex(int index)
    {
        _index = index;
    }

    public int GetIndex()
    {
        return _index;
    }

    void Start()
    {
        _selected = false;
        _clicked = false;
        _currect = false;
        _disPlayImage = GetComponent<SpriteRenderer>();

        _source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameEvents.OnEnabelSquareSelection += OnEnabelSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += OnSelectSquare;
        GameEvents.OnCorrectWord += CorrectWord;
    }
    private void OnDisable()
    {
        GameEvents.OnEnabelSquareSelection -= OnEnabelSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= OnSelectSquare;
        GameEvents.OnCorrectWord -= CorrectWord;

    }

    private void CorrectWord(string word, List<int> squareIndxes)
    {
        if(_selected && squareIndxes.Contains(_index))
        {
            _currect = true;
            _disPlayImage.sprite = _correctletterData.image;
        }
        _selected = false;
        _clicked = false;

    }
    public void OnEnabelSquareSelection()
    {
        _clicked = true;
        _selected = false;
    }

    public void OnDisableSquareSelection()
    {
        _selected = false;
        _clicked = false;

        if (_currect == true)
            _disPlayImage.sprite = _correctletterData.image;
        else
            _disPlayImage.sprite = _normalLetterData.image;
    }

    public void OnSelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
            _disPlayImage.sprite = _selectedLetterData.image;
    }

    public void SetSprite(AlphabetData.LetterData normalLetterData, AlphabetData.LetterData selectedLetterData, 
        AlphabetData.LetterData correctletterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctletterData = correctletterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;
    }

    private void OnMouseDown()
    {
        OnEnabelSquareSelection();
        GameEvents.EnabelSquareSelectionMethod();
        CheckSquare();
        _disPlayImage.sprite = _selectedLetterData.image;
    }

    private void OnMouseEnter()
    {
        CheckSquare();
    }

    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSquareSelectionMethod();
    }

    public void CheckSquare()
    {
        if(_selected == false && _clicked == true)
        {
            if (SoundManager.instance.IsSoundFXMute() == false)
                _source.Play();
            _selected = true;
            GameEvents.CheckSquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
}