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
        Debug.Log(RedTarget.tag);
        BlueTarget = GameObject.FindGameObjectsWithTag("bluetarget")[0];
    }


    public override void AcademyReset()
    {
        RedTarget.transform.position = new Vector3(Random.Range(-PlayAreaDistance, PlayAreaDistance), PositionY, Random.Range(-PlayAreaDistance, PlayAreaDistance));
        RedTarget.transform.rotation = Quaternion.identity;
        RedTarget.transform.Rotate(0, Random.Range(0, 360), 0);

        BlueTarget.transform.position = new Vector3(Random.Range(-PlayAreaDistance, PlayAreaDistance), PositionY, Random.Range(-PlayAreaDistance, PlayAreaDistance));
        BlueTarget.transform.rotation = Quaternion.identity;
        BlueTarget.transform.Rotate(0, Random.Range(0, 360), 0);
    }
}
