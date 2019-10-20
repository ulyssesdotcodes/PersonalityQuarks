using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Local Velocity")]
class MLObsLocalVelocity : MLObs {
    public override Option<Vector2> Vec2Obs(BaseAgent agent) {
        return agent.gameObject.GetComponent<Rigidbody>()
            .SomeNotNull()  
            .Map(rb => agent.gameObject.transform.InverseTransformDirection(rb.velocity))
            .Map(vel => new Vector2(vel.x, vel.z));
    }
}
