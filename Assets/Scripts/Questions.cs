using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Questions : ScriptableObject
{
    [System.Serializable]
    public class QuestionDatas
    {
        public string question = string.Empty;
        public bool isTrue = false;
        public bool questioned = false;

    }

    public int currentQuestion = 0;
    public List<QuestionDatas> questionList;

    public void AddQuestions()
    {
        questionList.Add(new QuestionDatas());
    }
}
