using System;
using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;
using OptionalUnity;

public class BaseAgent : Agent, IResettable {
    public Logger Logger;
    public MLReset[] Resets;
    public MLObs[] Observations;
    public MLReward[] Rewards;
    public MLAction[] Actions;

    private List<MLReset> myResets = new List<MLReset>();
    private List<MLObs> myObservations = new List<MLObs>();
    private List<MLReward> myRewards = new List<MLReward>();
    private List<MLAction> myActions = new List<MLAction>();

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
            MLReset r = MLReset.Instantiate(reset);
            r.Initialize(this);
            myResets.Add(r);
        }

        foreach (MLObs obs in Observations) {
            MLObs o = MLObs.Instantiate(obs);
            o.Initialize(this);
            myObservations.Add(o);
        }

        foreach (MLReward reward in Rewards) {
            MLReward r = MLReward.Instantiate(reward);
            r.Initialize(this);
            myRewards.Add(r);
        }

        foreach (MLAction action in Actions) {
            MLAction a = MLAction.Instantiate(action);
            a.Initialize(this);
            myActions.Add(a);
        }

    }

    public override void CollectObservations() {
        foreach (MLObs obs in myObservations) {
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
        foreach (MLReward reward in myRewards) {
            reward.AddReward(this, vectorAction);
        }

        foreach(MLAction action in myActions) {
            action.RunAction(this, vectorAction);
        }
    }

    public override void AgentReset() {
        base.AgentReset();
        Reset();
    }

    public void Reset()
    {
        Logger.Log(String.Concat("Reset ", gameObject.transform.position.y));
        foreach(MLReset reset in myResets) {
            reset.Reset(this);
        }
    }

    public void OnTriggerEnter(Collider col) {
        Logger.Log(String.Concat("Trigger Enter ", col.transform.position.y));
        Logger.Log(String.Concat("Trigger Enter ", col.gameObject.tag));
        TriggerCollider = col.Some();
    }

    public void OnTriggerExit(Collider col) {
        Logger.Log(String.Concat("Trigger Exit ", gameObject.transform.position.y));
        TriggerCollider = Option.None<Collider>();
    }
}
