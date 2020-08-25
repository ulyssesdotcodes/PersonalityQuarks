using UnityEngine;
using Unity.MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Velocity Towards Target")]
class MLRewardVelocityTowardsTarget : MLReward
{
    public float Reward = 1;
    public Rigidbody BaseRigidbody;
    public Vector3 Target;
    public float MaxVelocity;
    public bool EndOnMoveAway = false;
    public float MoveAwayReward = -1f;

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        // Normalize to calculate properly
        float offset = Vector3.Dot(
            (Target - BaseRigidbody.transform.position).normalized,
            BaseRigidbody.velocity.normalized
        );

        // Then multiple by the velocity
        offset *= BaseRigidbody.velocity.sqrMagnitude / (MaxVelocity * MaxVelocity);

        agent.AddReward(Reward * offset / (float)agent.MaxStep);

        if (offset < 0 && EndOnMoveAway)
        {
            agent.AddReward(MoveAwayReward);
            agent.EndEpisode();
        }
    }
}
