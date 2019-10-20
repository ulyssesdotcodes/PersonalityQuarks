using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperArea : Area
{
    public float TargetDistance;
    GameObject RedTarget;
    GameObject RedActor;
    GameObject BlueActor;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        RedTarget = FindGameObjectsWithTagInChildren("redtarget")[0];
        RedActor = FindGameObjectsWithTagInChildren("redactor")[0];
        /* BlueActor = FindGameObjectsWithTagInChildren("blueactor")[0]; */
    }

    public override void ResetArea() {
        Logger.Log("Resetting area " + StartY);
        RedTarget.transform.position = new Vector3(Random.Range(-TargetDistance, TargetDistance), StartY + 0.5f, Random.Range(-TargetDistance, TargetDistance));
        RedTarget.GetComponent<ObservableFields>().LabelsHash.Remove("hit");

        /* BlueActor.GetComponent<BaseAgent>().Reset(); */
        RedActor.GetComponent<BaseAgent>().Reset();
    }
}
