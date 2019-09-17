using UnityEngine;
using MLAgents;
using OptionalUnity;
using System;

[CreateAssetMenu(menuName="ML/Rewards/Trigger")]
class MLRewardTrigger : MLReward {
    public string Tag;
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

    private float Reward = 0;
    private float ContinuousReward = 0;
    private float Penalty = 0;

    Academy academy;

    private Area myArea;
    private Option<Collider> PreviousFrame;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
        agent.Logger.Log(String.Concat("Found Area: ", myArea.StartY));
        agent.Logger.Log(String.Concat("Found Pos: ", myArea.gameObject.transform.position.y));

        academy = FindObjectOfType<Academy>();

        Reward = AcademyParameters.FetchOrParse(academy, RewardKeyVal);
        ContinuousReward = AcademyParameters.FetchOrParse(academy, ContinuousRewardKeyVal);
        Penalty = AcademyParameters.FetchOrParse(academy, PenaltyKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        Reward = AcademyParameters.Update(academy, RewardKeyVal, Reward);
        ContinuousReward = AcademyParameters.Update(academy, ContinuousRewardKeyVal, ContinuousReward);
        Penalty = AcademyParameters.Update(academy, PenaltyKeyVal, Penalty);

        Option<GameObject> triggerColCont = 
            agent.TriggerCollider
                .Filter(tc => tc != null)
                .Map(tc => tc.gameObject)
                .Filter(tc => tc.gameObject.tag == Tag);

        Option<Collider> prevFrame = PreviousFrame.Filter(p => p != null);

        Option<GameObject> triggerCol =
            triggerColCont
                .Filter(tc => !tc.SomeNotNull().Equals(prevFrame.Map(p => p.gameObject)));

        triggerCol
            .MatchSome(_ => agent.Logger.Log(String.Concat("Tagged ", agent.transform.position.y)));

        triggerColCont.MatchSome(_ => {
            agent.AddReward(ContinuousReward);
        });

        prevFrame
            .Filter(pc => triggerColCont.HasValue)
            .FlatMap(pc => pc.GetComponent<ObservableFields>().SomeNotNull())
            .MatchSome(lc => {
                if (RemoveOnLeave) {
                    agent.Logger.Log(String.Concat("Removing label on leave ", Label));
                    lc.LabelsHash.Remove(Label);
                }
            });
        
        triggerCol
            .FlatMap(tc => tc.GetComponent<ObservableFields>().SomeNotNull())
            .MatchSome(lc => {
                if(lc.LabelsHash.Contains(Label)) {
                    agent.Logger.Log(String.Concat("already there ", agent.gameObject.tag));
                    if(Toggle) {
                        agent.Logger.Log(String.Concat("Removing label ", Label));
                        lc.LabelsHash.Remove(Label);
                    }

                    agent.Logger.Log(String.Concat("Penalizing already there ", Penalty));
                    agent.AddReward(Penalty);

                    if(DoneIfAlreadyThere) {
                        agent.Logger.Log(String.Concat("Done already there ", agent.gameObject.tag));
                        agent.Done();
                    }

                    if(ResetAreaIfAlreadyThere) {
                        agent.Logger.Log(String.Concat("Resetting already there ", agent.gameObject.tag));
                        myArea.ResetArea();
                    }
                } else if (!lc.LabelsHash.Contains(LabelPrevents)) {
                    agent.Logger.Log(String.Concat("Adding label ", Label));
                    agent.Logger.Log(String.Concat("Adding reward ", Reward));
                    agent.AddReward(Reward);
                    if(Label != "") {
                        lc.LabelsHash.Add(Label);
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
                agent.Logger.Log(String.Concat("Reset on collide ", Tag));
                agent.Reset();
            });

        triggerCol
            .Filter((GameObject go) => ResetArea)
            .MatchSome(_ => myArea.ResetArea());

        PreviousFrame = agent.TriggerCollider;
    }
}
