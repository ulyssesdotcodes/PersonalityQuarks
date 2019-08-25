using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Leave Playarea")]
class MLRewardLeavePlayarea : MLReward {
    public float Amount;
    public float LimitX;
    public float LimitZ;
    public float LimitY;
    public float PositionY;
    public bool Reset;
    public bool Done;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        bool isOut = false;
        if(Mathf.Abs(agent.gameObject.transform.position.x) > LimitX) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(Mathf.Abs(agent.gameObject.transform.localPosition.y - PositionY) > LimitY) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(Mathf.Abs(agent.gameObject.transform.position.z) > LimitZ) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(isOut && Done) {
            agent.Done();
        }

        if(isOut && Reset) {
            agent.Reset();
        }
    }
}
