using System;
using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;
using OptionalUnity;

public class BaseAgent : Agent, IResettable {
    public Logger Logger;
    public QuarkGroup Quarks;
    /* public MLReset[] Resets; */
    /* public MLObs[] Observations; */
    /* public MLReward[] Rewards; */
    /* public MLAction[] Actions; */

    /* private List<MLReset> myResets = new List<MLReset>(); */
    /* private List<MLObs> myObservations = new List<MLObs>(); */
    /* private List<MLReward> myRewards = new List<MLReward>(); */
    /* private List<MLAction> myActions = new List<MLAction>(); */
    /* private List<QuarkGroup> myGroups = new List<MLAction>(); */

    public Option<Collider> TriggerCollider;

    public List<string> ColliderTags = new List<string>();

    private Vector3 StartPosition;
    private Quaternion StartRotation;

    public void Start() {
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        Quarks = QuarkGroup.Instantiate(Quarks);
        Quarks.Initialize(this);

        /* foreach(MLReset reset in Resets) { */
        /*     MLReset r = (MLReset) ScriptableObject.Instantiate((ScriptableObject) reset); */
        /*     r.Initialize(this); */
        /*     myResets.Add(r); */
        /* } */

        /* foreach (MLObs obs in Observations) { */
        /*     MLObs o = (MLObs) ScriptableObject.Instantiate((ScriptableObject) obs); */
        /*     o.Initialize(this); */
        /*     myObservations.Add(o); */
        /* } */

        /* foreach (MLReward reward in Rewards) { */
        /*     MLReward r = (MLReward) ScriptableObject.Instantiate((ScriptableObject) reward); */
        /*     r.Initialize(this); */
        /*     myRewards.Add(r); */
        /* } */

        /* foreach (MLAction action in Actions) { */
        /*     MLAction a = (MLAction) ScriptableObject.Instantiate((ScriptableObject) action); */
        /*     a.Initialize(this); */
        /*     myActions.Add(a); */
        /* } */

        /* foreach (QuarkGroup group in Actions) { */
        /*     MLAction a = (MLAction) ScriptableObject.Instantiate((ScriptableObject) action); */
        /*     a.Initialize(this); */
        /*     myActions.Add(a); */
        /* } */
    }

    public override void CollectObservations() {
        AddVectorObs(Quarks.CollectObservations(this));
        /* foreach (MLObs obs in myObservations) { */
        /*     AddVectorObs(obs.CollectObservations(this)); */
        /* } */
    }

    public override void AgentAction(float[] vectorAction, string textAction) {
        Quarks.AgentAction(this, vectorAction);
        /* foreach (MLReward reward in myRewards) { */
        /*     reward.AddReward(this, vectorAction); */
        /* } */

        /* foreach(MLAction action in myActions) { */
        /*     action.RunAction(this, vectorAction); */
        /* } */
    }

    public override void AgentReset() {
        base.AgentReset();
        Reset();
    }

    public void Reset()
    {
        Logger.Log(String.Concat("Reset ", gameObject.transform.position.y));
        Quarks.Reset(this);
        /* foreach(MLReset reset in myResets) { */
        /*     reset.Reset(this); */
        /* } */
    }

    public void OnTriggerEnter(Collider col) {
        Logger.Log(String.Concat("Trigger Enter ", col.transform.position.y));
        Logger.Log(String.Concat("Trigger Enter ", col.gameObject.tag));
        if(ColliderTags.Contains(col.gameObject.tag)) {
          TriggerCollider = col.Some();
        }
    }

    public void OnTriggerExit(Collider col) {
        Logger.Log(String.Concat("Trigger Exit ", gameObject.transform.position.y));
        if(ColliderTags.Contains(col.gameObject.tag)) {
          TriggerCollider = Option.None<Collider>();
        }
    }

    public void OnCollisionEnter(Collision col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          Logger.Log(String.Concat("Collision Enter ", col.gameObject.tag));
          TriggerCollider = col.collider.Some();
        }
    }

    public void OnCollisionExit(Collision col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          Logger.Log(String.Concat("Collision Exit ", gameObject.transform.position.y));
          TriggerCollider = Option.None<Collider>();
        }
    }
}
