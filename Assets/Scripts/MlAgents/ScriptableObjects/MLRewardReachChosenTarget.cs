using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ML/Rewards/Reach Chosen Target")]
class MLRewardReachChosenTarget : MLRewardReachTarget
{
    ChooseNewTarget chooseNewTarget;
    public override void Initialize(BaseAgent agent)
    {
        chooseNewTarget = agent.GetComponent<ChooseNewTarget>();
        BaseRigidbody = agent.transform.parent.GetComponent<Rigidbody>();
        base.Initialize(agent);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions, int deltaSteps)
    {
        if (chooseNewTarget.target)
        {
            Target = chooseNewTarget.target.position;
            base.AddReward(agent, vectorActions, deltaSteps);
        }
    }
}