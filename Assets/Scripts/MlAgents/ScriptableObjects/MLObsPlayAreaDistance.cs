using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="ML/Obs/Play Area Distance")]
class MLObsPlayAreaDistance : MLObs {
    public float PlayAreaDistance;

    public override Option<List<float>> FloatListObs(Agent agent) {
        return agent.gameObject.transform
            .SomeNotNull()  
            .Map(t => 
                (t.position.x > 0 ? 
                    GeneratePlayAreaDistance(t, new Vector2(PlayAreaDistance, t.position.y)) :
                    GeneratePlayAreaDistance(t, new Vector2(-PlayAreaDistance, t.position.y)))
                .Concat(t.position.y > 0 ? 
                    GeneratePlayAreaDistance(t, new Vector2(t.position.x, PlayAreaDistance)) :
                    GeneratePlayAreaDistance(t, new Vector2(t.position.x, -PlayAreaDistance))))
            .Map(ienum => new List<float>(ienum));
    }

    private List<float> GeneratePlayAreaDistance(Transform t, Vector2 p) {
        Vector2 ip = t.InverseTransformPoint(p);
        return new List<float>(new float[]{ ip.x, ip.y });
    }
}