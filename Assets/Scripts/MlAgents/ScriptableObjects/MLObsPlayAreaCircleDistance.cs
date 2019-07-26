using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="ML/Obs/Play Area Circle Distance")]
class MLObsPlayAreaCircleDistance : MLObs {
    public float PlayAreaDistance;

    public override Option<List<float>> FloatListObs(Agent agent) {
        return agent.gameObject.transform
            .SomeNotNull()  
            .Map(t => new List<float>(){ t.position.x, t.position.y, t.position.z
            });
    }
}