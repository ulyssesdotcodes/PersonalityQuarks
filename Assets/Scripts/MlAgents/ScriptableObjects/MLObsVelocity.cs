using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Velocity")]
class MLObsVelocity : MLObs
{
    public Transform BaseTransform;
    public Rigidbody Rigidbody;
    public float MaxVelocity;
    public bool ObserveXZ = true;
    public bool ObserveY = false;

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        Vector3 inverseVelocity = BaseTransform.InverseTransformDirection(Rigidbody.velocity);
        if (ObserveXZ)
        {
            sensor.AddObservation(new Vector2(inverseVelocity.x, inverseVelocity.z) / MaxVelocity);
        }

        if (ObserveY)
        {
            sensor.AddObservation(inverseVelocity.y / MaxVelocity);
        }
    }
}
