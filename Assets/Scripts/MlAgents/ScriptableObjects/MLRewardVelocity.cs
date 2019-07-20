using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Velocity")]
class MLRewardVelocity : MLReward {
    public float Multiplier;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        Rigidbody rb = agent.gameObject.GetComponent<Rigidbody>();
        
        if(rb == null) return;

        agent.AddReward(rb.velocity.x * Multiplier);
        agent.AddReward(rb.velocity.z * Multiplier);
    }
}