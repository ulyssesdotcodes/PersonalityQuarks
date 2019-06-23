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
            .Map(t => GeneratePlayAreaDistance(t, new Vector2(0, PlayAreaDistance))
                .Concat(GeneratePlayAreaDistance(t, new Vector2(0, -PlayAreaDistance)))
                .Concat(GeneratePlayAreaDistance(t, new Vector2(PlayAreaDistance, 0)))
                .Concat(GeneratePlayAreaDistance(t, new Vector2(-PlayAreaDistance, 0)))
                )
            .Map(ienum => new List<float>(ienum));
    }

    private List<float> GeneratePlayAreaDistance(Transform t, Vector2 p) {
        Vector2 ip = t.InverseTransformPoint(p);
        return new List<float>(new float[]{ ip.x, ip.y });
    }
}