using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomEditor(typeof(PersonalityQuarksArea))]
public class PersonalityQuarksAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PersonalityQuarksArea area = (PersonalityQuarksArea)target;
        DrawPropertiesExcluding(serializedObject, new string[] { "AnimationsPath" });
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.ObjectField("Animations Directory", area.AnimationsPath, typeof(DefaultAsset), false);
        if (EditorGUI.EndChangeCheck())
        {
            PlayableDirector director = area.GetComponent<PlayableDirector>();
            string path = AssetDatabase.GetAssetPath(area.AnimationsPath);
            string[] animFiles = Directory.GetFiles(Application.dataPath + "/" + path);
            AnimationClip[] animClips = new AnimationClip[animFiles.Length];
            for (int i = 0; i < animFiles.Length; i++)
            {
                animClips[i] = AssetDatabase.LoadAssetAtPath<AnimationClip>(path + "/" + animFiles[i]);
            }

            // anim.stat
            // anim.runtimeAnimatorController = aoc;
        }
    }
}