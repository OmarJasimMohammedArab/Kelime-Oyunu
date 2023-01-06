using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    [SerializeField]
    private Text charText;
    [HideInInspector]
    public char charValue;
    public Button buttonObj;

    public void Awake()
    {
        buttonObj = GetComponent<Button>();

        if (buttonObj)
        {
            buttonObj.onClick.AddListener(() => CharSelect());
        }

    }

    public void SetChar(char value)
    {
        charText.text = value + "";
        charValue = value;

    }

    public void CharSelect()
    {
        QuizManager.instance.SelectedOption(this);
    }
}
