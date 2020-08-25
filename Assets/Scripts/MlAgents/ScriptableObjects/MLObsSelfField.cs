using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Self Field")]
class MLObsSelfField : MLObs
{
    public string FieldName;
    public bool ObserveAsBool = true;

    private ObservableFields fields;

    public override void Initialize(BaseAgent agent)
    {
        fields = agent.GetComponent<ObservableFields>();
    }

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        if (ObserveAsBool)
        {

            sensor.AddObservation(fields.FieldsHash.ContainsKey(FieldName) ? 1f : 0f);
        }
        else
        {
            float f = -1;
            fields.FieldsHash.TryGetValue(FieldName, out f);
            sensor.AddObservation(f);
        }
    }
}
