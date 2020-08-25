using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Obs/Joint")]
class MLObsJoint : MLObs
{
    ActionJoint actionJoint;
    public override void Initialize(BaseAgent agent)
    {
        actionJoint = agent.GetComponent<ActionJoint>();
    }
    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        //Add body rotation delta relative to orientation cube
        sensor.AddObservation(Quaternion.FromToRotation(actionJoint.rootTransform.forward, actionJoint.orientationCube.transform.forward));

        RaycastHit hit;
        float maxRaycastDist = 10;
        if (Physics.Raycast(actionJoint.rootTransform.position, Vector3.down, out hit, maxRaycastDist))
        {
            sensor.AddObservation(hit.distance / maxRaycastDist);
        }
        else
            sensor.AddObservation(1);

        foreach (var bodyPart in actionJoint.jointDriveController.bodyPartsList)
        {
            CollectObservationBodyPart(bodyPart, sensor);
        }
    }
    public void CollectObservationBodyPart(BodyPart bp, VectorSensor sensor)
    {
        //GROUND CHECK
        sensor.AddObservation(bp.groundContact.touchingGround); // Is this bp touching the ground

        //Get velocities in the context of our orientation cube's space
        //Note: You can get these velocities in world space as well but it may not train as well.
        sensor.AddObservation(actionJoint.orientationCube.transform.InverseTransformDirection(bp.rb.velocity));
        sensor.AddObservation(actionJoint.orientationCube.transform.InverseTransformDirection(bp.rb.angularVelocity));

        //Get position relative to hips in the context of our orientation cube's space
        sensor.AddObservation(actionJoint.orientationCube.transform.InverseTransformDirection(bp.rb.position - actionJoint.rootTransform.position));

        if (bp.rb.transform != actionJoint.rootTransform)
        {
            sensor.AddObservation(bp.rb.transform.localRotation);
            sensor.AddObservation(bp.currentStrength / actionJoint.jointDriveController.maxJointForceLimit);
        }
    }
}
