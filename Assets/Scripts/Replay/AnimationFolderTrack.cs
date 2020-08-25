using System;
using UnityEngine.Timeline;
using UnityEditor;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "AnimationFolderTrack")]
public class AnimationFolderTrack : AnimationTrack
{
    public DefaultAsset Folder;
    public string LastCheckedPath;

    public void OnEnable()
    {
    }

    public void OnValidate()
    {

        string path = AssetDatabase.GetAssetPath(Folder);
        if (LastCheckedPath != path && Folder != null)
        {
            m_Clips.Clear();
            string[] animFiles = AssetDatabase.FindAssets("t:AnimationClip", new string[] { path });
            AnimationClip[] animClips = new AnimationClip[animFiles.Length];
            TimelineClip[] timelineClips = new TimelineClip[animFiles.Length];

            for (int i = 0; i < animFiles.Length; i++)
            {
                animClips[i] = AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath(animFiles[i]));
                CreateClip(animClips[i]);
                // AnimationPlayableAsset animationAsset = ScriptableObject.CreateInstance<AnimationPlayableAsset>();
                // animationAsset.clip = animClips[i];
                // EditorUtility.SetDirty(animationAsset);
                // TimelineClip timelineClip = CreateClip<AnimationPlayableAsset>();
                // timelineClip.asset = animationAsset;
                // m_Clips.Add(timelineClip);
            }
            LastCheckedPath = path;
        }
    }
}