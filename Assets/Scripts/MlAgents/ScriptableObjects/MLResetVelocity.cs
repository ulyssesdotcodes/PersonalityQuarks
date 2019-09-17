using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Reset/Velocity")]
class MLResetVelocity : MLReset {

    public override void Reset(BaseAgent agent) {
        Rigidbody rb = agent.gameObject.GetComponent<Rigidbody>();
        if(rb == null) return;

        rb.velocity = Vector3.zero;
    }
}
