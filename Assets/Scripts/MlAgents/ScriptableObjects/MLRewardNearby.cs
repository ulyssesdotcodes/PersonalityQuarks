using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Nearby")]
class MLRewardNearby : MLReward {
    public string Tag;
    public float Reward;
    public float Distance;
    public float ResetAreaAfter = -1;
    private float ResetTime = -1;
    private Area myArea;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tag);
        foreach(GameObject obj in objs) {
            if ((obj.transform.position - agent.gameObject.transform.position).sqrMagnitude < Distance * Distance) {
                agent.AddReward(Reward / (float)agent.agentParameters.maxStep);

                if (ResetTime < 0) {
                  ResetTime = Time.time;
                } else if (Time.time - ResetTime > ResetAreaAfter) {
                  myArea.ResetArea();
                }

            } else {
              ResetTime = -1;
            }
        }
    }
}
