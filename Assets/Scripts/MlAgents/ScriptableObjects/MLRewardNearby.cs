using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Nearby")]
class MLRewardNearby : MLReward {
    public string Tag;
    public float Reward;
    public float Distance;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tag);
        foreach(GameObject obj in objs) {
            if ((obj.transform.position - agent.gameObject.transform.position).sqrMagnitude < Distance) {
                agent.AddReward(Reward / (float)agent.agentParameters.maxStep);
            }
        }
    }
}
