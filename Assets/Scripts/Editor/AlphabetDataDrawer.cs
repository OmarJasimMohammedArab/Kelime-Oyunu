using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AlphabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlphabetDataDrawer : Editor
{
    public ReorderableList AlpbetPlainList;
    public ReorderableList AlpbetNormalList;
    public ReorderableList AlpbetHighlightedList;
    public ReorderableList AlpbetWrongList;

    public void OnEnable()
    {
        InitializeReorderableList(ref AlpbetPlainList, propartyName: "AlphabetPlain", listLabel: "Alphabet Plain");
        InitializeReorderableList(ref AlpbetNormalList, propartyName: "AlphabetNormal", listLabel: "Alphabet Normal");
        InitializeReorderableList(ref AlpbetHighlightedList, propartyName: "AlphabetHighlighted", listLabel: "Alphabet Highlighted");
        InitializeReorderableList(ref AlpbetWrongList, propartyName: "AlphabetWrong", listLabel: "Alphabet Wrong");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AlpbetPlainList.DoLayoutList();
        AlpbetNormalList.DoLayoutList();
        AlpbetHighlightedList.DoLayoutList();
        AlpbetWrongList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    public void InitializeReorderableList(ref ReorderableList list, string propartyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, elements: serializedObject.FindProperty(propartyName),
          draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };

        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                position: new Rect(rect.x, rect.y, width:60, EditorGUIUtility.singleLineHeight),
              element.FindPropertyRelative("letter"), GUIContent.none);

            EditorGUI.PropertyField(
               position: new Rect(rect.x +70, rect.y, width:rect.width -60 -30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("image"), GUIContent.none);

        };
    }
}
