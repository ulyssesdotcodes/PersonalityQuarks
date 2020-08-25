using UnityEngine;
using UnityEditor;
using System;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Transform Contact")]
class MLRewardTransformContact : MLReward
{
    public float Amount;
    public Transform TargetTransform;
    public TagContact[] TagContacts;
    public bool Reset;

    public override void AddReward(BaseAgent agent, float[] vectorAction, int deltaSteps)
    {
        foreach (TagContact tagContact in TagContacts)
        {
            if (tagContact.transform == TargetTransform)
            {
                agent.AddReward(Amount * tagContact.Touching.Count / (float)agent.MaxStep);

                if (Reset)
                {
                    agent.EndEpisode();
                }
            }
        }
    }
}
