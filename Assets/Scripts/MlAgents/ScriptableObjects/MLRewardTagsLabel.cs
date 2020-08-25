using System;
using UnityEngine;
using Unity.MLAgents;
using System.Collections;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Tags Labels")]
class MLRewardTagsLabel : MLReward
{
    public List<string> Tags;
    public List<string> Labels;
    public bool Remove = true;
    public bool Reset = true;
    public bool MultRewardByTagCount = false;
    public bool AreaReset;
    public string RewardKeyVal = "0";

    private float Reward = 0;

    PersonalityQuarksArea myArea;
    private HashSet<int> AddedLastRound = new HashSet<int>();


    public override void Initialize(BaseAgent agent)
    {
        Reward = AcademyParameters.FetchOrParse(Academy.Instance, RewardKeyVal);

        myArea = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        List<ObservableFields> LabelObjects = new List<ObservableFields>();
        List<GameObject> objs = new List<GameObject>();

        foreach (string tag in Tags)
        {
            objs.AddRange(myArea.FindGameObjectsWithTagInChildren(tag));
        }

        foreach (GameObject obj in objs)
        {
            ObservableFields lobj = obj.GetComponent<ObservableFields>();
            if (lobj != null)
            {
                LabelObjects.Add(lobj);
            }
        }

        Reward = AcademyParameters.Update(Academy.Instance, RewardKeyVal, Reward);

        if (AddedLastRound.Contains(agent.gameObject.GetInstanceID()))
        {
            AddedLastRound.Remove(agent.gameObject.GetInstanceID());

            if (AreaReset)
            {
                myArea.ResetArea();
            }

            return;
        }

        bool allHaveTag = true;
        foreach (ObservableFields labels in LabelObjects)
        {
            foreach (string label in Labels)
            {
                allHaveTag &= labels.LabelsHash.Contains(label);
            }
        }

        if (allHaveTag)
        {
            // TAG: MakeEvent myArea.Logger.Log(String.Concat("All ", String.Join(",", Tags), " have labels ", String.Join(",", Labels), " Adding reward: ", Reward));
            AddedLastRound.Add(agent.gameObject.GetInstanceID());
            agent.AddReward(Reward * (MultRewardByTagCount ? objs.Count : 1));

            if (Remove)
            {
                foreach (ObservableFields labels in LabelObjects)
                {
                    foreach (string label in Labels)
                    {
                        labels.LabelsHash.Remove(label);
                    }
                }
            }

            if (Reset)
            {
                agent.EndEpisode();
            }

        }
    }
}
