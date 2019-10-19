using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Movement")]
class MLRewardMovement : MLReward {
    public int ForwardIdx = 0;
    public int RightIdx = 1;
    public int TurnIdx = 2;
    public float MultiplierForward;
    public float MultiplierRight;
    public float MultiplierTurn;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        agent.AddReward(Mathf.Abs(vectorActions[ForwardIdx]) * MultiplierForward / (float) agent.agentParameters.maxStep);
        agent.AddReward(Mathf.Abs(vectorActions[RightIdx]) * MultiplierRight / (float) agent.agentParameters.maxStep);
        agent.AddReward(Mathf.Abs(vectorActions[TurnIdx]) * MultiplierTurn / (float) agent.agentParameters.maxStep);
    }
}
