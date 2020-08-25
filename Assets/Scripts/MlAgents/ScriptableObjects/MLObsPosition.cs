using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Position")]
class MLObsPosition : MLObs
{
    // For normalizing observations
    public float MaxDistance = 1f;
    public bool ObserveXZ = true;
    public bool ObserveY = false;

    public Transform BaseTransform;
    public Vector3 TargetPosition;

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        Vector3 inversePostion = BaseTransform.InverseTransformPoint(TargetPosition);
        if (ObserveXZ)
        {
            sensor.AddObservation(new Vector2(inversePostion.x / MaxDistance, inversePostion.z / MaxDistance));
        }

        if (ObserveY)
        {
            sensor.AddObservation(inversePostion.y / MaxDistance);
        }
    }
}
