using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Leave Circle Playarea")]
class MLRewardLeaveCirclePlayarea : MLReward
{
    public string ChildTransform = "";
    public float Amount;
    public float LimitRadius;
    public float LimitY;
    public bool Clamp = false;
    public bool Reset;
    private float PositionY;
    private Transform targetTransform;
    private Rigidbody targetRb;

    public override void Initialize(BaseAgent agent)
    {
        if (ChildTransform == "")
        {
            targetTransform = agent.transform;
        }
        else
        {
            targetTransform = agent.transform.Find(ChildTransform);
        }

        PositionY = targetTransform.localPosition.y;
        targetRb = targetTransform.GetComponent<Rigidbody>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        bool isOut = false;

        Vector2 XZ = new Vector2(
            targetTransform.localPosition.x,
            targetTransform.localPosition.z
        );

        if (XZ.sqrMagnitude > LimitRadius * LimitRadius)
        {
            agent.AddReward(Amount);
            isOut = true;

            if (Clamp)
            {
                Vector3 pos = targetTransform.localPosition;
                XZ = XZ * LimitRadius / XZ.magnitude;
                pos.x = XZ.x;
                pos.z = XZ.y;
                targetTransform.localPosition = pos;

                if (targetRb != null)
                {
                    Vector3 vel = targetRb.velocity;
                    vel.x = 0;
                    vel.z = 0;
                    targetRb.velocity = vel;
                }
            }
        }

        if (Mathf.Abs(targetTransform.localPosition.y - PositionY) > LimitY)
        {
            agent.AddReward(Amount);
            isOut = true;

            if (Clamp)
            {
                Vector3 pos = targetTransform.localPosition;
                pos.y = Mathf.Sign(pos.y - PositionY) * LimitY + PositionY;
                targetTransform.localPosition = pos;

                if (targetRb != null)
                {
                    Vector3 vel = targetRb.velocity;
                    vel.y = 0;
                    targetRb.velocity = vel;
                }
            }
        }


        if (isOut && Reset)
        {
            agent.EndEpisode();
        }
    }
}