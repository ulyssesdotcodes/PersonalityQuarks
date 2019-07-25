using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="ML/Obs/Play Area Circle Distance")]
class MLObsPlayAreaCircleDistance : MLObs {
    public float PlayAreaDistance;

    public override Option<float> FloatObs(Agent agent) {
        return agent.gameObject.transform
            .SomeNotNull()  
            .Map(t => 
                (new Vector2(t.position.x, t.position.y)).sqrMagnitude / 
                (PlayAreaDistance * PlayAreaDistance)
            );
    }
}