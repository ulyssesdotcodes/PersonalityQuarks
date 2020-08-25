using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Obs/Constant")]
class MLObsConstant : MLObs
{
    public float obs;
    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        sensor.AddObservation(obs);
    }
}
