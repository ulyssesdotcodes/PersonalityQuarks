using System;
using UnityEngine;
using MLAgents;
using System.Collections;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Tags Labels")]
class MLRewardTagsLabel : MLReward {
    public List<string> Tags;
    public List<string> Labels;
    public bool Remove = true;
    public bool Reset = true;
    public bool MultRewardByTagCount = false;
    public bool AreaReset;
    public string RewardKeyVal = "0";

    Academy academy;
    private float Reward = 0;

    Area myArea;
    private HashSet<int> AddedLastRound = new HashSet<int>();


    public override void Initialize(BaseAgent agent) {
        academy = FindObjectOfType<Academy>();
        Reward = AcademyParameters.FetchOrParse(academy, RewardKeyVal);

        myArea = agent.gameObject.GetComponentInParent<Area>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        List<ObservableFields> LabelObjects = new List<ObservableFields>();
        List<GameObject> objs = new List<GameObject>();

        foreach(string tag in Tags) {
            objs.AddRange(myArea.FindGameObjectsWithTagInChildren(tag));
        }

        foreach(GameObject obj in objs){
            ObservableFields lobj = obj.GetComponent<ObservableFields>();
            if (lobj != null) {
                LabelObjects.Add(lobj);
            }
        }

        Reward = AcademyParameters.Update(academy, RewardKeyVal, Reward);

        if (AddedLastRound.Contains(agent.gameObject.GetInstanceID())) {
            AddedLastRound.Remove(agent.gameObject.GetInstanceID());

            if(AreaReset) {
                myArea.ResetArea();
            }

            return;
        }

        bool allHaveTag = true;
        foreach(ObservableFields labels in LabelObjects) {
            foreach(string label in Labels) {
                allHaveTag &= labels.LabelsHash.Contains(label);
            }
        }

        if (allHaveTag) {
            myArea.Logger.Log(String.Concat("All ", String.Join(",", Tags), " have labels ", String.Join(",", Labels), " Adding reward: ", Reward));
            AddedLastRound.Add(agent.gameObject.GetInstanceID());
            agent.AddReward(Reward * (MultRewardByTagCount ? objs.Count : 1));

            if (Remove) {
                foreach(ObservableFields labels in LabelObjects) {
                    foreach(string label in Labels) {
                        labels.LabelsHash.Remove(label);
                    }
                }
            }

            if (Reset) {
                agent.Done();
                agent.Reset();
            }

        }
    }
}
