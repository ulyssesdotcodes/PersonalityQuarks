using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class ObservableFields : MonoBehaviour {
    public HashSet<string> LabelsHash;
    public Dictionary<string, float> FieldsHash;

    public void Start() {
        if(LabelsHash == null) {
            LabelsHash = new HashSet<string>();
        }

        if(FieldsHash == null) {
            FieldsHash = new Dictionary<string,float>();
        }
    }
}
