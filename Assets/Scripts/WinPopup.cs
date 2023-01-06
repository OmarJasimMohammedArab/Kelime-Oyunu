using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    public GameObject winPupup;
   // public WordChecker wordChecker;
   // bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test false");
        winPupup.SetActive(false);

    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinPopup;
      //  AdManager.OnInterstitialAdsClosed += InterstitialAdCompleted;

    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinPopup;
       // AdManager.OnInterstitialAdsClosed -= InterstitialAdCompleted;
    }

    private void InterstitialAdCompleted()
    {
        throw new NotImplementedException();
    }

    public void ShowWinPopup()
    {
       // AdManager.Instance.HideBanner();
        Debug.Log("test True");
        winPupup.SetActive(true);
    }

    public void LoadNextLevel()
    {
      //  AdManager.Instance.ShowInterstitialAd();
        GameEvents.LoadNextLevelMethod();
    }
}
