using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    public QuizScripteData questionData;
    [SerializeField]
    public Image questionImage;

    [SerializeField]
    public WordData[] answerWordArry;
    [SerializeField]
    public WordData[] optionsWordArry;

    public char[] charArry = new char[12];
    public int currentAnswerIndex = 0;
    public bool currentAnswer = true;
    public List<int> selectedWordIndex;
    public int currentQuestionIndex = 0;
    public GameStatus gameStatus = GameStatus.Playing;
    public string answerWord;
    public int score = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        selectedWordIndex = new List<int>();
    }

    private void Start()
    {
        SetQuestion();
    }

    public void SetQuestion()
    {
        currentAnswerIndex = 0;
        selectedWordIndex.Clear();
        questionImage.sprite = questionData.questions[currentQuestionIndex].questionImage;
        answerWord = questionData.questions[currentQuestionIndex].answer;
        ResetQuestion();

        for (int i = 0; i < answerWord.Length; i++)
        {
            charArry[i] = char.ToUpper(answerWord[i]);
        }

        for (int i = answerWord.Length; i < optionsWordArry.Length; i++)
        {
            charArry[i] = (char)UnityEngine.Random.Range(65, 91);


        }

        charArry = ShuffleList.ShuffleListItems<char>(charArry.ToList()).ToArray();
        for (int i = 0; i < optionsWordArry.Length; i++)
        {
            optionsWordArry[i].SetChar(charArry[i]);
        }
        currentQuestionIndex++;
        gameStatus = GameStatus.Playing;
    }

    public void SelectedOption(WordData wordData)
    {
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length)
            return;
        selectedWordIndex.Add(wordData.transform.GetSiblingIndex());
        answerWordArry[currentAnswerIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        currentAnswerIndex++;
        if (currentAnswerIndex >= answerWord.Length)
        {
            currentAnswer = true;
        }

        for (int i = 0; i < answerWord.Length; i++)
        {
            if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordArry[i].charValue))
            {
                currentAnswer = false;
                break;
            }
        }
        if (currentAnswer)
        {
            gameStatus = GameStatus.Next;
            score += 50;
            Debug.Log("We Have Answered correct : " + score);
            if (currentQuestionIndex < questionData.questions.Count)
            {
                Invoke("SetQuestion", 0.5f);
            }
            else
            {
                gameOver.SetActive(true);
            }
        }
        else if (!currentAnswer)
        {
            Debug.Log("We Have not Answered correct");
        }

    }

    public void ResetQuestion()
    {
        for (int i = 0; i < answerWordArry.Length; i++)
        {
            answerWordArry[i].gameObject.SetActive(true);
            answerWordArry[i].SetChar('_');
        }

        for (int i = answerWord.Length; i < answerWordArry.Length; i++)
        {
            answerWordArry[i].gameObject.SetActive(false);
            // answerWordArry[i].SetChar('_');
        }

        for (int i = 0; i < optionsWordArry.Length; i++)
        {
            optionsWordArry[i].gameObject.SetActive(true);
        }

    }

    public void ReseLastWord()
    {
        if (selectedWordIndex.Count > 0)
        {
            int index = selectedWordIndex[selectedWordIndex.Count - 1];
            optionsWordArry[index].gameObject.SetActive(true);
            selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1);
            currentAnswerIndex--;
            answerWordArry[currentAnswerIndex].SetChar('_');
        }
    }
}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;

}

public enum GameStatus
{
    Playing,
    Next
};