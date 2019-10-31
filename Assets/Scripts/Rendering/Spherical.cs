using System.Collections.Generic;
using UnityEngine;

public class Spherical : QuarkEventListener {
  private Dictionary<int, GameObject> InstanceMap;
  public float Radius = 20f;
  public float GroundWidth = 80f;
  public float GroundHeight = 80f;

  public void Start() {
    InstanceMap = new Dictionary<int, GameObject>();
  }

  public override int Id {
    get { return -1; }
  }

  public override void OnEvent(QuarkEvent e) {
    switch(e.Type) {
      case QuarkEventType.Create:
        CreateEvent ce = (CreateEvent) e;
        GameObject gob = GameObject.CreatePrimitive(ce.Tag == "actor" ? PrimitiveType.Sphere : PrimitiveType.Cube);
        gob.transform.SetParent(gameObject.transform);
        gob.transform.localPosition = SphericalToEuclidean(ce.Position);
        gob.transform.localRotation = ce.Rotation;
        gob.transform.localScale = ce.Scale;
        gob.tag = ce.Tag;
        RenderedAgent renderedAgent = gob.AddComponent<RenderedAgent>();
        renderedAgent.EventSystem = this.EventSystem;
        renderedAgent.AddId(ce.Id);
        InstanceMap.Add(ce.Id, gob);
        break;
    }
  }

  private Vector3 SphericalToEuclidean(Vector3 pos) {
    float theta = Mathf.PI * (0.5f + pos.x / GroundWidth);
    float theotherone = 2 * Mathf.PI * (0.5f + pos.z / GroundHeight);
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
