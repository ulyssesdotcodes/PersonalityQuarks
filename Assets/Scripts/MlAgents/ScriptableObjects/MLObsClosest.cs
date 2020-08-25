using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Closest")]
class MLObsClosest : MLObs
{
    public string ClosestTagChild = "";
    public string BaseTransformName = "";
    public float PlayArea = 1f;
    private ClosestTagTarget closestTagTarget;
    private Transform baseTransform;

    public override void Initialize(BaseAgent agent)
    {
        if (ClosestTagChild == "")
        {
            closestTagTarget = agent.GetComponent<ClosestTagTarget>();
        }
        else
        {
            closestTagTarget = agent.transform.Find(ClosestTagChild).GetComponent<ClosestTagTarget>();
        }

        if (BaseTransformName == "")
        {
            baseTransform = agent.transform;
        }
        else
        {
            baseTransform = agent.transform.Find(BaseTransformName);
        }
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        if (closestTagTarget.Closest != null)
        {
            sensor.AddObservation(baseTransform.InverseTransformPoint(closestTagTarget.Closest.transform.position) / PlayArea);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
        }
    }
}
