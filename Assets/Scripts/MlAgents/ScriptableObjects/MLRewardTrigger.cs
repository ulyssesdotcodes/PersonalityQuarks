using UnityEngine;
using MLAgents;
using OptionalUnity;
using System;

[CreateAssetMenu(menuName="ML/Rewards/Trigger")]
class MLRewardTrigger : MLReward {
    public string RequiresTag;
    public string Tag;
    public string NewTag;
    public string Label;
    public string LabelPrevents;
    public string RewardKeyVal = "0";
    public string ContinuousRewardKeyVal = "0";
    public string PenaltyKeyVal = "0";
    public bool Remove = true;
    public bool RemoveOnLeave = true;
    public bool Reset = true;
    public bool Done = false;
    public bool Toggle = false;
    public bool ResetAreaIfAlreadyThere = false;
    public bool DoneIfAlreadyThere = false;
    public bool ResetArea = true;
    public float Cooldown = 5f;
    public bool RunResetMessage = false;

    public string AgentCollisionMessage = "";

    private float Reward = 0;
    private float ContinuousReward = 0;
    private float Penalty = 0;

    Academy academy;

    private Area myArea;
    private Option<Collider> PreviousFrame;
    private string PreviousFrameTag;

    public enum LabelValueType {
      Time,
      Boolean
    }
    
    public LabelValueType LabelValue;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
        myArea.Logger.Log(String.Concat("Found Area: ", myArea.StartY));
        myArea.Logger.Log(String.Concat("Found Pos: ", myArea.gameObject.transform.position.y));

        agent.ColliderTags.Add(Tag);

        academy = FindObjectOfType<Academy>();

        Reward = AcademyParameters.FetchOrParse(academy, RewardKeyVal);
        ContinuousReward = AcademyParameters.FetchOrParse(academy, ContinuousRewardKeyVal);
        Penalty = AcademyParameters.FetchOrParse(academy, PenaltyKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        Reward = AcademyParameters.Update(academy, RewardKeyVal, Reward);
        ContinuousReward = AcademyParameters.Update(academy, ContinuousRewardKeyVal, ContinuousReward);
        Penalty = AcademyParameters.Update(academy, PenaltyKeyVal, Penalty);

        float labelValue;
        switch(LabelValue){
          case LabelValueType.Boolean: labelValue = 1; break;
          case LabelValueType.Time: labelValue = Time.time; break;
          default: labelValue = 0; break;
        }


        Option<GameObject> triggerColCont = 
            agent.TriggerCollider
                .Filter(tc => RequiresTag == "" || PreviousFrameTag  == RequiresTag)
                .Filter(tc => tc != null)
                .Map(tc => tc.gameObject)
                .Filter(gob => gob.tag == Tag);

        Option<Collider> prevFrame = PreviousFrame.Filter(p => p != null);

        Option<GameObject> triggerCol =
            triggerColCont
                .Filter(tc => !tc.SomeNotNull().Equals(prevFrame.Map(p => p.gameObject)));

        triggerCol
            .MatchSome(_ => {
              if(AgentCollisionMessage != "" ){
                myArea.Logger.Log(Logger.CreateMessage(LogMessageType.Agent, AgentCollisionMessage), agent);
              }

              if(RunResetMessage) {
                agent.RunResetMessage();
              }

              myArea.Logger.Log(String.Concat("Tagged ", agent.transform.position.y));
            });

        triggerColCont.MatchSome(_ => {
            agent.AddReward(ContinuousReward);
        });

        prevFrame
            .Filter(pc => triggerColCont.HasValue)
            .FlatMap(pc => pc.GetComponent<ObservableFields>().SomeNotNull())
            .MatchSome(lc => {
                if (RemoveOnLeave) {
                    myArea.Logger.Log(String.Concat("Removing label on leave ", Label));
                    lc.FieldsHash.Remove(Label);
                }
            });

        triggerCol
            .Map(tc => tc.gameObject)
            .MatchSome(go => {
                ObservableFields lc = go.GetComponent<ObservableFields>();
                if((lc == null || !lc.FieldsHash.ContainsKey(LabelPrevents)) && NewTag != "") {
                    myArea.Logger.Log(String.Concat("Adding tag ", NewTag));
                    go.tag = NewTag;
                }
            });
        
        triggerCol
            .FlatMap(tc => tc.GetComponent<ObservableFields>().SomeNotNull())
            .MatchSome(lc => {
                if(lc.FieldsHash.ContainsKey(Label) && Time.time - lc.FieldsHash[Label] < Cooldown) {
                    myArea.Logger.Log(String.Concat("already there ", Label));
                    if(Toggle) {
                        myArea.Logger.Log(String.Concat("Removing label ", Label));
                        lc.FieldsHash.Remove(Label);
                    }

                    myArea.Logger.Log(String.Concat("Penalizing already there ", Penalty));
                    agent.AddReward(Penalty);

                    if(DoneIfAlreadyThere) {
                        myArea.Logger.Log(String.Concat("Done already there ", agent.gameObject.tag));
                        agent.Done();
                    }

                    if(ResetAreaIfAlreadyThere) {
                        myArea.Logger.Log(String.Concat("Resetting already there ", agent.gameObject.tag));
                        myArea.ResetArea();
                    }
                } else if (!lc.FieldsHash.ContainsKey(LabelPrevents)) {
                    myArea.Logger.Log(String.Concat("Adding reward ", Reward));
                    agent.AddReward(Reward);
                    if(Label != "") {
                        myArea.Logger.Log(String.Concat("Adding label ", Label));
                        lc.FieldsHash.Add(Label, labelValue);
                    }
                }
            });

        triggerCol
            .Filter((GameObject go) => Remove)
            .MatchSome(tc => GameObject.Destroy(tc));

        triggerCol
            .Filter((GameObject go) => Done)
            .MatchSome(_ => agent.Done());

        triggerCol
            .Filter((GameObject go) => Reset)
            .MatchSome(_ => {
                myArea.Logger.Log(String.Concat("Reset on collide ", Tag));
                agent.Reset();
            });

        triggerCol
            .Filter((GameObject go) => ResetArea)
            .MatchSome(_ => myArea.ResetArea());

        PreviousFrame = agent.TriggerCollider;

        PreviousFrameTag = agent.gameObject.tag;
    }
}
