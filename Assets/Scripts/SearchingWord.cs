using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchingWord : MonoBehaviour
{

    public Text disPlayedText;
    public Image corssLine;
    private string _word;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameEvents.OnCorrectWord += CorrectWord;


    }

    private void OnDisable()
    {
        GameEvents.OnCorrectWord -= CorrectWord;

    }

    public void SetWord(string word)
    {
        _word = word;
        disPlayedText.text = word;

    }

    private void CorrectWord(string word, List<int> squareIndex)
    {
        if(word == _word)
        {
            corssLine.gameObject.SetActive(true);

        }
    }

}
