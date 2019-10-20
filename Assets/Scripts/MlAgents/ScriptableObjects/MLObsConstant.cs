using UnityEngine;
using MLAgents;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Obs/Constant")]
class MLObsConstant : MLObs {
    public float obs;

    public override Option<float> FloatObs(BaseAgent agent) {
        return obs.SomeNotNull();
    }
}
