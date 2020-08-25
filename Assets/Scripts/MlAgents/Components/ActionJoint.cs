using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class ActionJoint : MonoBehaviour
{
    public Transform rootTransform;
    public List<Transform> transforms;
    [HideInInspector]
    public JointDriveController jointDriveController;
    public OrientationCubeController orientationCube;

    // TODO: Remove
    private ClosestTagTarget target;
    private PersonalityQuarksArea area;

    public void Start()
    {
        area = GetComponentInParent<PersonalityQuarksArea>();
        target = GetComponent<ClosestTagTarget>();
        jointDriveController = GetComponent<JointDriveController>();
        transforms = rootTransform.GetComponentsInChildren<ConfigurableJoint>().Select(x => x.gameObject.transform).ToList();
        // jointDriveController.SetupBodyPart(rootTransform);
        foreach (Transform t in transforms)
        {
            jointDriveController.SetupBodyPart(t);
        }
    }

    public void FixedUpdate()
    {
        UpdateOrientationCube();
    }

    public void UpdateOrientationCube()
    {
        if (target.Closest != null)
        {
            orientationCube.UpdateOrientation(rootTransform, target.Closest.transform);
        }
    }
}