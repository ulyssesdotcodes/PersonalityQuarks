using UnityEngine;
using MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Tags Distance")]
class MLRewardTagsDistance : MLReward {
    public string TagA;
    public string TagB;
    public float Reward = 1;
    public float MaxDistance = 10;

    private GameObject TagAGameObject;
    private GameObject TagBGameObject;

    public override void Initialize() {
        TagAGameObject = GameObject.FindGameObjectsWithTag(TagA)[0];
        TagBGameObject = GameObject.FindGameObjectsWithTag(TagB)[0];
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        float sqrmag = (TagAGameObject.transform.position - TagBGameObject.transform.position).sqrMagnitude;
        float byMaxDist = sqrmag / (MaxDistance * MaxDistance);
        float scaledReward = Mathf.Max((1 - byMaxDist) * Reward, 0);
        agent.AddReward(scaledReward);
    }
}
