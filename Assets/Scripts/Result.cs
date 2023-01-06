using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Result : MonoBehaviour
{
    public Questions questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public Score score;

    public Button trueBottun;
    public Button falseBottun;

    public UnityEvent onNextQuestion;

    // Start is called before the first frame update
    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);
    }

    public void ShowResult(bool answer)
    {
        correctSprite.SetActive(questions.questionList[questions.currentQuestion].isTrue == answer);
        incorrectSprite.SetActive(questions.questionList[questions.currentQuestion].isTrue != answer);

        if (questions.questionList[questions.currentQuestion].isTrue == answer)
            score.AddScore();
        else
            score.DeductScore();

        trueBottun.interactable = false;
        falseBottun.interactable = false;

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1.0f);

        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        trueBottun.interactable = true;
        falseBottun.interactable = true;

        onNextQuestion.Invoke();
    }
}
