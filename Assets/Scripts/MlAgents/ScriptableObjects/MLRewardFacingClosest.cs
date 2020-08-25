using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Facing Closest")]
class MLRewardFacingClosest : MLReward
{
    public float Amount = 1f;
    private ClosestTagTarget closestTagTarget;

    public override void Initialize(BaseAgent agent)
    {
        closestTagTarget = agent.GetComponent<ClosestTagTarget>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (closestTagTarget.Closest != null)
        {
            float facing = Vector3.Dot(agent.transform.forward, closestTagTarget.Closest.transform.position - agent.transform.position);
            agent.AddReward(Amount * facing * deltaSteps / agent.MaxStep);
        }
    }
}
