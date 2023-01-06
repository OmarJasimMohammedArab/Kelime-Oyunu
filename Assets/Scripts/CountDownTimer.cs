using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{

    public GameData currenGameData;
    public Text timerText;
    private float _timerLeft; 
    private float _minutes; 
    private float _seconds; 
    private float _oneSecondDown;
    private bool _timeOut;
    private bool _stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        _stopTimer = false;
        _timeOut = false;
        _timerLeft = currenGameData.selectedBoarddata.timeInSeconds;

        GameEvents.OnBoardCompleted += StopTimer;
        GameEvents.OnUnlockNextCategory += StopTimer;

    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= StopTimer;
        GameEvents.OnUnlockNextCategory -= StopTimer;

    }

    public void StopTimer()
    {
        _stopTimer = true;
    }

    void Update()
    {
        if (_stopTimer == false)
            _timerLeft -= Time.deltaTime;
        if (_timerLeft <= _oneSecondDown)
        {
            _oneSecondDown = _timerLeft - 1f;
        }
    }

    private void OnGUI()
    {
        if(_timeOut == false)
        {
            if (_timerLeft > 0)
            {
                _minutes = Mathf.Floor(_timerLeft / 60);
                _seconds = Mathf.RoundToInt(_timerLeft % 60);

                timerText.text = _minutes.ToString("00") + ":" + _seconds.ToString("00");
            }
            else
            {
                _stopTimer = true;
                ActivateGameOverGUI();
            }
        }
    }

    public void ActivateGameOverGUI()
    {
        GameEvents.GameOverMethod();
        _timeOut = true;
    }
}
