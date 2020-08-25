using UnityEngine;

[CreateAssetMenu(menuName = "ML/Reset/Parent")]
class MLResetParent : MLReset
{
    public override void Initialize(BaseAgent agent)
    {
    }

    public override void Reset(BaseAgent agent)
    {
        // agent.transform.parent.GetComponent<BaseAgent>().RequestDecision();
        agent.transform.parent.GetComponent<BaseAgent>().EndEpisode();
        agent.transform.parent.GetComponent<BaseAgent>().OnEpisodeBegin();
        // agent.RequestDecision();
    }
}