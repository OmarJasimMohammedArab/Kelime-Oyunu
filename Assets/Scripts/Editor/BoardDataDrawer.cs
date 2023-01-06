using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text.RegularExpressions;

[CustomEditor(typeof(BoardData), editorForChildClasses:false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
    public BoardData GameDataInstanse => target as BoardData;
    public ReorderableList _dataList;

    public void OnEnable()
    {
        InitializeReorderableList(ref _dataList, propertyName: "SearchWords", listLabel:"Searching Words");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameDataInstanse.timeInSeconds = EditorGUILayout.FloatField("Max Game Time (in Seconds)", GameDataInstanse.timeInSeconds);
        DrawColumnsRowsInputFields();
        EditorGUILayout.Space();
        ConvertToUpperButton();

        if (GameDataInstanse.Boards != null && GameDataInstanse.Columns > 0 && GameDataInstanse.Rows > 0)
            DrawBoardTable();
        GUILayout.BeginHorizontal();
        ClearboardButton();
        FillUpWidhRandomLetterButton();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        _dataList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstanse);

        }
    }

    private void DrawColumnsRowsInputFields()
    {
        var columnsTemp = GameDataInstanse.Columns;
        var rowsTemp = GameDataInstanse.Rows;
        GameDataInstanse.Columns = EditorGUILayout.IntField(label: "Columns", GameDataInstanse.Columns);
        GameDataInstanse.Rows = EditorGUILayout.IntField(label: "Rows", GameDataInstanse.Rows);
        if ((GameDataInstanse.Columns != columnsTemp || GameDataInstanse.Rows != rowsTemp) 
            && GameDataInstanse.Columns>0 && GameDataInstanse.Rows>0) 
        {
            GameDataInstanse.CreateNewBoard();
        }
    }

    public void DrawBoardTable()
    {
        var tablestyle = new GUIStyle(other: "box");
        tablestyle.padding = new RectOffset(left: 10,right: 10,top: 10,bottom: 10);
        tablestyle.margin.left = 32;
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;
        var ColumnStyle = new GUIStyle();
        ColumnStyle.fixedWidth = 50;
        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter ;
        var textFieldStyle = new GUIStyle();
        textFieldStyle.normal.background = Texture2D.grayTexture;
        textFieldStyle.normal.textColor = Color.white;
        textFieldStyle.fontStyle = FontStyle.Bold;
        textFieldStyle.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.BeginHorizontal(tablestyle);
        for (var x = 0; x < GameDataInstanse.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : ColumnStyle);
            for (var y = 0; y < GameDataInstanse.Rows; y++)
            {
                if(x >=0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstanse.Boards[x].Row[y], textFieldStyle);
                    if (GameDataInstanse.Boards[x].Row[y].Length > 1)
                    {
                        character = GameDataInstanse.Boards[x].Row[y].Substring(startIndex: 0, length: 1);
                    }
                    GameDataInstanse.Boards[x].Row[y] = character;
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    public void InitializeReorderableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
            draggable: true, displayHeader:true, displayAddButton:true, displayRemoveButton:true);

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, listLabel);
        };

        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y +=2;
            EditorGUI.PropertyField(
                position: new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth,EditorGUIUtility.singleLineHeight),
              element.FindPropertyRelative("Word"), GUIContent.none);
        };


    }

    public void ConvertToUpperButton()
    {
        if(GUILayout.Button(text:"To Upper"))
        {
            for (int i = 0; i < GameDataInstanse.Columns; i++)
            {
                for (int j = 0; j < GameDataInstanse.Rows; j++)
                {
                    var errorConter = Regex.Matches(input: GameDataInstanse.Boards[i].Row[j], pattern: @"a-2").Count;
                    if (errorConter > 0)
                        GameDataInstanse.Boards[i].Row[j] = GameDataInstanse.Boards[i].Row[j].ToUpper();
                }
            }

            foreach (var searchWord in GameDataInstanse.SearchWords)
            {
                var errorConter = Regex.Matches(input: searchWord.Word, pattern: @"a-2").Count;

                if(errorConter > 0)
                {
                    searchWord.Word = searchWord.Word.ToUpper();
                }

            }
        }
    }

    private void ClearboardButton()
    {
        if(GUILayout.Button(text:"Clear Board"))
        {
            for (int i = 0; i < GameDataInstanse.Columns; i++)
            {
                for (int j = 0; j < GameDataInstanse.Rows; j++)
                {
                    GameDataInstanse.Boards[i].Row[j] = " ";
                }
            }
        }
    }

    private void FillUpWidhRandomLetterButton()
    {

        if (GUILayout.Button(text: "Fill Up Widh Random"))
        {
            for (int i = 0; i < GameDataInstanse.Columns; i++)
            {
                for (int j = 0; j < GameDataInstanse.Rows; j++)
                {
                   int erroCounter = Regex.Matches(input: GameDataInstanse.Boards[i].Row[j], pattern: @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int index = UnityEngine.Random.Range(0, letters.Length);

                    if (erroCounter == 0)
                        GameDataInstanse.Boards[i].Row[j] = letters[index].ToString();
                }
            }
        }
    }
}
