using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Relative Velocity")]
class MLObsRelativeVelocity : MLObs
{
    public string BaseTransformName = "";
    public string TargetRigidbodyName = "";
    public bool ObserveXZ = true;
    public bool ObserveY = false;
    private Transform baseTransform;
    private Rigidbody targetRigidbody;
    public override void Initialize(BaseAgent agent)
    {
        if (BaseTransformName == "")
        {
            baseTransform = agent.transform.parent;
        }
        else
        {
            baseTransform = agent.transform.Find(BaseTransformName);
        }

        if (TargetRigidbodyName == "")
        {
            targetRigidbody = agent.GetComponent<Rigidbody>();
        }
        else
        {
            targetRigidbody = agent.transform.Find(TargetRigidbodyName).GetComponent<Rigidbody>();
        }
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        Vector3 inverseVelocity = baseTransform.InverseTransformDirection(targetRigidbody.velocity);

        if (ObserveXZ)
        {
            sensor.AddObservation(new Vector2(inverseVelocity.x, inverseVelocity.z));
        }

        if (ObserveY)
        {
            sensor.AddObservation(inverseVelocity.y);
        }
    }
}
