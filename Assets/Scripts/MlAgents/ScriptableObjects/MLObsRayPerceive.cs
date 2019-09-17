using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Ray Perceive")]
class MLObsRayPerceive : MLObs {
    public string[] DetectableObjects;
    public float[] rayAngles = {20f, 90f, 160f, 45f, 135f, 70f, 110f};
    public float rayDistance = 50f;
    public float startOffset = 0f;
    public float endOffset = 0f;

    public override Option<List<float>> FloatListObs(Agent agent) {
        RayPerception rayPer = agent.gameObject.GetComponent<RayPerception>();
        return Option.Some(rayPer.Perceive(rayDistance, rayAngles, DetectableObjects, startOffset, endOffset));
    }
}
