using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Leave Circle Playarea")]
class MLRewardLeaveCirclePlayarea : MLReward {
    public float Amount;
    public float LimitRadius;
    public float LimitY;
    public float PositionY;
    public bool Reset;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        bool isOut = false;

        if((new Vector2(agent.transform.position.x, agent.transform.position
            .z)).sqrMagnitude > LimitRadius * LimitRadius) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(Mathf.Abs(agent.gameObject.transform.localPosition.y - PositionY) > LimitY) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(isOut && Reset) {
            agent.Done();
            agent.Reset();
        }
    }
}