using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ML/Groups/Move To Target")]
class MLGroupMoveToTarget : QuarkGroup
{
    public float PlayAreaDistance;
    public float Speed = 3f;
    public float SpeedVariance = 0.3f;
    public float MaxSpeed = 5f;
    public bool ResetRotation;
    public bool Rotate;
    public bool MoveY = false;
    public float RewardTowards = 0.5f;
    public float RewardReach = 1f;
    public bool ReachResets = true;
    public bool MoveAwayResets = true;
    public bool ResetPosition = true;

    private MoveToTarget moveToTarget;
    private MLObsPosition obsPosition;
    private MLObsVelocity obsVelocity;
    private MLRewardVelocityTowardsTarget rewardVelocityTowardsTarget;
    private MLRewardReachTarget rewardReachTarget;

    public override void Initialize(BaseAgent agent)
    {
        moveToTarget = agent.GetComponent<MoveToTarget>();


        // Reset position and velocity

        MLResetRandom resetPosition = ScriptableObject.CreateInstance<MLResetRandom>();
        resetPosition.PlayAreaDistanceKeyVal = PlayAreaDistance.ToString();
        resetPosition.ResetPositionXZ = true;
        resetPosition.ResetRotation = ResetRotation;
        resetPosition.ResetPositionY = MoveY;
        resetPosition.TargetTransform = agent.transform;

        if (ResetPosition)
        {
            myResets.Add(resetPosition);
        }

        // Observe target position, target velocity, agent velocity

        obsPosition = ScriptableObject.CreateInstance<MLObsPosition>();
        obsPosition.BaseTransform = agent.transform;
        obsPosition.ObserveXZ = true;
        obsPosition.ObserveY = MoveY;
        obsPosition.MaxDistance = PlayAreaDistance;
        myObservations.Add(obsPosition);

        obsVelocity = ScriptableObject.CreateInstance<MLObsVelocity>();
        obsVelocity.BaseTransform = agent.transform;
        obsVelocity.Rigidbody = agent.GetComponent<Rigidbody>();
        obsVelocity.MaxVelocity = MaxSpeed;
        obsVelocity.ObserveY = MoveY;
        myObservations.Add(obsVelocity);

        MLObsRandom obsRandom = ScriptableObject.CreateInstance<MLObsRandom>();
        myObservations.Add(obsRandom);


        // Reward agent velocity towards target, agent reaching target, constant negative

        rewardVelocityTowardsTarget = ScriptableObject.CreateInstance<MLRewardVelocityTowardsTarget>();
        rewardVelocityTowardsTarget.BaseRigidbody = agent.GetComponent<Rigidbody>();
        rewardVelocityTowardsTarget.Reward = RewardTowards;
        rewardVelocityTowardsTarget.MaxVelocity = MaxSpeed;
        rewardVelocityTowardsTarget.EndOnMoveAway = MoveAwayResets;
        myRewards.Add(rewardVelocityTowardsTarget);

        MLRewardConstant negative = ScriptableObject.CreateInstance<MLRewardConstant>();
        negative.AmountKeyVal = "-1";
        myRewards.Add(negative);

        rewardReachTarget = ScriptableObject.CreateInstance<MLRewardReachTarget>();
        rewardReachTarget.BaseRigidbody = agent.GetComponent<Rigidbody>();
        rewardReachTarget.MaxSpeed = MaxSpeed;
        rewardReachTarget.Reset = ReachResets;
        rewardReachTarget.Reward = RewardReach;
        myRewards.Add(rewardReachTarget);


        // Act movement

        MLActionMove actionMove = ScriptableObject.CreateInstance<MLActionMove>();
        actionMove.rigidbody = agent.GetComponent<Rigidbody>();
        actionMove.Speed = Speed;
        actionMove.SpeedVariance = SpeedVariance;
        actionMove.MaxSpeed = MaxSpeed;
        actionMove.MoveY = MoveY;
        myActions.Add(actionMove);

        base.Initialize(agent);
    }

    public override void Reset(BaseAgent agent)
    {
        base.Reset(agent);
    }

    public override void CollectObservations(BaseAgent agent, Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        base.CollectObservations(agent, sensor);
        sensor.AddObservation(moveToTarget.Tolerence);
    }

    public override void FixedUpdate()
    {
        obsPosition.TargetPosition = moveToTarget.Target;
        rewardVelocityTowardsTarget.Target = moveToTarget.Target;
        rewardReachTarget.Target = moveToTarget.Target;
        rewardReachTarget.Tolerence = moveToTarget.Tolerence;
    }
}