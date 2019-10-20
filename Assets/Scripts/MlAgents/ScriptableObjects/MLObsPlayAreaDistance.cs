using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName="ML/Obs/Play Area Distance")]
class MLObsPlayAreaDistance : MLObs {
    public float PlayAreaDistance;

    public override Option<List<float>> FloatListObs(BaseAgent agent) {
        return agent.gameObject.transform
            .SomeNotNull()  
            /* .Map(t => t.position) */
            /* .Map(p => new List<float>(new float[] { Mathf.Abs(p.x) / PlayAreaDistance, Mathf.Abs(p.z) / PlayAreaDistance })) */
            /* .Map(x => { */
            /*     Debug.Log(x[0]); */
            /*     Debug.Log(x[1]); */
            /*     return x; */
            /* }) */
            /* .Map(r => r.Concat(r)) */
            /* .Map(ienum => new List<float>(ienum)); */
            .Map(t => 
                (t.position.x > 0 ? 
                    GeneratePlayAreaDistance(t, new Vector3(PlayAreaDistance, 0, t.position.z)) :
                    GeneratePlayAreaDistance(t, new Vector3(-PlayAreaDistance, 0, t.position.z)))
                .Concat(t.position.y > 0 ? 
                    GeneratePlayAreaDistance(t, new Vector3(t.position.x, 0, PlayAreaDistance)) :
                    GeneratePlayAreaDistance(t, new Vector3(t.position.x, 0, -PlayAreaDistance))))
            .Map(ienum => new List<float>(ienum));
    }

    private List<float> GeneratePlayAreaDistance(Transform t, Vector3 p) {
        Vector3 ip = t.InverseTransformPoint(p);
        return new List<float>(new float[]{ ip.x / PlayAreaDistance, ip.z / PlayAreaDistance });
    }
}
