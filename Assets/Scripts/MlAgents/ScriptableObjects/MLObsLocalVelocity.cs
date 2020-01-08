using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Local Velocity")]
class MLObsLocalVelocity : MLObs {
    public override Option<Vector3> Vec3Obs(BaseAgent agent) {
        return agent.gameObject.GetComponent<Rigidbody>()
            .SomeNotNull()  
            .Map(rb => agent.gameObject.transform.InverseTransformDirection(rb.velocity))
            .Map(vel => new Vector3(vel.x, vel.y, vel.z));
    }
}
