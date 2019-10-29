
using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Reset/Random")]
class MLResetRandom : MLReset {
    public string PlayAreaDistanceKeyVal;
    public float PositionYOffset;
    public float PositionY;
    public float CenterDistance = 0.5f;

    private float PlayAreaDistance;
    private Academy academy;

    public override void Initialize(BaseAgent agent) {
        PositionY = agent.gameObject.transform.position.y;
        academy = FindObjectOfType<Academy>();
        PlayAreaDistance = AcademyParameters.FetchOrParse(academy, PlayAreaDistanceKeyVal);
    }

    public override void Reset(BaseAgent agent) {
        PlayAreaDistance = AcademyParameters.Update(academy, PlayAreaDistanceKeyVal, PlayAreaDistance);


        /* float spawn = PlayAreaDistance * 0.5f; */
        /* float x = Random.Range(-halfdist, halfdist); */
        /* x += halfdist * Mathf.Sign(x); */
        /* float y = Random.Range(-halfdist, halfdist); */
        /* y += halfdist * Mathf.Sign(y); */

        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Vector3 polarPosition = rotation * new Vector3(0, 0, Random.Range(PlayAreaDistance * CenterDistance, PlayAreaDistance));

        agent.transform.position = new Vector3(polarPosition.x, PositionY, polarPosition.z);
        agent.transform.rotation = Quaternion.identity;
        agent.transform.Rotate(0, Random.Range(0, 360), 0);

        agent.area.EventSystem.RaiseEvent(ResetEvent.Create(agent.gameObject.GetInstanceID()));
        agent.area.EventSystem.RaiseEvent(TransformEvent.Create(
          agent.gameObject.GetInstanceID(),
          agent.gameObject.transform.localPosition,
          agent.gameObject.transform.localRotation,
          agent.gameObject.transform.localScale
        ));
    }
}
