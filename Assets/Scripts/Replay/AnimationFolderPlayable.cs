using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName = "AnimationFolderPlayable")]
public class AnimationFolderPlayable : PlayableAsset
{
    public DefaultAsset Folder;
    private double TotalDuration = 0f;
    public override double duration
    {
        get
        {
            return TotalDuration;
        }
    }
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        string path = AssetDatabase.GetAssetPath(Folder);
        string[] animFiles = AssetDatabase.FindAssets("t:AnimationClip", new string[] { path });
        AnimationClip[] animClips = new AnimationClip[animFiles.Length];
        AnimationClip newAnim = new AnimationClip();
        for (int i = 0; i < animFiles.Length; i++)
        {
            animClips[i] = AssetDatabase.LoadAssetAtPath<AnimationClip>(AssetDatabase.GUIDToAssetPath(animFiles[i]));
            TotalDuration += animClips[i].length;
        }

        TimelineAsset timeline = (TimelineAsset)owner.GetComponent<PlayableDirector>().playableAsset;
        TrackAsset track = timeline.GetRootTrack(0);

        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(graph, animFiles[0], owner.GetComponent<Animator>());
        AnimationClipPlayable playableClip = AnimationClipPlayable.Create(graph, animClips[0]);
        playableOutput.SetSourcePlayable(playableClip);


        return playableClip;
    }
}