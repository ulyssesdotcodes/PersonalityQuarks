using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Rotation")]
class MLObsRotation : MLObs
{
    public string ChildTransform = "";
    public bool ObserveX = false;
    public bool ObserveY = true;
    public bool ObserveZ = false;

    private Transform targetTransform;

    public override void Initialize(BaseAgent agent)
    {
        if (ChildTransform == "")
        {
            targetTransform = agent.transform;
        }
        else
        {
            targetTransform = agent.transform.Find(ChildTransform);
        }
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        Vector3 eulerAngles = targetTransform.localRotation.eulerAngles;

        if (ObserveX)
        {
            sensor.AddObservation(eulerAngles.y);
        }

        if (ObserveY)
        {
            sensor.AddObservation(eulerAngles.y);
        }

        if (ObserveZ)
        {
            sensor.AddObservation(eulerAngles.z);
        }
    }
}
