using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Actions/Field")]
class MLActionField : MLAction {
    public int idx = 0;
    public string Field = "action";

    private ObservableFields Fields;

    public override void Initialize(BaseAgent agent) {
        Fields = agent.gameObject.GetComponent<ObservableFields>();
        if(Fields.FieldsHash == null) {
            Fields.FieldsHash = new Dictionary<string, float>();
        }
        Fields.FieldsHash.Add(Field, 0);
    }

    public override void RunAction(float[] vectorActions, GameObject gameObject) {
        if(idx >= vectorActions.Length) {
            return;
        }
        
        if(Fields.FieldsHash.ContainsKey(Field)) {
            Fields.FieldsHash.Remove(Field);
        }

        Fields.FieldsHash.Add(Field, vectorActions[idx]);
    }
}
