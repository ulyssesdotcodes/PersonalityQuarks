using UnityEngine;

[CreateAssetMenu(menuName = "ML/Obs/Parent Chosen Position")]
class MLObsParentChosenPosition : MLObsPosition
{
    private MoveToTarget moveToTarget;
    public override void Initialize(BaseAgent agent)
    {
        moveToTarget = agent.GetComponent<MoveToTarget>();
        base.BaseTransform = agent.transform.parent;
        base.Initialize(agent);
    }

    public override void CollectObservations(BaseAgent agent, Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        base.TargetPosition = moveToTarget.Target;
        sensor.AddObservation(moveToTarget.Tolerence);
        base.CollectObservations(agent, sensor);
    }
}