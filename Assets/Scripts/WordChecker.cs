using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;
    public GameLevelData gameLevelData;
    public WinPopup win;
    private string _word;

    private int _assignedPoints = 0;
    public int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayLeft, _rayRight;
    private Ray _raydiagonalLeftUp, _raydiagonalLeftDown;
    private Ray _raydiagonaRightftUp, _raydiagonalRightDown;
    private Ray _currentRay = new Ray();
    private Vector3 _rayStartPosition;
    private List<int> _correctSquareList = new List<int>();

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnLoadNextLevel += LoadNextGameLevel;
    }
    

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnLoadNextLevel -= LoadNextGameLevel;
    }

    private void LoadNextGameLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

     void Start()
     {
        currentGameData.selectedBoarddata.ClearData();
         _assignedPoints = 0;
         _completedWords = 0;
      //  AdManager.Instance.ShowBanner();
     }

     void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction * 4);
            Debug.DrawRay(_rayDown.origin, _rayDown.direction * 4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction * 4);
            Debug.DrawRay(_raydiagonalLeftUp.origin, _raydiagonalLeftUp.direction * 4);
            Debug.DrawRay(_raydiagonalLeftDown.origin, _raydiagonalLeftDown.direction * 4);
            Debug.DrawRay(_raydiagonaRightftUp.origin, _raydiagonaRightftUp.direction * 4);
            Debug.DrawRay(_raydiagonalRightDown.origin, _raydiagonalRightDown.direction * 4);
        }
    }

    private void SquareSelected(string letter, Vector3 squarePosition, int squareIndex)
    {
        if(_assignedPoints == 0)
        {
            _rayStartPosition = squarePosition;
            _correctSquareList.Add(squareIndex);
            _word += letter;

            _rayUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, 1));
            _rayDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 0f));
            _rayRight = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 0f));
            _raydiagonalLeftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 1));
            _raydiagonalLeftDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, -1));
            _raydiagonaRightftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 1));
            _raydiagonalRightDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, -1));

        }
        else if(_assignedPoints == 1)
        {
            _correctSquareList.Add(squareIndex);
            _currentRay = SelectRay(_rayStartPosition, squarePosition);
            GameEvents.SelectSquareMethod(squarePosition);
            _word += letter;
            CheckWord();
        }

        else
        {
            if(InPointOnTheRay(_currentRay, squarePosition))
            {
                _correctSquareList.Add(squareIndex);
                GameEvents.SelectSquareMethod(squarePosition);
                _word += letter;
                CheckWord();
            }
        }
        _assignedPoints++;

    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedBoarddata.SearchWords)
        {
            if (_word == searchingWord.Word && searchingWord.found == false)
            {
                searchingWord.found = true;
                GameEvents.CorrectWordMethod(_word, _correctSquareList);
                _completedWords++;
                _word = string.Empty;
                _correctSquareList.Clear();
                CheckBoardCompleted();
                return;
            }
        }
    }

    private bool InPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, maxDistance: 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.position ==point)
                return true;
        }
        return false;
    }

    private Ray SelectRay(Vector2 firstPosition,Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;
        if(Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - 1f) < tolerance)
        {
            return _rayUp;
        }
        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - (-1f)) < tolerance)
        {
            return _rayDown;
        }
        if (Math.Abs(direction.x - (-1f)) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayLeft;
        }
        if (Math.Abs(direction.x - 1f) < tolerance && Math.Abs(direction.y) < tolerance)
        {
            return _rayRight;
        }
        if(direction.x < 0f && direction.y > 0f)
        {
            return _raydiagonalLeftUp;
        }
        if (direction.x < 0f && direction.y < 0f)
        {
            return _raydiagonalLeftDown;
        }
        if (direction.x > 0f && direction.y > 0f)
        {
            return _raydiagonaRightftUp;
        }
        if (direction.x > 0f && direction.y < 0f)
        {
            return _raydiagonalRightDown;
        }

        return _rayDown;

    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }

    private void CheckBoardCompleted()
    {
        bool loadNextCategory = false;
        if (currentGameData.selectedBoarddata.SearchWords.Count == _completedWords) 
        {
            //save current level progres
            var categoryName = currentGameData.selectedCategoryName;
            var currentBoardIndex = DataServer.ReadCategoryCurrectIndexValue(categoryName);
            var nextBoardIndex = -1;
            var currenCategoryIndex = 0;
            bool readNextLevelName = false;
            for (int index = 0; index < gameLevelData.data.Count; index++)
            {
                if (readNextLevelName)
                {
                    nextBoardIndex = DataServer.ReadCategoryCurrectIndexValue(gameLevelData.data[index].categoryName);
                    readNextLevelName = false;
                }
                if (gameLevelData.data[index].categoryName == categoryName)
                {
                    readNextLevelName = true;
                    currenCategoryIndex = index;
                    Debug.Log("Test");
                    win.ShowWinPopup();
                }
                
            }
            var currentLevelSize = gameLevelData.data[currenCategoryIndex].boardData.Count;
            if (currentBoardIndex < currentLevelSize)
                currentBoardIndex += 1;
            
            DataServer.SavedCategoryData(categoryName, currentBoardIndex);
            //Inlocak Next Category
            if (currentBoardIndex >= currentLevelSize)
            {
                currenCategoryIndex++;
                if (currenCategoryIndex < gameLevelData.data.Count)
                {
                    categoryName = gameLevelData.data[currenCategoryIndex].categoryName;
                    currentBoardIndex = 0;
                    loadNextCategory = true;

                    if (nextBoardIndex <= 0)
                    {
                        DataServer.SavedCategoryData(categoryName, currentBoardIndex);
                      
                    }
                }
                else
                {
                    SceneManager.LoadScene("SelectCategory");
                }
            }
            else
            {
                GameEvents.BoardCompletedMethod();
            }
            if (loadNextCategory)
                GameEvents.UnlockNextCategoryMethod();
           
        }

    }

}
