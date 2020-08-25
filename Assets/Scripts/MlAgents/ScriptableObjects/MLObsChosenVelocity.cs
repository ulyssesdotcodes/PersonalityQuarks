using UnityEngine;

[CreateAssetMenu(menuName = "ML/Obs/Chosen Velocity")]
class MLObsChosenVelocity : MLObsVelocity
{
    ChooseNewTarget chooseNewTarget;
    public override void Initialize(BaseAgent agent)
    {
        BaseTransform = agent.transform.parent;
        chooseNewTarget = agent.GetComponent<ChooseNewTarget>();
        base.Initialize(agent);
    }

    public override void CollectObservations(BaseAgent agent, Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        Rigidbody = chooseNewTarget.target.GetComponent<Rigidbody>();
        base.CollectObservations(agent, sensor);
    }
}