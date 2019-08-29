using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public float StartY;

    protected virtual void Start()
    {
        StartY = gameObject.transform.position.y;
    }


    public virtual void ResetArea()
    {

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
