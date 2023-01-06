using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtility : MonoBehaviour
{
   public void LoadScene(string sceneName)
   {
       SceneManager.LoadScene(sceneName);
      
   }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void HidBannerAds()
    {
     //   AdManager.Instance.HideBanner();
    }

    public void MuteToggleBackgroundMusic()
    {
        SoundManager.instance.ToggleBackGroundMusic();
    }
    public void MuteToggleSoundFX()
    {
        SoundManager.instance.ToggleSoundFX();
    }
}
