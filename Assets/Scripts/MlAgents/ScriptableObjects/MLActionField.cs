using UnityEngine;
using System.Collections.Generic;
using MLAgents;

[CreateAssetMenu(menuName="ML/Actions/Field")]
class MLActionField : MLAction {
    public int idx = 0;
    public string Field = "action";
    public int BranchSize = 8;

    private ObservableFields Fields;

    public override void Initialize(BaseAgent agent) {
        Fields = agent.gameObject.GetComponent<ObservableFields>();
        if(Fields.FieldsHash == null) {
            Fields.FieldsHash = new Dictionary<string, float>();
        }
        Fields.FieldsHash.Add(Field, 0);
    }

    public override void RunAction(BaseAgent agent, float[] act) {
        GameObject gameObject = agent.gameObject;
        if(idx >= act.Length) {
            return;
        } 

        float value = 0;

        if(Fields.FieldsHash.ContainsKey(Field)) {
            Fields.FieldsHash.Remove(Field);
        }
        
        if (agent.brain.brainParameters.vectorActionSpaceType == SpaceType.continuous) {
            value = act[idx];
        } else {
            value = (float) act[idx] / BranchSize;
        }

        Fields.FieldsHash.Add(Field, value);
    }
}
