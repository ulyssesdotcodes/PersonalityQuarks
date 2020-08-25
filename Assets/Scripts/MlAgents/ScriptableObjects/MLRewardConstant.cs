using UnityEngine;
using UnityEditor;
using System;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Constant")]
class MLRewardConstant : MLReward
{
    public string AmountKeyVal;

    private float Amount;
    Academy academy;

    public override void Initialize(BaseAgent agent)
    {
        academy = Academy.Instance;
        Amount = AcademyParameters.FetchOrParse(academy, AmountKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorAction, int deltaSteps)
    {
        Amount = AcademyParameters.Update(academy, AmountKeyVal, Amount);
        agent.AddReward(Amount * (float)deltaSteps / (float)agent.MaxStep);
    }
}
