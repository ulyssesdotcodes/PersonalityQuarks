using UnityEngine;
using UnityEditor;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Constant")]
class MLRewardConstant : MLReward {
    public float Amount;

    public override void AddReward(BaseAgent agent, float[] vectorAction) {
        agent.AddReward(Amount);
    }
}