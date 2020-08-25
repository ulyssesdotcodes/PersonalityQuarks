using System;
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Reach Target")]
class MLRewardReachTarget : MLReward
{
    public float Reward = 1;
    public float MaxSpeed = 1;
    public Rigidbody BaseRigidbody;
    public bool Reset = false;
    public Vector3 Target;
    public EnvironmentParameter Tolerence = 1f;

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if ((BaseRigidbody.transform.position - Target).sqrMagnitude < Tolerence * Tolerence)
        {
            if (Reset)
            {
                agent.AddReward(Reward);
                agent.EndEpisode();
            }
            else
            {
                agent.AddReward(Reward / (float)agent.MaxStep);
            }
        }
    }
}
