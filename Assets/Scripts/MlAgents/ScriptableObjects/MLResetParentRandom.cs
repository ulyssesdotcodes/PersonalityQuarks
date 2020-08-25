using UnityEngine;

[CreateAssetMenu(menuName = "ML/Reset/Parent Random")]
class MLResetParentRandom : MLResetRandom
{
    public override void Initialize(BaseAgent agent)
    {
        TargetTransform = agent.transform.parent;
    }
}