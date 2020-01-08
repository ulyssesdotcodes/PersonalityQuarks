using UnityEngine;
using MLAgents;
using System.Collections.Generic;

[CreateAssetMenu(menuName="ML/Rewards/Nearby")]
class MLRewardNearby : MLReward {
    public string Tag;
    public float Reward;
    public float Distance;
    public string ResetAreaAfterKeyVal = "0";
    private float ResetTime = -1;
    private Area myArea;
    private Academy academy;
    private float ResetAreaAfter;

    private bool resetNextFrame = false;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
        academy = FindObjectOfType<Academy>();
        ResetAreaAfter = AcademyParameters.FetchOrParse(academy, ResetAreaAfterKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
      if(resetNextFrame) {
        agent.Done();
        myArea.ResetArea();
        resetNextFrame = false;
      } else {
        List<GameObject> objs = myArea.FindGameObjectsWithTagInChildren(Tag);
        foreach(GameObject obj in objs) {
            if ((obj.transform.position - agent.gameObject.transform.position).sqrMagnitude < Distance * Distance) {
              if (ResetTime < 0) {
                ResetTime = Time.time;
              } 

              ResetAreaAfter = AcademyParameters.Update(academy, ResetAreaAfterKeyVal, ResetAreaAfter);
              if (Time.time - ResetTime >= ResetAreaAfter) {
                agent.AddReward(Reward);
                resetNextFrame = true;
              }

            } else {
              ResetTime = -1;
            }
        }
      }
    }
}
