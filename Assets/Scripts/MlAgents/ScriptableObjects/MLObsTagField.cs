using System;
using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Obs/Tag Field")]
class MLObsTagField : MLObs {
    public bool ShowFields;
    public Logger FieldLogger;
    public string Tag;
    public int NumFields = 1;
    public string FieldName;

    private List<ObservableFields> TaggedObjectFields;
    private Area area;

    public override void Initialize(BaseAgent agent) {
        area = agent.gameObject.GetComponentInParent<Area>();
        TaggedObjectFields = new List<ObservableFields>();
        foreach(GameObject target in area.FindGameObjectsWithTagInChildren(Tag)) {
            if(target.GetComponent<ObservableFields>() != null) {
                FieldLogger.Log(String.Concat("Adding tog"));
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
                if(ShowFields) {
                    var level = new List<string>(){".", ".", ".", "."};
                    level[(int)(f * 4)] = "|";
                    agent.gameObject.GetComponent<LabelCanvas>().AddText(String.Concat(FieldName, i), String.Join("", level));
                }
                observations.Add(f);
            }
        }

        return observations.SomeNotNull();
    }
}
