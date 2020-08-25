using System;
using UnityEngine;
using Unity.MLAgents;
using System.Collections;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Self Label")]
class MLRewardSelfLabel : MLReward
{
    public string Label;

    public float Reward = 0;

    public override void Initialize(BaseAgent agent)
    {
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (agent.GetComponent<ObservableFields>().FieldsHash.ContainsKey(Label))
        {
            agent.AddReward(Reward / (float)agent.MaxStep);
        }
    }
}
