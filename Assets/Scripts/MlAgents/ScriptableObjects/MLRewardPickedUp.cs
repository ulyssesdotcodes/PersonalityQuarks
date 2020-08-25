using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/PickedUp")]
class MLRewardPickedUp : MLReward
{
    public float Amount = 1f;
    public string ChildTransform = "";
    private TagPickup tagPickup;

    public override void Initialize(BaseAgent agent)
    {
        if (ChildTransform == "")
        {
            tagPickup = agent.GetComponent<TagPickup>(); ;
        }
        else
        {
            tagPickup = agent.transform.Find(ChildTransform).GetComponent<TagPickup>(); ;
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        agent.AddReward(Amount * tagPickup.Count());
    }
}
