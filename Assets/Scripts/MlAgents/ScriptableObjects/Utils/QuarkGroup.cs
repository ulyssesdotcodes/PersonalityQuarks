using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Quark Group")]

public class QuarkGroup : ScriptableObject {
    public QuarkGroup[] Groups;
    public MLReset[] Resets;
    public MLObs[] Observations;
    public MLReward[] Rewards;
    public MLAction[] Actions;

    private List<QuarkGroup> myGroups = new List<QuarkGroup>();
    private List<MLReset> myResets = new List<MLReset>();
    private List<MLObs> myObservations = new List<MLObs>();
    private List<MLReward> myRewards = new List<MLReward>();
    private List<MLAction> myActions = new List<MLAction>();

    public virtual void Initialize(BaseAgent agent) {
        foreach (QuarkGroup group in Groups) {
            QuarkGroup qg = QuarkGroup.Instantiate(group);
            qg.Initialize(agent);
            myGroups.Add(qg);
        }

        foreach (MLObs obs in Observations) {
            MLObs o = MLObs.Instantiate(obs);
            o.Initialize(agent);
            myObservations.Add(o);
        }

        foreach(MLReset reset in Resets) {
            MLReset r = MLReset.Instantiate(reset);
            r.Initialize(agent);
            myResets.Add(r);
        }

        foreach (MLReward reward in Rewards) {
            MLReward r = MLReward.Instantiate(reward);
            r.Initialize(agent);
            myRewards.Add(r);
        }

        foreach (MLAction action in Actions) {
            MLAction a = MLAction.Instantiate(action);
            a.Initialize(agent);
            myActions.Add(a);
        }
    }

    public virtual List<float> CollectObservations(BaseAgent agent) {
        List<float> obses = new List<float>();
        foreach (MLObs obs in myObservations) {
            obses.AddRange(obs.CollectObservations(agent));
        }

        foreach (QuarkGroup group in myGroups) {
            obses.AddRange(group.CollectObservations(agent));
        }

        return obses;
    }
    
    public virtual void AgentAction(BaseAgent agent, float[] vectorAction) {
        foreach (MLReward reward in myRewards) {
            reward.AddReward(agent, vectorAction);
        }

        foreach(MLAction action in myActions) {
            action.RunAction(agent, vectorAction);
        }

        foreach(QuarkGroup group in myGroups) {
            group.AgentAction(agent, vectorAction);
        }
    }

    public virtual void Reset(BaseAgent agent) {
        foreach(MLReset reset in myResets) {
            reset.Reset(agent);
        }

        foreach(QuarkGroup group in myGroups) {
            group.Reset(agent);
        }
    }
}
