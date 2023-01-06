using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataServer : MonoBehaviour
{
   public static int ReadCategoryCurrectIndexValue(string name)
    {
        var value = -1;
        if (PlayerPrefs.HasKey(name))
            value = PlayerPrefs.GetInt(name);

        return value;

    }

    public static void SavedCategoryData(string categoryName, int categoryIndex)
    {
        PlayerPrefs.SetInt(categoryName, categoryIndex);
        PlayerPrefs.Save();

    }

    public static void ClearGameData(GameLevelData levelData)
    {
        foreach (var data in levelData.data)
        {
            PlayerPrefs.SetInt(data.categoryName, -1);
        }

        // Unlock first Level
        PlayerPrefs.SetInt(levelData.data[0].categoryName, 0);
        PlayerPrefs.Save();

    }

}
