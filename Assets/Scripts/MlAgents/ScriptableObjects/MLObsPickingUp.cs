using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Picking Up")]
class MLObsPickingUp : MLObs
{
    public string ColliderName;
    public int MaxCount = 1;
    private TagPickup tagPickup;

    public override void Initialize(BaseAgent agent)
    {
        tagPickup = agent.transform.Find(ColliderName).GetComponent<TagPickup>();
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        sensor.AddObservation(tagPickup.IsDropped());
        sensor.AddObservation(Mathf.Min(tagPickup.Count(), MaxCount));
    }
}
