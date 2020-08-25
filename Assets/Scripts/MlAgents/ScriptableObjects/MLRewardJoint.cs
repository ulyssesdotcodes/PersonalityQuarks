using System;
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Joint")]
class MLRewardJoint : MLReward
{
    ActionJoint actionJoint;

    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        var movingTowardsDot = Vector3.Dot(actionJoint.orientationCube.transform.forward,
            Vector3.ClampMagnitude(actionJoint.jointDriveController.bodyPartsDict[actionJoint.rootTransform].rb.velocity, 10));
        ;

        agent.AddReward(0.03f * movingTowardsDot);
        agent.AddReward(0.01f * Vector3.Dot(actionJoint.orientationCube.transform.forward, actionJoint.rootTransform.forward));
    }
}
