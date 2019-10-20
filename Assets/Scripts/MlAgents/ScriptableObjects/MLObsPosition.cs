using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Position")]
class MLObsPosition : MLObs {

    public override Option<Vector2> Vec2Obs(BaseAgent agent) {
        return Option.Some(new Vector2(agent.gameObject.transform.position.x, agent.gameObject.transform.position.z));
    }
}
