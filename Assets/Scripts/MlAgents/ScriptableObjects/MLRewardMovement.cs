using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Movement")]
class MLRewardMovement : MLReward
{
    public int ForwardIdx = 0;
    public int RightIdx = 1;
    public int TurnIdx = 2;
    public float MultiplierForward;
    public float MultiplierRight;
    public float MultiplierTurn;

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        agent.AddReward(Mathf.Abs(vectorActions[ForwardIdx]) * MultiplierForward / (float)agent.MaxStep);
        agent.AddReward(Mathf.Abs(vectorActions[RightIdx]) * MultiplierRight / (float)agent.MaxStep);
        agent.AddReward(Mathf.Abs(vectorActions[TurnIdx]) * MultiplierTurn / (float)agent.MaxStep);
    }
}
