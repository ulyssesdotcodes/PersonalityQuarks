using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Field")]
class MLObsTagField : MLObs {
    public string Tag;
    public string Field;

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
            if(target.gameObject == agent.gameObject || !target.FieldsHash.ContainsKey(Field)) {
                continue;
            }

            float f = 0;
            target.FieldsHash.TryGetValue(Field, out f);
            observations.Add(f);
        }

        return observations.SomeNotNull();
    }
}
