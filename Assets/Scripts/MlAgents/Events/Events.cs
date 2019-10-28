using System;
using UnityEngine;

public enum QuarkEventType {
  All,
  Transform,
  Collision,
  Tag
}

[Serializable]
public class QuarkEvent : System.Object {
  public QuarkEventType Type;
  public int Id;
}

[Serializable]
public class TransformEvent : QuarkEvent {
  public Vector3 Position;
  public Quaternion Rotation;
  public Vector3 Scale;
  
  public static TransformEvent Create(int id, Vector3 pos, Quaternion rot, Vector3 Scale) {
    TransformEvent e = new TransformEvent();
    e.Type = QuarkEventType.Transform;
    e.Id = id;
    e.Position = pos;
    e.Rotation = rot;
    e.Scale = Scale;
    return e;
  }
}

[Serializable]
public class CollisionEvent : QuarkEvent {
  public int OtherGameObjectId;
}

[Serializable]
public class TagEvent : QuarkEvent {
  public string Tag;
}
