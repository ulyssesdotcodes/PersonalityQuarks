using UnityEngine;
using UnityEditor;
using System;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Tag Contact")]
class MLRewardTagContact : MLReward
{
    public string ColliderName;
    public float Amount;
    private TagContact[] tagContacts;

    public override void Initialize(BaseAgent agent)
    {
        tagContacts = agent.transform.Find(ColliderName).GetComponents<TagContact>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorAction, int deltaSteps)
    {
        foreach (TagContact tagContact in tagContacts)
        {
            agent.AddReward(Amount * tagContact.Touching.Count / (float)agent.MaxStep);
        }
    }
}
