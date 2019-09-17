
using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Reset/Random")]
class MLResetRandom : MLReset {
    public string PlayAreaDistanceKeyVal;
    public float PositionYOffset;
    public float PositionY;

    private float PlayAreaDistance;
    private Academy academy;

    public override void Initialize(BaseAgent agent) {
        PositionY = agent.gameObject.transform.position.y;
        academy = FindObjectOfType<Academy>();
        PlayAreaDistance = AcademyParameters.FetchOrParse(academy, PlayAreaDistanceKeyVal);
    }

    public override void Reset(BaseAgent agent) {
        PlayAreaDistance = AcademyParameters.Update(academy, PlayAreaDistanceKeyVal, PlayAreaDistance);


        float halfdist = PlayAreaDistance * 0.5f;
        float x = Random.Range(-halfdist, halfdist);
        x += halfdist * Mathf.Sign(x);
        float y = Random.Range(-halfdist, halfdist);
        y += halfdist * Mathf.Sign(y);

        agent.transform.position = new Vector3(x, PositionY, y);
        agent.transform.rotation = Quaternion.identity;
        agent.transform.Rotate(0, Random.Range(0, 360), 0);
    }
}
