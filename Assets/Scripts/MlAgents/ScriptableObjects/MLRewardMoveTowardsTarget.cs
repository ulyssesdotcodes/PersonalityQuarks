using System;
using UnityEngine;
using Unity.MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Move Towards Target")]
class MLRewardMoveTowardsTarget : MLReward
{
    public float Reward = 1;
    public Transform BaseTransform;
    public Vector3 Target;
    public bool EndOnMoveAway = false;
    public float MoveAwayReward = -1f;
    private Vector3 lastPosition;

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (lastPosition == null)
        {
            lastPosition = BaseTransform.position;
            return;
        }

        // This has to be normalized to work properly.
        float offset = Vector3.Dot(
            (Target - BaseTransform.position).normalized,
            (BaseTransform.position - lastPosition).normalized
        );

        lastPosition = BaseTransform.position;

        agent.AddReward(Reward * offset * deltaSteps / agent.MaxStep);

        if (offset < 0 && EndOnMoveAway)
        {
            agent.AddReward(MoveAwayReward);
            agent.EndEpisode();
        }

    }
}
