using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSelector : MonoBehaviour
{
    public GameData currentGameData;
    public GameLevelData levelData;

    // Start is called before the first frame update
    void Awake()
    {
        SelectSquarentalBoardData();
    }

    private void SelectSquarentalBoardData()
    {
        foreach (var data in levelData.data)
        {
            if (data.categoryName == currentGameData.selectedCategoryName)
            {
                var boardIndex = DataServer.ReadCategoryCurrectIndexValue(currentGameData.selectedCategoryName);
                if (boardIndex < data.boardData.Count)
                {
                    currentGameData.selectedBoarddata = data.boardData[boardIndex];
                }
                else
                {
                    var randomIndex = Random.Range(0, data.boardData.Count);
                    currentGameData.selectedBoarddata = data.boardData[randomIndex];
                }

            }
        }
    }
}
