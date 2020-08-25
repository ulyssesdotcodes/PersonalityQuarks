using UnityEngine;
using System;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Rewards/Leave Playarea")]
class MLRewardLeavePlayarea : MLReward
{
    public string ChildTransform = "";
    public float Amount;
    public float AmountY;
    public float LimitX;
    public float LimitZ;
    public float LimitY;
    public bool Done;
    public bool Wrap;
    public string AgentLeaveMessage = "Wheeeee!";
    private Transform targetTransform;
    private float PositionY;

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
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {

        bool isOut = false;
        if (Mathf.Abs(targetTransform.localPosition.x) > LimitX)
        {
            agent.AddReward(Amount);
            isOut = true;
        }

        if (Mathf.Abs(targetTransform.localPosition.y - PositionY) > LimitY)
        {
            agent.AddReward(AmountY);
            isOut = true;
        }

        if (Mathf.Abs(targetTransform.localPosition.z) > LimitZ)
        {
            //TAG: MakeEvent             myArea.Logger.Log(String.Concat("LeavePlayArea Reward", Amount));
            //TAG: MakeEvent  myArea.Logger.Log(Logger.CreateMessage(LogMessageType.Agent, AgentLeaveMessage), agent);
            agent.AddReward(Amount);
            isOut = true;
        }

        if (isOut && Wrap)
        {
            targetTransform.position = new Vector3(
              FuckCSharpModulo(targetTransform.localPosition.x + LimitX, LimitX * 2) - LimitX,
              FuckCSharpModulo(targetTransform.localPosition.y + LimitY, LimitY * 2) - LimitY,
              FuckCSharpModulo(targetTransform.localPosition.z + LimitZ, LimitZ * 2) - LimitZ
            );
        }

        if (isOut && Done)
        {
            agent.EndEpisode();
        }
    }

    private float FuckCSharpModulo(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }
}
