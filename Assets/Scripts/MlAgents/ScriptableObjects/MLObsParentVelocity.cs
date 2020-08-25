using UnityEngine;

[CreateAssetMenu(menuName = "ML/Obs/Parent Velocity")]
class MLObsParentVelocity : MLObsVelocity
{
    public override void Initialize(BaseAgent agent)
    {
        BaseTransform = agent.transform.parent;
        Rigidbody = agent.transform.parent.GetComponent<Rigidbody>();
        base.Initialize(agent);
    }
}