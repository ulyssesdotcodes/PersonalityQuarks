using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveToTarget))]
public class StartEpisodeRequestDecision : StartEpisodeAction
{
    public BaseAgent TargetAgent;

    public override void OnStartEpisodePostReset()
    {
        TargetAgent.RequestDecision();
    }

}