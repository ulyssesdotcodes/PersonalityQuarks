using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Movement")]
class MLRewardMovement : MLReward {
    public int ForwardIdx = 0;
    public int TurnIdx = 1;
    public float MultiplierForward;
    public float MultiplierTurn;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        agent.AddReward(Mathf.Abs(vectorActions[ForwardIdx]) * MultiplierForward);
        agent.AddReward(Mathf.Abs(vectorActions[TurnIdx]) * MultiplierTurn);
    }
}