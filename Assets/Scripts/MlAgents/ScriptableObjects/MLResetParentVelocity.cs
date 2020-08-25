using UnityEngine;

[CreateAssetMenu(menuName = "ML/Reset/Parent Velocity")]
class MLResetParentVelocity : MLResetVelocity
{
    public override void Initialize(BaseAgent agent)
    {
        Rigidbody = agent.transform.parent.GetComponent<Rigidbody>();
    }
}