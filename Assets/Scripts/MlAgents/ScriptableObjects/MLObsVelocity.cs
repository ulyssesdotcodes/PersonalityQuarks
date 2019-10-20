using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Velocity")]
class MLObsVelocity : MLObs {

    public override Option<Vector2> Vec2Obs(BaseAgent agent) {
        return agent.gameObject.GetComponent<Rigidbody>()
            .SomeNotNull()  
            .Map(rb => new Vector2(rb.velocity.x, rb.velocity.z));
    }
}
