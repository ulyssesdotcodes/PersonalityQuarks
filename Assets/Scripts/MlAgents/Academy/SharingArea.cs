using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingArea : Area
{
    public float TargetDistance;
    GameObject Target;
    GameObject RedActor;
    GameObject BlueActor;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Target = FindGameObjectsWithTagInChildren("target")[0];
        RedActor = FindGameObjectsWithTagInChildren("redactor")[0];
        BlueActor = FindGameObjectsWithTagInChildren("blueactor")[0];
    }

    public override void ResetArea() {
        RedActor.GetComponent<BaseAgent>().Logger.Log("Resetting area " + StartY);
        Target.transform.position = new Vector3(Random.Range(-TargetDistance, TargetDistance), StartY + 0.5f, Random.Range(-TargetDistance, TargetDistance));
        Target.GetComponent<ObservableFields>().LabelsHash.Remove("hitred");
        Target.GetComponent<ObservableFields>().LabelsHash.Remove("hitblue");

        BlueActor.GetComponent<BaseAgent>().Reset();
        RedActor.GetComponent<BaseAgent>().Reset();
    }
}
