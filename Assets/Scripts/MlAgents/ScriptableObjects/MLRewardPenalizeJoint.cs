using System;
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Penalize Joint Movement")]
class MLRewardPenalizeJoint : MLReward
{
    ActionJoint actionJoint;

    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        agent.AddReward(-0.00000001f * actionJoint.jointDriveController.GetCurrentForceTorque());
    }
}
