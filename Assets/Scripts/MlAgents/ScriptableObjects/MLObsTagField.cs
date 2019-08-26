using System;
using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Field")]
class MLObsTagField : MLObs {
    public string Tag;
    public int NumFields = 1;
    public string FieldName;

    private List<ObservableFields> TaggedObjectFields;

    public override void Initialize() {
        TaggedObjectFields = new List<ObservableFields>();
        foreach(GameObject target in GameObject.FindGameObjectsWithTag(Tag)) {
            if(target.GetComponent<ObservableFields>() != null) {
                TaggedObjectFields.Add(target.GetComponent<ObservableFields>());
            }
        }
    }

    public override Option<List<float>> FloatListObs(Agent agent) {
        List<float> observations = new List<float>();
        foreach(ObservableFields target in TaggedObjectFields) {
            if(target.gameObject == agent.gameObject) {
                continue;
            }

            for(int i = 0; i < NumFields; i++) {
                float f = 0;
                target.FieldsHash.TryGetValue(String.Concat(FieldName, i), out f);
                /* Debug.Log(String.Concat(target.gameObject.tag, ": ", FieldName, i, ": ", f)); */
                observations.Add(f);
            }
        }

        return observations.SomeNotNull();
    }
}
