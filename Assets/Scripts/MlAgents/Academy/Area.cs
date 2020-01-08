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

  [HideInInspector]
  public QuarkEvents EventSystem;

  public AreaReset[] AreaResets;
  private List<AreaReset> AreaResetClones;

  public Logger Logger;
  public Canvas WorldCanvas;

  private int lastReset;

  protected virtual void Start()
  {
    EventSystem = GetComponent<QuarkEvents>();

    StartY = gameObject.transform.position.y;
    academy = FindObjectOfType<Academy>();

    AreaResetClones = new List<AreaReset>();
    foreach(AreaReset areaReset in AreaResets) {
      AreaReset ar = Object.Instantiate(areaReset) as AreaReset;
      AreaResetClones.Add(ar);
    }
  }


  public virtual void ResetArea()
  {
    if(Time.frameCount > 0 && lastReset != Time.frameCount) {
      foreach(AreaReset areaReset in AreaResetClones) {
        areaReset.Init(this);
      }
    }

    lastReset = Time.frameCount;
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
