using System.Linq;
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class Recorder : QuarkEventListener
{
    public bool Record = false;
    public float MinimumAnimationTime = 10f;
    private int Count = 0;
    private bool isRecording = false;
    private LinkedList<QuarkEvent> events;
    private AnimationClip animationClip;
    private PersonalityQuarksArea area;
    private int StartFrame;
    private float StartTime;
    private string folder;

    public void Start()
    {
        area = GetComponent<PersonalityQuarksArea>();
        folder = DateTime.Now.ToString("yyyy_dd_MM_HH_mm");
    }

    public void OnApplicationQuit()
    {
        AssetDatabase.SaveAssets();
    }

    public void StartRecording()
    {
        if (!(Record && Application.isEditor))
        {
            return;
        }

        if (!AssetDatabase.IsValidFolder("Assets/Recordings/" + folder))
        {
            AssetDatabase.CreateFolder("Assets/Recordings", folder);
            AssetDatabase.SaveAssets();
        }

        isRecording = true;
        animationClip = new AnimationClip();
        StartFrame = Time.frameCount;
        StartTime = Time.time;
    }

    public void StopRecording()
    {
        if (!isRecording)
        {
            return;
        }

        isRecording = false;

        if (animationClip.length < MinimumAnimationTime)
        {
            return;
        }

        AnimationEvent[] events = animationClip.events;
        foreach (AnimationEvent e in events)
        {
            e.time = e.time / animationClip.length;
        }

        AnimationUtility.SetAnimationEvents(animationClip, events);
        AssetDatabase.CreateAsset(animationClip, "Assets/Recordings/" + folder + "/Animation_" + Count + ".anim");
        Count++;
    }

    public override void OnEvent(QuarkEvent e)
    {
        if (!isRecording)
        {
            return;
        }


        switch (e.Type)
        {
            case QuarkEventType.Transform:
                if (e.Target != null)
                {
                    string path = AnimationUtility.CalculateTransformPath(e.Target.transform, area.transform);
                    Recorder.AppendTransformCurve(animationClip, path, StartFrame, (TransformEvent)e);
                }
                break;
            case QuarkEventType.Create:
                AnimationEvent createEvent = new AnimationEvent();
                createEvent.functionName = "SpawnPrefab";
                createEvent.time = Time.time - StartTime;
                createEvent.stringParameter = JsonUtility.ToJson(e);
                animationClip.AddEvent(createEvent);
                break;
        }
    }

    // public void SpawnPrefab(string prefab)
    // {
    //     area.SpawnPrefab(prefab);
    // }

    // Add a float value to the animation curve specified by the clip, path, Type, and propertyName at the current frame
    private static void AppendCurve(AnimationClip clip, string path, Type type, string propertyName, float value, int StartTime)
    {
        EditorCurveBinding binding = EditorCurveBinding.FloatCurve(path, typeof(Transform), propertyName);
        AnimationCurve curve = AnimationUtility.GetEditorCurve(
            clip,
            binding
        );

        if (curve == null)
        {
            curve = new AnimationCurve();
        }

        curve.AddKey(Time.frameCount - StartTime, value);

        AnimationUtility.SetEditorCurve(clip, binding, curve);
    }

    //  Append all the values of a transform to the specified animation clip
    private static void AppendTransformCurve(AnimationClip clip, string path, int StartTime, TransformEvent t)
    {
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalPosition.x", t.Position.x, StartTime);
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalPosition.y", t.Position.y, StartTime);
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalPosition.z", t.Position.z, StartTime);

        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalRotation.w", t.Rotation.w, StartTime);
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalRotation.x", t.Rotation.x, StartTime);
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalRotation.y", t.Rotation.y, StartTime);
        Recorder.AppendCurve(clip, path, typeof(Transform), "m_LocalRotation.z", t.Rotation.z, StartTime);
    }

}