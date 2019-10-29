using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDoorArea : Area
{
    public float TargetDistance;
    GameObject RedActor;
    GameObject BlueActor;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        /* RedTarget = FindGameObjectsWithTagInChildren("redtarget")[0]; */
        /* BlueTarget = FindGameObjectsWithTagInChildren("bluetarget")[0]; */
        RedActor = FindGameObjectsWithTagInChildren("redactor")[0];
        BlueActor = FindGameObjectsWithTagInChildren("blueactor")[0];
    }

    public override void ResetArea() {
        float redResetTime = base.academy.resetParameters["red_hidden_time"];
        float blueResetTime = base.academy.resetParameters["blue_hidden_time"];
        /* Logger.Log(System.String.Concat("Red reset time: ", redResetTime)); */
        /* Logger.Log(System.String.Concat("Blue reset time: ", blueResetTime)); */
        

        /* Logger.Log("Resetting area " + StartY); */

        /* RedTarget.transform.position = new Vector3(Random.Range(-TargetDistance, TargetDistance), StartY + 0.5f, Random.Range(-TargetDistance, TargetDistance)); */
        /* RedTarget.GetComponent<ObservableFields>().LabelsHash.Remove("hit"); */
        /* StartCoroutine(DisableEnableCollider(RedTarget.GetComponent<Collider>(), Random.Range(0, redResetTime))); */
        /* StartCoroutine(DisableEnableRenderer(RedTarget.GetComponent<Renderer>(), Random.Range(0, redResetTime))); */

        /* BlueTarget.transform.position = new Vector3(Random.Range(-TargetDistance, TargetDistance), StartY + 0.5f, Random.Range(-TargetDistance, TargetDistance)); */
        /* BlueTarget.GetComponent<ObservableFields>().LabelsHash.Remove("hit"); */
        /* StartCoroutine(DisableEnableCollider(BlueTarget.GetComponent<Collider>(), Random.Range(0, blueResetTime))); */
        /* StartCoroutine(DisableEnableRenderer(BlueTarget.GetComponent<Renderer>(), Random.Range(0, blueResetTime))); */
    }

    private IEnumerator DisableEnableCollider(Collider comp, float seconds) {
        comp.enabled = false;
        yield return new WaitForSeconds(seconds);
        /* Logger.Log(System.String.Concat("Enabling: ", comp, " after ", seconds)); */
        comp.enabled = true;
    }

    private IEnumerator DisableEnableRenderer(Renderer comp, float seconds) {
        comp.enabled = false;
        yield return new WaitForSeconds(seconds);
        comp.enabled = true;
    }
}
