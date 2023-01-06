using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPuzzelButtun : MonoBehaviour
{

    public GameData gameData;
    public GameLevelData levelData;
    public Text categoryText;
    public Image progressBarFilling;

    private string gameSceneName = "GameScene";

    private bool _levelLocked;
    void Start()
    {
        _levelLocked = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        UpdateButtonInformation();

        if (_levelLocked )
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true; 
        }
    }

    private void OnEnable()
    {
        //  AdManager.OnInterstitialAdsClosed += InterstitialAdsClosed;
    }

    private void OnDisable()
    {
       // AdManager.OnInterstitialAdsClosed -= InterstitialAdsClosed;
    }

    private void InterstitialAdsClosed()
    {
        throw new NotImplementedException();
    }

    void Update()
    {

    }

    private void UpdateButtonInformation()
    {
        var currentIndex = -1;
        var totalBorards = 0;
        foreach (var data in levelData.data)
        {
             if(data.categoryName == gameObject.name)
             {
                currentIndex = DataServer.ReadCategoryCurrectIndexValue(gameObject.name);
                totalBorards = data.boardData.Count;
                if(levelData.data[0].categoryName == gameObject.name && currentIndex < 0)
                {
                    DataServer.SavedCategoryData(levelData.data[0].categoryName, 0);
                    currentIndex = DataServer.ReadCategoryCurrectIndexValue(gameObject.name);
                    totalBorards = data.boardData.Count;

                }
             }
        }
        if (currentIndex == -1)
            _levelLocked = true;

        categoryText.text = _levelLocked ? string.Empty : (currentIndex.ToString()+ "/" + totalBorards.ToString());
        progressBarFilling.fillAmount = (currentIndex > 0 && totalBorards > 0) ? ((float)currentIndex / (float)totalBorards) : 0f;
    }

    private void OnButtonClick()
    {
        gameData.selectedCategoryName = gameObject.name;
      //  AdManager.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(gameSceneName);
    }
}
