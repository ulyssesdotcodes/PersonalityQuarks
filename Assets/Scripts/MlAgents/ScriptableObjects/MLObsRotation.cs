using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Rotation")]
class MLObsRotation : MLObs {

    public override Option<float> FloatObs(Agent agent) {
        return agent.gameObject.transform.rotation.eulerAngles.y.SomeNotNull();
    }
}