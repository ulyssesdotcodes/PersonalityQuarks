using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Rotation")]
class MLObsRotation : MLObs {

    public override Option<float> FloatObs(BaseAgent agent) {
        return agent.gameObject.transform.localRotation.eulerAngles.y.SomeNotNull();
    }
}
