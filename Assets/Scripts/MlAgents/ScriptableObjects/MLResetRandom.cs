
using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Reset/Random")]
class MLResetRandom : MLReset {
    public float PlayAreaDistance;
    public float PositionY;

    public override void Initialize(BaseAgent agent) {
        PositionY = agent.gameObject.transform.position.y;
    }

    public override void Reset(BaseAgent agent) {
        agent.transform.position = new Vector3(Random.Range(-PlayAreaDistance, PlayAreaDistance), PositionY, Random.Range(-PlayAreaDistance, PlayAreaDistance));
        agent.transform.rotation = Quaternion.identity;
        agent.transform.Rotate(0, Random.Range(0, 360), 0);
    }
}