using UnityEngine;

[CreateAssetMenu(menuName = "ML/Actions/Choose Parent Target")]
class MLActionChooseParentTarget : MLAction
{
    public MoveToTarget moveToTarget;
    public float PlayAreaSize = 8f;
    public override void Initialize(BaseAgent agent)
    {
        moveToTarget = agent.transform.parent.GetComponent<MoveToTarget>();
    }

    public override void RunAction(BaseAgent agent, float[] act)
    {
        Vector3 localTarget = new Vector3(
            Mathf.Clamp(act[0], -1, 1),
            Mathf.Clamp(act[1], -1, 1),
            Mathf.Clamp(act[2], -1, 1)
        ) * PlayAreaSize;

        moveToTarget.Target = agent.transform.parent.transform.position + localTarget;
        moveToTarget.Tolerence = Mathf.Clamp(act[3], 0, 1);
    }
}