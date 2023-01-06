using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class GameLevelData : ScriptableObject
{
    [System.Serializable] 
    public struct CategoryRecort
    {
        public string categoryName;
        public List<BoardData> boardData;
    }

    public List<CategoryRecort> data; 
}
