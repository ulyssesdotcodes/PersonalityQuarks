using System;
using UnityEngine;

public enum QuarkEventType
{
    All,
    CollisionEnter,
    CollisionExit,
    Consumable,
    Create,
    Distance,
    Reset,
    Tag,
    Tagging,
    Transform,
    Pickup,
    Drop
}

[Serializable]
public class QuarkEvent : System.Object
{
    public QuarkEventType Type;
    public GameObject Target;
    public string Tag;
}

[Serializable]
public class TransformEvent : QuarkEvent
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public static TransformEvent Create(GameObject gameObject)
    {
        TransformEvent e = new TransformEvent();
        e.Type = QuarkEventType.Transform;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.Position = gameObject.transform.localPosition;
        e.Rotation = gameObject.transform.localRotation;
        e.Scale = gameObject.transform.localScale;
        return e;
    }
}

[Serializable]
public class CreateEvent : QuarkEvent
{
    public string Name;
    public string AssetPath;

    public static CreateEvent Create(string assetPath, string name)
    {
        CreateEvent e = new CreateEvent();
        e.Type = QuarkEventType.Create;
        e.AssetPath = assetPath;
        e.Name = name;
        return e;
    }

    public static CreateEvent Create(GameObject gameObject, string AssetPath)
    {
        TransformEvent t = TransformEvent.Create(gameObject);
        CreateEvent e = new CreateEvent();
        e.Type = QuarkEventType.Create;
        e.Target = t.Target;
        e.Tag = t.Tag;
        e.Name = gameObject.name;
        e.AssetPath = AssetPath;
        return e;
    }
}

[Serializable]
public class ResetEvent : QuarkEvent
{
    public static ResetEvent Create(GameObject gameObject)
    {
        ResetEvent e = new ResetEvent();
        e.Type = QuarkEventType.Reset;
        e.Tag = gameObject.tag;
        return e;
    }
}

[Serializable]
public class CollisionEnterEvent : QuarkEvent
{
    public Vector3 CollisionPosition;
    public string OtherTag;

    public static CollisionEnterEvent Create(GameObject gameObject, Collision col)
    {
        CollisionEnterEvent e = new CollisionEnterEvent();
        e.Type = QuarkEventType.CollisionEnter;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.OtherTag = col.gameObject.tag;
        e.CollisionPosition = col.GetContact(0).point;
        return e;
    }
}

[Serializable]
public class CollisionExitEvent : QuarkEvent
{
    public Vector3 CollisionPosition;
    public string OtherTag;

    public static CollisionExitEvent Create(GameObject gameObject, Collision col)
    {
        CollisionExitEvent e = new CollisionExitEvent();
        e.Type = QuarkEventType.CollisionExit;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.OtherTag = col.gameObject.tag;
        return e;
    }
}

[Serializable]
public class TriggerEnterEvent : QuarkEvent
{
    public string OtherTag;

    public static TriggerEnterEvent Create(GameObject gameObject, Collider col)
    {
        TriggerEnterEvent e = new TriggerEnterEvent();
        e.Type = QuarkEventType.CollisionEnter;
        e.Tag = gameObject.tag;
        e.OtherTag = col.gameObject.tag;
        return e;
    }
}

[Serializable]
public class TriggerExitEvent : QuarkEvent
{
    public int OtherId;

    public static TriggerExitEvent Create(GameObject gameObject, Collider col)
    {
        TriggerExitEvent e = new TriggerExitEvent();
        e.Type = QuarkEventType.CollisionExit;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.OtherId = col.gameObject.GetInstanceID();
        return e;
    }
}

[Serializable]
public class TagEvent : QuarkEvent
{
    public static TagEvent Create(GameObject gameObject)
    {
        TagEvent e = new TagEvent();
        e.Type = QuarkEventType.Tag;
        // e.Id = gameObject.GetInstanceID();
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        return e;
    }
}

[Serializable]
public class TaggingEvent : QuarkEvent
{
    public string NewTag;

    public static TaggingEvent Create(GameObject gameObject, string NewTag)
    {
        TaggingEvent e = new TaggingEvent();
        e.Type = QuarkEventType.Tagging;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.NewTag = NewTag;
        return e;
    }
}

[Serializable]
public class DistanceEvent : QuarkEvent
{
    public float Distance;

    public static DistanceEvent Create(GameObject gameObject, float distance)
    {
        DistanceEvent e = new DistanceEvent();
        e.Type = QuarkEventType.Distance;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.Distance = distance;
        return e;
    }
}

[Serializable]
public class ConsumableEvent : QuarkEvent
{
    public float Reward;
    public Vector3 Position;

    public static ConsumableEvent Create(GameObject gameObject, float reward, Vector3 position)
    {
        ConsumableEvent e = new ConsumableEvent();
        e.Type = QuarkEventType.Consumable;
        e.Target = gameObject;
        e.Tag = gameObject.tag;
        e.Reward = reward;
        e.Position = position;
        return e;
    }
}

[Serializable]
public class DropEvent : QuarkEvent
{

    public static DropEvent Create(GameObject gameObject, GameObject target)
    {
        DropEvent e = new DropEvent();
        e.Type = QuarkEventType.Drop;
        e.Target = gameObject;
        e.Tag = gameObject.tag;

        return e;
    }
}