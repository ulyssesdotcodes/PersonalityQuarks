using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Ray Perceive")]
class MLObsRayPerceive : MLObs {
    public string[] DetectableObjects;
    public float[] rayAngles = { 0f, 20f, 90f, 160f, 45f, 135f, 70f, 110f, 180f };
    public float rayDistance = 50f;
    public float offsetX = 0f;
    public float offsetY = 0f;

    public override Option<List<float>> FloatListObs(Agent agent) {
        RayPerception rayPer = agent.gameObject.GetComponent<RayPerception>();
        return Option.Some(rayPer.Perceive(rayDistance, rayAngles, DetectableObjects, offsetX, offsetY));
    }
}