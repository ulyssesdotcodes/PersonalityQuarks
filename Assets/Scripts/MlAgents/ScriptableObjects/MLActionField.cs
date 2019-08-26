using System;
using UnityEngine;
using System.Collections.Generic;
using MLAgents;

[CreateAssetMenu(menuName="ML/Actions/Field")]
class MLActionField : MLAction {
    public int StartIdx;
    public int NumFields = 1;
    public string FieldName;
    public int BranchSize = 8;

    private ObservableFields Fields;

    public override void Initialize(BaseAgent agent) {
        Fields = agent.gameObject.GetComponent<ObservableFields>();
        if(Fields.FieldsHash == null) {
            Fields.FieldsHash = new Dictionary<string, float>();
        }
        for (int i = 0; i < NumFields; i++) {
            Fields.FieldsHash.Add(String.Concat(FieldName, i), 0);
        }
    }

    public override void RunAction(BaseAgent agent, float[] act) {
        GameObject gameObject = agent.gameObject;

        for (int i = 0; i < NumFields; i++) {
            string Field = String.Concat(FieldName, i);
            float value = 0;

            if(Fields.FieldsHash.ContainsKey(Field)) {
                Fields.FieldsHash.Remove(Field);
            }
            
            if (agent.brain.brainParameters.vectorActionSpaceType == SpaceType.continuous) {
                value = act[StartIdx + i];
            } else {
                value = (float) act[StartIdx + i] / BranchSize;
            }

            Fields.FieldsHash.Add(Field, value);
        }

    }
}
