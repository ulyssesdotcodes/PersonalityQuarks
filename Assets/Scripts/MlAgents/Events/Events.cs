using System;
using UnityEngine;

public enum QuarkEventType {
  All,
  Create,
  Reset,
  Transform,
  CollisionEnter,
  CollisionExit,
  Tag,
  Tagging
}

[Serializable]
public class QuarkEvent : System.Object {
  public QuarkEventType Type;
  public int Id;
  public string Tag;
}

[Serializable]
public class TransformEvent : QuarkEvent {
  public Vector3 Position;
  public Quaternion Rotation;
  public Vector3 Scale;
  
  public static TransformEvent Create(GameObject gameObject) {
    TransformEvent e = new TransformEvent();
    e.Type = QuarkEventType.Transform;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    e.Position = gameObject.transform.localPosition;
    e.Rotation = gameObject.transform.localRotation;
    e.Scale = gameObject.transform.localScale;
    return e;
  }
}

[Serializable]
public class CreateEvent : TransformEvent {
  public string Name;
  
  public static CreateEvent Create(string name, GameObject gameObject) {
    TransformEvent t  = TransformEvent.Create(gameObject);
    CreateEvent e = new CreateEvent();
    e.Type = QuarkEventType.Create;
    e.Id = t.Id;
    e.Tag = t.Tag;
    e.Position = t.Position;
    e.Rotation = t.Rotation;
    e.Scale = t.Scale;
    e.Name = name;
    return e;
  }
}

[Serializable]
public class ResetEvent : QuarkEvent {
  public static ResetEvent Create(GameObject gameObject) {
    ResetEvent e = new ResetEvent();
    e.Type = QuarkEventType.Reset;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    return e;
  }
}

[Serializable]
public class CollisionEnterEvent : QuarkEvent {
  public Vector3 CollisionPosition;
  public string OtherTag;

  public static CollisionEnterEvent Create(GameObject gameObject, Collision col) {
    CollisionEnterEvent e = new CollisionEnterEvent();
    e.Type = QuarkEventType.CollisionEnter;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    e.OtherTag = col.gameObject.tag;
    e.CollisionPosition = col.GetContact(0).point;
    return e;
  }
}

[Serializable]
public class CollisionExitEvent : QuarkEvent {
  public Vector3 CollisionPosition;
  public string OtherTag;

  public static CollisionExitEvent Create(GameObject gameObject, Collision col) {
    CollisionExitEvent e = new CollisionExitEvent();
    e.Type = QuarkEventType.CollisionExit;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    e.OtherTag = col.gameObject.tag;
    return e;
  }
}

[Serializable]
public class TagEvent : QuarkEvent {
  public static TagEvent Create(GameObject gameObject) {
    TagEvent e = new TagEvent();
    e.Type = QuarkEventType.Tag;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    return e;
  }
}

[Serializable]
public class TaggingEvent : QuarkEvent {
  public string NewTag;

  public static TaggingEvent Create(GameObject gameObject, string NewTag) {
    TaggingEvent e = new TaggingEvent();
    e.Type = QuarkEventType.Tagging;
    e.Id = gameObject.GetInstanceID();
    e.Tag = gameObject.tag;
    e.NewTag = NewTag;
    return e;
  }
}
