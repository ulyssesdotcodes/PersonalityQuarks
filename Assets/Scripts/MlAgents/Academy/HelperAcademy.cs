using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class HelperAcademy : Academy
{
    public float PlayAreaDistance; 
    public float PositionY;

    private GameObject RedTarget;
    private GameObject BlueTarget;

    public override void InitializeAcademy() {
        RedTarget = GameObject.FindGameObjectsWithTag("redtarget")[0];
        BlueTarget = GameObject.FindGameObjectsWithTag("bluetarget")[0];
    }


    public override void AcademyReset()
    {
        RedTarget.transform.position = new Vector3(Random.Range(-PlayAreaDistance, PlayAreaDistance), PositionY, Random.Range(-PlayAreaDistance, PlayAreaDistance));
        RedTarget.GetComponent<ObservableFields>().LabelsHash.Clear();
        RedTarget.GetComponent<ObservableFields>().FieldsHash.Clear();

        BlueTarget.transform.position = new Vector3(Random.Range(-PlayAreaDistance, PlayAreaDistance), PositionY, Random.Range(-PlayAreaDistance, PlayAreaDistance));
        BlueTarget.GetComponent<ObservableFields>().LabelsHash.Clear();
        BlueTarget.GetComponent<ObservableFields>().FieldsHash.Clear();
    }
}
