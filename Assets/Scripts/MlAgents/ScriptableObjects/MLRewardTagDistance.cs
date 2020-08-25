using UnityEngine;
using Unity.MLAgents;
using System.Collections.Generic;
using OptionalUnity;

[CreateAssetMenu(menuName = "ML/Rewards/Tags Distance")]
class MLRewardTagDistance : MLReward
{
    public string TagA;
    public string TagB;
    public float Reward = 1;
    public float MaxDistance = 50;

    GameObject TagAGameObject;
    GameObject TagBGameObject;

    public override void Initialize(BaseAgent agent)
    {
        TagAGameObject = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(TagA)[0];
        TagBGameObject = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(TagB)[0];
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        float sqrmag = (TagAGameObject.transform.position - TagBGameObject.transform.position).sqrMagnitude;
        float byMaxDist = sqrmag / (MaxDistance * MaxDistance);
        float scaledReward = Mathf.Max((1 - byMaxDist) * Reward, 0) / (float)agent.MaxStep;
        agent.AddReward(scaledReward);

        if (agent.area.EventSystem != null)
        {
            agent.area.EventSystem.RaiseEvent(DistanceEvent.Create(agent.gameObject, sqrmag));
        }
    }
}
