using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Tag Position")]
class MLObsTagPosition : MLObs
{
    public string Tag;
    public float PlayAreaDistance;
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

            Vector3 localPosition =
                agent.gameObject.transform.InverseTransformPoint(target.transform.position);
            observations.Add(localPosition.x / PlayAreaDistance);
            observations.Add(localPosition.y / PlayAreaDistance);
            observations.Add(localPosition.z / PlayAreaDistance);
        }

        sensor.AddObservation(observations);
    }
}
