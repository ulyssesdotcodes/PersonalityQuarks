using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Label If Tag")]
class MLRewardStartWithLabelIfTag : MLReward
{
    public string Tag;
    public string Label;

    public override void Initialize(BaseAgent agent)
    {
        if (agent.gameObject.CompareTag(Tag))
        {
            agent.GetComponent<ObservableFields>().FieldsHash.Add(Label, Time.time);
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        // Empty method feels weird
    }
}
