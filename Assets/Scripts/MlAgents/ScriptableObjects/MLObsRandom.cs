using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Obs/Random")]
public class MLObsRandom : MLObs
{
    [HideInInspector]
    public float value;

    public override void Initialize(BaseAgent agent)
    {
        value = Random.value;
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        sensor.AddObservation(value);
    }

    public override void Reset(BaseAgent agent)
    {
        value = Random.value;
    }

}
