using System.Collections.Generic;
using UnityEngine;

public class Spherical : QuarkEventListener {
  private Dictionary<int, GameObject> InstanceMap;
  public float Radius = 20f;
  public float GroundWidth = 20f;
  public float GroundHeight = 20f;

  public void Start() {
    InstanceMap = new Dictionary<int, GameObject>();
  }

  public override int Id {
    get { return -1; }
  }

  public override void OnEvent(QuarkEvent e) {
    switch(e.Type) {
      case QuarkEventType.Transform:
        TransformEvent te = (TransformEvent) e;
        GameObject gob = GetOrCreate(te.Id);
        gob.transform.localPosition = SphericalToEuclidean(te.Position);
        gob.transform.localRotation = te.Rotation;
        gob.transform.localScale = te.Scale;
        Debug.DrawLine(te.Position, gob.transform.position);
        break;
    }
  }

  private Vector3 SphericalToEuclidean(Vector3 pos) {
    float theta = Mathf.PI * (0.5f + pos.x / GroundWidth);
    float theotherone = Mathf.PI * (1 + pos.z * 2 / GroundHeight);
    return new Vector3(
        Radius * Mathf.Cos(theta) * Mathf.Sin(theotherone), 
        Radius * Mathf.Sin(theta) * Mathf.Sin(theotherone),
        Radius * Mathf.Cos(theotherone)
    );
  }

  private GameObject GetOrCreate(int id) {
    if(InstanceMap.ContainsKey(id)) {
      return InstanceMap[id];
    }

    GameObject gob = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    gob.transform.SetParent(gameObject.transform);
    InstanceMap.Add(id, gob);
    return gob;
  }
}
