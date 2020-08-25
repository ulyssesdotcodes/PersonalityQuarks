using UnityEngine;
using System.Collections.Generic;

// Maintains a count of the number of objects with a specific tag touching the collider
public class ClosestTagTarget : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] Targets;
    // Don't use Tag, can get confused with tag
    public string TargetTag;

    [HideInInspector]
    public GameObject Closest;

    public void Update()
    {
        Targets = GameObject.FindGameObjectsWithTag(TargetTag);

        Closest = null;

        float distance = 100000000f;
        foreach (GameObject go in Targets)
        {
            if ((go.transform.position - transform.position).sqrMagnitude < distance)
            {
                // Closest to the closest valid target
                distance = (go.transform.position - transform.position).sqrMagnitude;
                Closest = go;
            }
        }
    }
}