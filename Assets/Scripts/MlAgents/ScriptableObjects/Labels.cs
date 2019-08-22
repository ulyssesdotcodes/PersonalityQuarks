using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class Labels : MonoBehaviour {
    public HashSet<string> LabelsHash;

    public void Start() {
        LabelsHash = new HashSet<string>();
    }
}
