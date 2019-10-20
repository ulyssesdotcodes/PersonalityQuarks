using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Rotation")]
class MLObsTagRotation : MLObs {
    public string Tag;
    List<GameObject> TaggedObjects;

    public override void Initialize(BaseAgent agent) {
        TaggedObjects = agent.gameObject.GetComponentInParent<Area>().FindGameObjectsWithTagInChildren(Tag);
    }

    public override Option<List<float>> FloatListObs(BaseAgent agent) {
        List<float> observations = new List<float>();
        foreach(GameObject target in TaggedObjects) {
            if(target == agent.gameObject) {
                continue;
            }

            Vector3 localRotation = agent.gameObject.transform.InverseTransformDirection(
                target.transform.TransformDirection(Vector3.forward)
            );

            observations.Add(localRotation.x);
            observations.Add(localRotation.y);
            observations.Add(localRotation.z);
        }

        return observations.SomeNotNull();
    }
}
