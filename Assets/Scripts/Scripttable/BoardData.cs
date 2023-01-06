using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    [System.Serializable]
    public class SearchingWord
    {
        [HideInInspector]
        public bool found = false;
        public string Word;
    }

    [System.Serializable]
    public class BoardRow
    {
        public int Size;
        public string[] Row;

        public BoardRow() { }
        public BoardRow(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            Size = size;
            Row = new string[Size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }
    }

    public float timeInSeconds;
    public int Columns = 0;
    public int Rows = 0;


    public BoardRow[] Boards;
    public List<SearchingWord> SearchWords = new List<SearchingWord>();

    public void ClearData()
    {
        foreach (var word in SearchWords)
        {
            word.found = false;
        }
    }

    public void ClearWiteEmptyStrin()
    {
        for (int i = 0; i < Columns; i++)
        {
            Boards[i].ClearRow(); 
        }
    }

    public void CreateNewBoard()
    {
        Boards = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Boards[i] = new BoardRow(Rows);
        }
    }
}
