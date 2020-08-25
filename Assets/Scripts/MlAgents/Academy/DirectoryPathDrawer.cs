using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DirectoryPathAttribute))]
public class DirectoryPathDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log(property.objectReferenceValue);
        EditorGUI.ObjectField(position, property);
    }
}