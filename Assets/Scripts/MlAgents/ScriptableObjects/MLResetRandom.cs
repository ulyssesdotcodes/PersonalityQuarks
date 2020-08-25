
using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Reset/Random")]
class MLResetRandom : MLReset
{
    public string PlayAreaDistanceKeyVal;
    public Vector3 Offset;
    private Vector3 StartPosition;
    public float CenterDistance = 0.5f;

    public bool ResetPositionXZ = true;
    public bool ResetPositionY = false;
    public bool ResetRotation = true;

    private float PlayAreaDistance;
    private Academy academy;
    [HideInInspector]
    public Transform TargetTransform;

    public override void Initialize(BaseAgent agent)
    {
        Vector3 StartPosition = TargetTransform.position;
        academy = Academy.Instance;
        PlayAreaDistance = AcademyParameters.FetchOrParse(academy, PlayAreaDistanceKeyVal);
    }

    public override void Reset(BaseAgent agent)
    {
        PlayAreaDistance = AcademyParameters.Update(academy, PlayAreaDistanceKeyVal, PlayAreaDistance);

        TargetTransform.localPosition = new Vector3(
            ResetPositionXZ ? Random.value * PlayAreaDistance : StartPosition.x,
            ResetPositionY ? Random.value * PlayAreaDistance : StartPosition.y,
            ResetPositionXZ ? Random.value * PlayAreaDistance : StartPosition.z
        ) + Offset;

        if (ResetRotation)
        {
            TargetTransform.rotation = Quaternion.identity;
            TargetTransform.Rotate(0, Random.Range(0, 360), 0);
        }

        if (agent.area.EventSystem != null)
        {
            agent.area.EventSystem.RaiseEvent(ResetEvent.Create(agent.gameObject));
            agent.area.EventSystem.RaiseEvent(TransformEvent.Create(agent.gameObject));
        }
    }
}
