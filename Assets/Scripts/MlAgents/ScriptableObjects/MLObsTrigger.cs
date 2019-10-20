using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Trigger")]
class MLObsTrigger : MLObs {
    public string Tag;

    public override Option<float> FloatObs(BaseAgent agent) {
        return ((BaseAgent)agent).TriggerCollider
            .Filter(tc => tc != null)
            .Map(tc => tc.gameObject)
            .Filter(tc => tc.gameObject.tag == Tag)
            .Map(tc => 1f)
            .Else(0f.Some());
    }
}
