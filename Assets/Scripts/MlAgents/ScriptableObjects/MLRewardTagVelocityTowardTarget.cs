using UnityEngine;
using Unity.MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Tag Velocity Toward Target")]
class MLRewardTagVelocityTowardTarget : MLReward
{
    public string Tag;
    public string TargetTransformName = "";
    public float Reward = 1;

    private Transform targetTransform;

    public override void Initialize(BaseAgent agent)
    {
        if (TargetTransformName == "")
        {
            targetTransform = agent.transform;
        }
        else
        {
            targetTransform = agent.transform.Find(TargetTransformName);
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(Tag);

        foreach (GameObject go in taggedObjects)
        {
            Rigidbody goRb = go.GetComponent<Rigidbody>();
            float offset = Vector3.Dot(
                targetTransform.position - go.transform.position,
                goRb.velocity
            );
            agent.AddReward(Reward * offset * deltaSteps / (float)agent.MaxStep);
        }
    }
}
