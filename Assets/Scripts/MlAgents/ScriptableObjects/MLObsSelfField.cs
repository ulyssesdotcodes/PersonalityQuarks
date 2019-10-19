using System;
using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Self Field")]
class MLObsSelfField : MLObs {
    public string FieldName;
    public bool ObserveAsBool = true;

    public override void Initialize(BaseAgent agent) {
    }

    public override Option<float> FloatObs(Agent agent) {
      if(ObserveAsBool) {
        return (agent.GetComponent<ObservableFields>().FieldsHash.ContainsKey(FieldName) ? 1f : 0f)
          .SomeNotNull();
      } else {
        float f = 0;
        agent.GetComponent<ObservableFields>().FieldsHash.TryGetValue(FieldName, out f);
        return f.SomeNotNull();
      }
    }
}
