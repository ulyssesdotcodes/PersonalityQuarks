using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Tag Rotation")]
class MLObsTagRotation : MLObs
{
    public string Tag;
    List<GameObject> TaggedObjects;

    public override void Initialize(BaseAgent agent)
    {
        TaggedObjects = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(Tag);
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        List<float> observations = new List<float>();
        foreach (GameObject target in TaggedObjects)
        {
            if (target == agent.gameObject)
            {
                continue;
            }

            Vector3 localRotation = agent.gameObject.transform.InverseTransformDirection(
                target.transform.TransformDirection(Vector3.forward)
            );

            observations.Add(localRotation.x);
            observations.Add(localRotation.y);
            observations.Add(localRotation.z);
        }

        sensor.AddObservation(observations);
    }
}
