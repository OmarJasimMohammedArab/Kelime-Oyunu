using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingWordsList : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject searchingWordPrefab;
    public float offset = 0.0f;
    public int maxColumns = 5;
    public int maxRows = 4;

    private int _columns = 2;
    private int _rows;
    private int _wordsNumber;

    private List<GameObject> _word = new List<GameObject>();


    // Start is called before the first frame update
    private void Start()
    {

        _wordsNumber = currentGameData.selectedBoarddata.SearchWords.Count;
        if (_wordsNumber < _columns)
            _rows = 1;
        else
            CalculateColumnsAndRowsNumber();

        CreateWordObjects();
        SetWordPosition();


    }

    private void CalculateColumnsAndRowsNumber()
    {
        do
        {
            _columns++;
            _rows = _wordsNumber / _columns;

        }
        while (_rows >= maxRows);
        
        if(_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;

        }

    }

    private bool TryIncreaseColumnsNumber()
    {
        _columns++;
        _rows = _wordsNumber / _columns;
        if (_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;

            return false;
        }
        if (_wordsNumber % _columns > 0)
            _rows++;
        return true;
    }

    private void CreateWordObjects()
    {
        var squareScale = GetSquareScale(new Vector3(1f, 1f, 0.1f));
        for (int index = 0; index < _wordsNumber; index++)
        {
            _word.Add(Instantiate(searchingWordPrefab) as GameObject);
            _word[index].transform.SetParent(this.transform);
            _word[index].GetComponent<RectTransform>().localScale = squareScale;
            _word[index].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            _word[index].GetComponent<SearchingWord>().SetWord(currentGameData.selectedBoarddata.SearchWords[index].Word);

        }

    }

    private Vector3 GetSquareScale( Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if(finalScale.x <= 0 || finalScale.y <=0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;


            }

        }
        return finalScale;
    }

    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = searchingWordPrefab.GetComponent<RectTransform>();
        var parenRect = this.GetComponent<RectTransform>();
        var squareSize = new Vector2(0f, 0f);
        squareSize.x = squareRect.rect.width * targetScale.x + offset;
        squareSize.y = squareRect.rect.height * targetScale.y + offset;

        var totalSquaresHieght = squareSize.y * _rows;

        if(totalSquaresHieght > parenRect.rect.height)
        {
            while (totalSquaresHieght > parenRect.rect.height)
            {
                if (TryIncreaseColumnsNumber())
                    totalSquaresHieght = squareSize.y * _rows;
                else
                    return true;
            }
        }

        var totalSquaresWidth = squareSize.x * _columns;
        if (totalSquaresHieght > parenRect.rect.width)
            return true;

        return false;
    }

    private void SetWordPosition()
    {
        var squareRect = _word[0].GetComponent<RectTransform>();
        var wordOffset = new Vector2
        {
            x = squareRect.rect.width * squareRect.transform.localScale.x + offset,
            y = squareRect.rect.height * squareRect.transform.localScale.y + offset
        };

        int columnsNumber = 0;
        int rowNumber = 0;
        var startPosition = GetFirstSquarePosition();
        foreach (var word in _word)
        {
            if(columnsNumber +1 > _columns)
            {
                columnsNumber = 0;
                rowNumber++;
            }
            var positionX = startPosition.x + wordOffset.x * columnsNumber;
            var positionY = startPosition.y - wordOffset.y * rowNumber;

            word.GetComponent<RectTransform>().localPosition = new Vector2(positionX, positionY);
            columnsNumber++;

        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = _word[0].GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.rect.width * squareRect.transform.localScale.x + offset;
        squareSize.y = squareRect.rect.height * squareRect.transform.localScale.y + offset;

        //make sure they are in the center
        var shiftBy = (parentRect.rect.width - (squareSize.x * _columns)) / 2;

        startPosition.x = ((parentRect.rect.width - squareSize.x) / 2) * (-1);
        startPosition.x += shiftBy;
        startPosition.y = (parentRect.rect.height- squareSize.y) / 2;

        return startPosition;

    }
}
