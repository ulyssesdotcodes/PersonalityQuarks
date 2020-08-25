using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "ML/Actions/Joint")]
public class MLActionJoint : MLAction
{
    public int StartIdx = 0;
    private ActionJoint actionJoint;


    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
    }

    public override void RunAction(BaseAgent agent, float[] vectorAction)
    {
        // The dictionary with all the body parts in it are in the jdController
        var bpDict = actionJoint.jointDriveController.bodyPartsDict;

        var i = StartIdx - 1;
        // Pick a new target joint rotation
        foreach (Transform t in actionJoint.transforms)
        {
            float zrotation = 0;
            if (t.gameObject.GetComponent<ConfigurableJoint>().angularZMotion == ConfigurableJointMotion.Limited)
            {
                zrotation = vectorAction[++i];
            }
            bpDict[t].SetJointTargetRotation(vectorAction[++i], 0, zrotation);
        }

        foreach (Transform t in actionJoint.transforms)
        {
            bpDict[t].SetJointStrength(vectorAction[++i]);
        }
    }
}
