using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Reset/Joint")]
class MLResetJoint : MLReset
{
    private ActionJoint actionJoint;
    private Quaternion rootRotation;

    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
        rootRotation = actionJoint.rootTransform.localRotation;
    }

    public override void Reset(BaseAgent agent)
    {
        foreach (var bodyPart in actionJoint.jointDriveController.bodyPartsDict.Values)
        {
            bodyPart.Reset(bodyPart);
        }

        actionJoint.rootTransform.localRotation = rootRotation;

        actionJoint.UpdateOrientationCube();
    }
}
