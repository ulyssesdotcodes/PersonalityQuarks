
using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Target Velocity Towards Closest")]
class MLRewardTargetVelocityTowardClosest : MLReward
{
    public float Reward = 1f;
    public string TargetRigidbodyName = "";
    private ClosestTagTarget closestTagTarget;
    private Rigidbody targetRigidbody;

    public override void Initialize(BaseAgent agent)
    {
        if (TargetRigidbodyName == "")
        {
            closestTagTarget = agent.GetComponent<ClosestTagTarget>(); ;
            targetRigidbody = agent.GetComponent<Rigidbody>();
        }
        else
        {
            targetRigidbody = agent.transform.Find(TargetRigidbodyName).GetComponent<Rigidbody>();
            closestTagTarget = targetRigidbody.GetComponent<ClosestTagTarget>();
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (closestTagTarget.Closest != null)
        {
            float offset = Vector3.Dot(
                closestTagTarget.Closest.transform.position - targetRigidbody.transform.position,
                targetRigidbody.velocity
            );

            agent.AddReward(Reward * offset / (float)agent.MaxStep);
        }
    }
}
