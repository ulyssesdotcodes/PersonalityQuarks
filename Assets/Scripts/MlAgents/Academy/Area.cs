using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Area : MonoBehaviour
{
  [HideInInspector]
  public float StartY;

  [HideInInspector]
  public Academy academy;

  public QuarkEvents EventSystem;

  public AreaReset[] AreaResets;

  public Logger Logger;
  public Canvas WorldCanvas;

  protected virtual void Start()
  {
    StartY = gameObject.transform.position.y;
    academy = FindObjectOfType<Academy>();

    Logger.Start(EventSystem); 

    foreach(AreaReset areaReset in AreaResets) {
      areaReset.Init(this);
    }
  }


  public virtual void ResetArea()
  {
    foreach(AreaReset areaReset in AreaResets) {
      areaReset.ResetArea(this);
    }
  }
  
  public List<GameObject> FindGameObjectsWithTagInChildren(string tag) {
      GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
      List<GameObject> res = new List<GameObject>();

      foreach(GameObject go in gos) {
          if (go.GetComponentInParent<Area>() == this) {
              res.Add(go);
          }
      }

      return res;
  }
}
