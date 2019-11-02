using UnityEngine;
using System;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Leave Playarea")]
class MLRewardLeavePlayarea : MLReward {
    public float Amount;
    public float LimitX;
    public float LimitZ;
    public float LimitY;
    public float PositionY;
    public bool Reset;
    public bool Done;
    public bool Wrap;
    public string AgentLeaveMessage = "Wheeeee!";
        
    public Area myArea;

    public override void Initialize(BaseAgent agent) {
        myArea = agent.gameObject.GetComponentInParent<Area>();
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {

        bool isOut = false;
        if(Mathf.Abs(agent.gameObject.transform.position.x) > LimitX) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(Mathf.Abs(agent.gameObject.transform.localPosition.y - PositionY) > LimitY) {
            agent.AddReward(Amount);
            isOut = true;
        }

        if(Mathf.Abs(agent.gameObject.transform.position.z) > LimitZ) {
//TAG: MakeEvent             myArea.Logger.Log(String.Concat("LeavePlayArea Reward", Amount));
           //TAG: MakeEvent  myArea.Logger.Log(Logger.CreateMessage(LogMessageType.Agent, AgentLeaveMessage), agent);
            agent.AddReward(Amount);
            isOut = true;
        }

        if(isOut && Wrap) {
          agent.gameObject.transform.position= new Vector3(
            FuckCSharpModulo(agent.gameObject.transform.position.x + LimitX, LimitX * 2) - LimitX,
            FuckCSharpModulo(agent.gameObject.transform.position.y + LimitY, LimitY * 2) - LimitY,
            FuckCSharpModulo(agent.gameObject.transform.position.z + LimitZ, LimitZ * 2) - LimitZ
          );
        }

        if(isOut && Done) {
            agent.Done();
        }

        if(isOut && Reset) {
            agent.Reset();
        }
    }

    private float FuckCSharpModulo(float a, float b) {
      return a - b * Mathf.Floor(a / b);
    }
}
