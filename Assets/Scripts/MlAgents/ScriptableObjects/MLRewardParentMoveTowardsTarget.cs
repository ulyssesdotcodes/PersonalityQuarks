using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Parent Move Towards Target")]
class MLRewardParentMoveTowardsTarget : MLRewardMoveTowardsTarget
{
    private MoveToTarget moveToTarget;

    public override void Initialize(BaseAgent agent)
    {
        BaseTransform = agent.transform.parent;
        moveToTarget = agent.GetComponent<MoveToTarget>();
        base.Initialize(agent);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        Target = moveToTarget.Target;
        base.AddReward(agent, vectorActions, deltaSteps);
    }
}
