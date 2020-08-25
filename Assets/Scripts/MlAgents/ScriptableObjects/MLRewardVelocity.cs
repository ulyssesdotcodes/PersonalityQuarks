using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Velocity")]
class MLRewardVelocity : MLReward
{
    public float Multiplier;

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        Rigidbody rb = agent.gameObject.GetComponent<Rigidbody>();

        if (rb == null) return;

        agent.AddReward(Mathf.Abs(rb.velocity.x) * Multiplier / (float)agent.MaxStep);
        agent.AddReward(Mathf.Abs(rb.velocity.z) * Multiplier / (float)agent.MaxStep);
    }
}
