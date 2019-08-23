using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;
using OptionalUnity;

public class BaseAgent : Agent, IResettable {
    public MLReset[] Resets;
    public MLObs[] Observations;
    public MLReward[] Rewards;
    public MLAction[] Actions;

    public Option<Collider> TriggerCollider;

    private Vector3 StartPosition;
    private Quaternion StartRotation;

    public void Start() {
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        foreach(MLReset reset in Resets) {
            reset.Initialize(this);
        }

        foreach (MLObs obs in Observations) {
            obs.Initialize();
        }

        foreach (MLReward reward in Rewards) {
            reward.Initialize();
        }

        foreach (MLAction action in Actions) {
            action.Initialize(this);
        }

    }

    public override void CollectObservations() {
        foreach (MLObs obs in Observations) {

            obs.IntObs(this).MatchSome(io => AddVectorObs(io));
            obs.FloatObs(this).MatchSome(io => AddVectorObs(io));
            obs.Vec2Obs(this).MatchSome(io => AddVectorObs(io));
            obs.Vec3Obs(this).MatchSome(io => AddVectorObs(io));
            obs.FloatArrObs(this).MatchSome(io => AddVectorObs(io));
            obs.FloatListObs(this).MatchSome(io => AddVectorObs(io));
            obs.QuatObs(this).MatchSome(io => AddVectorObs(io));
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction) {
        foreach (MLReward reward in Rewards) {
            reward.AddReward(this, vectorAction);
        }

        foreach(MLAction action in Actions) {
            action.RunAction(vectorAction, this.gameObject);
        }
    }

    public override void AgentReset() {
        base.AgentReset();
        Reset();
    }

    public void Reset()
    {
        foreach(MLReset reset in Resets) {
            reset.Reset(this);
        }
    }

    public void OnTriggerEnter(Collider col) {
        TriggerCollider = col.Some();
    }

    public void OnTriggerExit(Collider col) {
        TriggerCollider = Option.None<Collider>();
    }
}
