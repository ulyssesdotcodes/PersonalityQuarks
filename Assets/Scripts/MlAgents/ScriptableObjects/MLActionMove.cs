using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Actions/Move")]
class MLActionMove : MLAction
{
    public int startIdx = 0;
    public float Speed = 3f;
    public float SpeedVariance = 0f;
    public float TagSpeedChange = 1f;
    public float MaxSpeed = 5f;
    public string Tag = "";
    public Rigidbody rigidbody;

    public bool MoveX = true;
    public bool MoveY = false;
    public bool MoveZ = true;

    public override void RunAction(BaseAgent agent, float[] act)
    {
        int i = startIdx;

        var rightAmount = 0f;
        var upAmount = 0f;
        var forwardAmount = 0f;

        if (MoveX)
        {
            rightAmount = GetMultiplier(act[i++]);
        }
        if (MoveY)
        {
            upAmount = GetMultiplier(act[i++]);
        }
        if (MoveZ)
        {
            forwardAmount = GetMultiplier(act[i++]);
        }

        float SpeedRand = Random.Range(Speed - SpeedVariance, Speed + SpeedVariance);
        float TagMod = Tag != "" && agent.gameObject.CompareTag(Tag) ? TagSpeedChange : 1f;

        Vector3 MoveVector = new Vector3(rightAmount, upAmount, forwardAmount);

        rigidbody.AddForce(MoveVector * SpeedRand * TagMod, ForceMode.VelocityChange);

        if (rigidbody.velocity.sqrMagnitude > MaxSpeed * MaxSpeed) // slow it down
        {
            rigidbody.velocity *= 0.95f;
        }

        if (agent.area.EventSystem != null)
        {
            agent.area.EventSystem.RaiseEvent(TransformEvent.Create(agent.gameObject));
        }
    }

    private float GetMultiplier(float action)
    {
        switch (action)
        {
            case 1: return -1;
            case 2: return 1;
            default: return 0;
        }
    }
}
