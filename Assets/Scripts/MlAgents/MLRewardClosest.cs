using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Closest")]
class MLRewardClosest : MLReward
{
    public float Amount = 1f;
    public float MinDistance = 1f;
    public string ChildTransform = "";
    private ClosestTagTarget closestTagTarget;
    private Transform targetTransform;

    public override void Initialize(BaseAgent agent)
    {
        if (ChildTransform == "")
        {
            closestTagTarget = agent.GetComponent<ClosestTagTarget>(); ;
            targetTransform = agent.transform;
        }
        else
        {
            targetTransform = agent.transform.Find(ChildTransform);
            closestTagTarget = targetTransform.GetComponent<ClosestTagTarget>();
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (closestTagTarget.Closest != null)
        {
            float distance = (closestTagTarget.Closest.transform.position - targetTransform.position).sqrMagnitude;
            agent.AddReward(Amount * Mathf.Min(1, MinDistance / distance) * deltaSteps / agent.MaxStep);
        }
    }
}
