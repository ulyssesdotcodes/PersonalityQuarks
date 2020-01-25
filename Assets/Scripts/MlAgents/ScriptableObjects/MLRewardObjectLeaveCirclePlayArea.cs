using UnityEngine;
using UnityEditor;
using MLAgents;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Rewards/Object Leave Circle Play Area")]
class MLRewardObjectLeaveCirclePlayArea : MLReward {
    public float Amount;
    public float Radius;
    public float PositionY;
    public float LimitY;
    public string Tag;
    public bool Reset;

    List<GameObject> TargetObjects;

    public override void Initialize(BaseAgent agent) {
        TargetObjects = agent.gameObject.GetComponentInParent<PersonalityQuarksArea>().FindGameObjectsWithTagInChildren(Tag);
    }

    public override void AddReward(BaseAgent agent, float[] vectorAction) {
        bool isOut = false;

        foreach(GameObject TargetObject in TargetObjects) {
            if(TargetObject == agent.gameObject) {
                continue;
            }

            if((new Vector2(TargetObject.transform.position.x, TargetObject.transform.position.z)).sqrMagnitude 
                > Radius * Radius) {
                agent.AddReward(Amount);
                isOut = true;
            }

            if(Mathf.Abs(TargetObject.transform.localPosition.y - PositionY) > LimitY) {
                agent.AddReward(Amount);
                isOut = true;
            }
        }

        if(isOut && Reset) {
            agent.Done();
            agent.Reset();
        }
    }
}
