using System.Collections;
using UnityEngine;

public class RenderedAgent : QuarkEventListener {
  private int id = -2;
  public float Radius = 20f;
  public float GroundWidth = 80f;
  public float GroundHeight = 80f;

  public override int Id {
    get { return this.id; }
  }

  public void AddId(int id) {
    this.id = id;
    this.EventSystem.AddListener(this);
  }

  public override void OnEvent(QuarkEvent e) {
    switch(e.Type) {
      case QuarkEventType.Transform:
        TransformEvent te = (TransformEvent) e;
        transform.localPosition = SphericalToEuclidean(te.Position);
        transform.localRotation = te.Rotation;
        transform.localScale = te.Scale;
        /* Debug.DrawLine(te.Position, transform.position); */
        break;
      case QuarkEventType.CollisionEnter:
        StartCoroutine(AnimateColorRed());
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


  IEnumerator AnimateColorRed() {
    float startTime = Time.time;
    float endTime = startTime + 0.5f;
    while(Time.time < endTime) {
      gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, (Time.time - startTime) / 0.5f);
      yield return null;
    }
  }
}
