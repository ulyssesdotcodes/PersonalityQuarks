using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Quark Group")]

public class QuarkGroup : ScriptableObject
{
    public QuarkGroup[] Groups;
    public MLReset[] Resets;
    public MLObs[] Observations;
    public MLReward[] Rewards;
    public MLAction[] Actions;

    protected List<QuarkGroup> myGroups = new List<QuarkGroup>();
    protected List<MLReset> myResets = new List<MLReset>();
    protected List<MLObs> myObservations = new List<MLObs>();
    protected List<MLReward> myRewards = new List<MLReward>();
    protected List<MLAction> myActions = new List<MLAction>();

    public virtual void Initialize(BaseAgent agent)
    {
        foreach (QuarkGroup group in Groups)
        {
            QuarkGroup qg = QuarkGroup.Instantiate(group);
            qg.Initialize(agent);
            myGroups.Add(qg);
        }

        foreach (MLObs obs in Observations)
        {
            MLObs o = MLObs.Instantiate(obs);
            o.Initialize(agent);
            myObservations.Add(o);
        }

        foreach (MLReset reset in Resets)
        {
            MLReset r = MLReset.Instantiate(reset);
            r.Initialize(agent);
            myResets.Add(r);
        }

        foreach (MLReward reward in Rewards)
        {

            MLReward r = MLReward.Instantiate(reward);
            r.Initialize(agent);
            myRewards.Add(r);
        }

        foreach (MLAction action in Actions)
        {
            MLAction a = MLAction.Instantiate(action);
            a.Initialize(agent);
            myActions.Add(a);
        }
    }

    public virtual void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        List<float> obses = new List<float>();
        foreach (MLObs obs in myObservations)
        {
            obs.CollectObservations(agent, sensor);
        }

        foreach (QuarkGroup group in myGroups)
        {
            group.CollectObservations(agent, sensor);
        }
    }

    public virtual void AgentAction(BaseAgent agent, float[] vectorAction)
    {
        foreach (MLAction action in myActions)
        {
            action.RunAction(agent, vectorAction);
        }

        foreach (QuarkGroup group in myGroups)
        {
            group.AgentAction(agent, vectorAction);
        }
    }

    public virtual void RewardAgent(BaseAgent agent, int deltaSteps)
    {
        foreach (MLReward reward in myRewards)
        {
            reward.AddReward(agent, new float[0], deltaSteps);
        }

        foreach (QuarkGroup group in myGroups)
        {
            group.RewardAgent(agent, deltaSteps);
        }
    }

    public virtual void Reset(BaseAgent agent)
    {
        foreach (MLReset reset in myResets)
        {
            reset.Reset(agent);
        }

        foreach (MLObs obs in myObservations)
        {
            obs.Reset(agent);
        }

        foreach (QuarkGroup group in myGroups)
        {
            group.Reset(agent);
        }
    }

    public virtual void FixedUpdate() { }
}
