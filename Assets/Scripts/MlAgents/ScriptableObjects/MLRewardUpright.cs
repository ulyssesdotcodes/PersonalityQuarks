using UnityEngine;
using UnityEditor;
using System;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Upright")]
class MLRewardUpright : MLReward
{
    public float Amount;
    ActionJoint actionJoint;

    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorAction, int deltaSteps)
    {
        agent.AddReward(Amount * Vector3.Dot(actionJoint.rootTransform.forward, Vector3.up * -1f) / (float)agent.MaxStep);
    }
}
