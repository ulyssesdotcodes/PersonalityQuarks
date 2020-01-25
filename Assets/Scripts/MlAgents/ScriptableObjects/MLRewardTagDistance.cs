using UnityEngine;
using MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Tags Distance")]
class MLRewardTagDistance : MLReward {
    public string TagA;
    public string TagB;
    public float Reward = 1;
    public float MaxDistance = 10;

    GameObject TagAGameObject;
    GameObject TagBGameObject;

    public override void Initialize(BaseAgent agent) {
        TagAGameObject = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(TagA)[0];
        TagBGameObject = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(TagB)[0];
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        float sqrmag = (TagAGameObject.transform.position - TagBGameObject.transform.position).sqrMagnitude;
        float byMaxDist = sqrmag / (MaxDistance * MaxDistance);
        float scaledReward = Mathf.Max((1 - byMaxDist) * Reward, 0) / (float)agent.agentParameters.maxStep;
        agent.AddReward(scaledReward);
    }
}
