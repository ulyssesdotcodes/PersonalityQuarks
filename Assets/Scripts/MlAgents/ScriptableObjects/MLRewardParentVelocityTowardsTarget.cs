using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Parent Velocity Towards Target")]
class MLRewardParentVelocityTowardsTarget : MLRewardVelocityTowardsTarget
{
    private MoveToTarget moveToTarget;

    public override void Initialize(BaseAgent agent)
    {
        BaseRigidbody = agent.transform.parent.GetComponent<Rigidbody>();
        moveToTarget = agent.GetComponent<MoveToTarget>();
        base.Initialize(agent);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        Target = moveToTarget.Target;
        base.AddReward(agent, vectorActions, deltaSteps);
    }
}
