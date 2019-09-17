using UnityEngine;
using UnityEditor;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Constant")]
class MLRewardConstant : MLReward {
    public string AmountKeyVal;

    private float Amount;
    Academy academy;

    public override void Initialize(BaseAgent agent) {
        academy = FindObjectOfType<Academy>();
        Amount = AcademyParameters.FetchOrParse(academy, AmountKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorAction) {
        Amount = AcademyParameters.Update(academy, AmountKeyVal, Amount);
        agent.AddReward(Amount / (float)agent.agentParameters.maxStep);
    }
}
