using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Reset/Velocity")]
class MLResetRandom : MLReset {
    public float PlayAreaDistance;

    private override void Reset(BaseAgent agent) {
        Rigidbody rb = agent.gameObject.GetComponent<Rigidbody>();
        if(rb == null) return;

        rb.velocity = Vector3.zero;
    }
}