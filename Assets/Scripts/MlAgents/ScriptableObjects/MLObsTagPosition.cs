using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Position")]
class MLObsTagPosition : MLObs {
    public string Tag;
    public float PlayAreaDistance;

    private GameObject[] TaggedObjects;

    public override void Initialize() {
        TaggedObjects = GameObject.FindGameObjectsWithTag(Tag);
    }

    public override Option<List<float>> FloatListObs(Agent agent) {
        List<float> observations = new List<float>();
        foreach(GameObject target in TaggedObjects) {
            if(target == agent.gameObject) {
                continue;
            }

            Vector3 localPosition = 
                agent.gameObject.transform.InverseTransformPoint(target.transform.position);
            observations.Add(localPosition.x / PlayAreaDistance);
            observations.Add(localPosition.y / PlayAreaDistance);
            observations.Add(localPosition.z / PlayAreaDistance);
        }

        return observations.SomeNotNull();
    }
}