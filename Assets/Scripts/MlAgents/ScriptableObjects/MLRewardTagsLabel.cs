using System;
using UnityEngine;
using MLAgents;
using System.Collections;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Tags Label")]
class MLRewardTagsLabel : MLReward {
    public List<string> Tags;
    public string Label;
    public bool Remove = true;
    public bool Reset = true;
    public bool AcademyReset;
    public bool AcademyDone;
    public float Reward = 0;

    private Academy myAcademy;

    private List<ObservableFields> LabelObjects;
    private HashSet<int> AddedLastRound = new HashSet<int>();

    public override void Initialize() {
        myAcademy = GameObject.FindGameObjectsWithTag("academy")[0].GetComponent<Academy>();

        LabelObjects = new List<ObservableFields>();

        foreach(string tag in Tags) {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag)){
                ObservableFields lobj = obj.GetComponent<ObservableFields>();
                if (lobj != null) {
                    LabelObjects.Add(lobj);
                }
            }
        }
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {

        if (AddedLastRound.Contains(agent.gameObject.GetInstanceID())) {
            AddedLastRound.Remove(agent.gameObject.GetInstanceID());

            if(AcademyReset) {
                myAcademy.AcademyReset();
            }

            if(AcademyDone) {
                myAcademy.Done();
            }

            return;
        }


        bool allHaveTag = true;
        foreach(ObservableFields labels in LabelObjects) {
            if (!labels.LabelsHash.Contains(Label)) {
                allHaveTag = false;
            }
        }

        if (allHaveTag) {
            AddedLastRound.Add(agent.gameObject.GetInstanceID());
            agent.AddReward(Reward);

            if (Remove) {
                foreach(ObservableFields labels in LabelObjects) {
                    labels.LabelsHash.Remove(Label);
                }
            }

            if (Reset) {
                agent.Done();
                agent.Reset();
            }

        }
    }
}
