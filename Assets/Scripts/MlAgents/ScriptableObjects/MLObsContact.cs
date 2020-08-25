using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Contact")]
class MLObsContact : MLObs
{
    public string ColliderName;
    private TagContact tagContact;

    public override void Initialize(BaseAgent agent)
    {
        tagContact = agent.transform.Find(ColliderName).GetComponent<TagContact>();
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        sensor.AddObservation(tagContact.Touching.Count > 0);
    }
}
