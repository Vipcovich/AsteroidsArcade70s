using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMax))]
public class MinMaxEditor : PropertyDrawer
{
    private float lineHeight = 18f;
    private Rect lastRect;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        lastRect = new Rect(position.x, position.y, 0f, lineHeight);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUI.LabelField    (GetNextRect(30), "min");
        EditorGUI.PropertyField (GetNextRect(50), property.FindPropertyRelative("min"), GUIContent.none);
        EditorGUI.LabelField    (GetNextRect(40), "  max");
        EditorGUI.PropertyField (GetNextRect(50), property.FindPropertyRelative("max"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return lineHeight;
    }

    private Rect GetNextRect(float width)
    {
        lastRect.x += lastRect.width;
        lastRect.width = width;
        return lastRect;
    }
}

