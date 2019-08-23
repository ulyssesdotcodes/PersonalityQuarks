using UnityEngine;
using MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Tags Label")]
class MLRewardTagsLabel : MLReward {
    public List<string> Tags;
    public string Label;
    public bool Remove = true;
    public bool Reset = true;
    public bool AcademyReset;
    public float Reward = 0;

    private Academy myAcademy;

    private List<ObservableFields> LabelObjects;

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
        bool allHaveTag = true;
        foreach(ObservableFields labels in LabelObjects) {
            if (!labels.LabelsHash.Contains(Label)) {
                allHaveTag = false;
            }
        }

        if (allHaveTag) {
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

            if(AcademyReset) {
                myAcademy.Done();
                myAcademy.AcademyReset();
            }
        }
    }
}
