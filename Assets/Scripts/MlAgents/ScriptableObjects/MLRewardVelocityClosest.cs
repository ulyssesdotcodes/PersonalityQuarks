using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Velocity Closest")]
class MLRewardVelocityClosest : MLReward
{
    public float Amount = 1f;
    public string ChildTransform = "";
    private ClosestTagTarget closestTagTarget;
    private Transform targetTransform;
    private Rigidbody targetRb;

    public override void Initialize(BaseAgent agent)
    {
        closestTagTarget = agent.GetComponent<ClosestTagTarget>();
        if (ChildTransform == "")
        {
            targetTransform = agent.transform;
            targetRb = agent.GetComponent<Rigidbody>();
        }
        else
        {
            targetTransform = agent.transform.Find(ChildTransform);
            targetRb = targetTransform.GetComponent<Rigidbody>();
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (closestTagTarget.Closest != null)
        {
            Vector3 direction = closestTagTarget.Closest.transform.position - targetTransform.position;
            float distance = Vector3.Project(targetRb.velocity, direction).sqrMagnitude;
            agent.AddReward(Amount * distance / agent.MaxStep);
        }
    }
}
