using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Reset/Velocity")]
class MLResetVelocity : MLReset
{
    public Rigidbody Rigidbody;

    public override void Reset(BaseAgent agent)
    {
        Rigidbody.velocity = Vector3.zero;
    }
}
