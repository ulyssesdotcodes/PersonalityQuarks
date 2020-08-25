using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class ObservableFields : MonoBehaviour
{
    public HashSet<string> LabelsHash = new HashSet<string>();
    public Dictionary<string, float> FieldsHash = new Dictionary<string, float>();

    public void Start()
    {
    }
}
