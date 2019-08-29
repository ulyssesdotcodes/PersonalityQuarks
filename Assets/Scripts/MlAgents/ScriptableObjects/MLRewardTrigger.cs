using UnityEngine;
using MLAgents;
using OptionalUnity;
using System;

[CreateAssetMenu(menuName="ML/Rewards/Trigger")]
class MLRewardTrigger : MLReward {
    public string Tag;
    public string Label;
    public float Reward = 0;
    public float ContinuousReward = 0;
    public float Penalty = 0;
    public bool Remove = true;
    public bool Reset = true;
    public bool Done = false;
    public bool Toggle = false;
    public bool PenaltyIfAlreadyThere = false;
    public bool ResetArea = true;

    private Area myArea;
    private Option<Collider> PreviousFrame;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
        agent.Logger.Log(String.Concat("Found Area: ", myArea.StartY));
        agent.Logger.Log(String.Concat("Found Pos: ", myArea.gameObject.transform.position.y));
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        Option<GameObject> triggerCol = 
            agent.TriggerCollider
                .Map(tc => tc.gameObject)
                .Filter(tc => tc.gameObject.tag == Tag)
                .Filter(tc => !tc.SomeNotNull().Equals(PreviousFrame.HasValue));

        triggerCol
            .MatchSome(_ => agent.Logger.Log(String.Concat("Tagged ", agent.transform.position.y)));

        triggerCol.MatchSome(_ => {
            agent.AddReward(ContinuousReward);
        });

        triggerCol
            .Filter((GameObject go) => Remove)
            .MatchSome(tc => GameObject.Destroy(tc));

        triggerCol
            .Filter((GameObject go) => Reset)
            .MatchSome(_ => agent.Reset());

        triggerCol
            .Filter((GameObject go) => Done)
            .MatchSome(_ => agent.Done());
        
        triggerCol
            .Filter(_ => Label != "")
            .FlatMap(tc => tc.GetComponent<ObservableFields>().SomeNotNull())
            .MatchSome(lc => {
                if(lc.LabelsHash.Contains(Label)) {
                    if(Toggle) {
                        agent.Logger.Log(String.Concat("Removing label ", Label));
                        lc.LabelsHash.Remove(Label);
                    }

                    if(PenaltyIfAlreadyThere) {
                        agent.Logger.Log(String.Concat("Penalizing already there ", agent.gameObject.tag));
                        agent.AddReward(Penalty);
                    }
                } else {
                    agent.Logger.Log(String.Concat("Adding label ", Label));
                    agent.AddReward(Reward);
                    lc.LabelsHash.Add(Label);
                }
            });

        triggerCol
            .Filter((GameObject go) => ResetArea)
            .MatchSome(_ => myArea.ResetArea());

        PreviousFrame = agent.TriggerCollider;
    }
}
