using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class QuestionDatas : MonoBehaviour
{
    public Questions questions;
    [SerializeField] private Text _questionText;

    public Score  score;
    public GameObject gameEnd;
   // public Text YourScore;
    private void Start()
    {
        AskQuestion();
    }

    public void AskQuestion()
    {
        if (CountValidQuestions() == 0)
        {
            _questionText.text = string.Empty;
            ClearQuestions();
            gameEnd.SetActive(true);
            
            return;
        }

        var randomIndex = 0;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, questions.questionList.Count);
        } while (questions.questionList[randomIndex].questioned == true);

        questions.currentQuestion = randomIndex;
        questions.questionList[questions.currentQuestion].questioned = true;
        _questionText.text = questions.questionList[questions.currentQuestion].question;
    }

    private void ClearQuestions()
    {
        foreach (var question in questions.questionList)
        {
            question.questioned = false;
        }
    }



    private int CountValidQuestions()
    {
        int validQuestion = 0;

        foreach (var question in questions.questionList)
        {
            if (question.questioned == false)
                validQuestion++;
        }
        Debug.Log("Question Left" + validQuestion);
        return validQuestion;
    }
}
