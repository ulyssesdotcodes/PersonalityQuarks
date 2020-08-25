using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "ML/Reset/Request Decision")]
class MLResetRequestDecision : MLReset
{
    public int DelayFrames;

    public override void Reset(BaseAgent agent)
    {
        agent.StartCoroutine(DecisionCoroutine(agent, DelayFrames));
    }

    private IEnumerator DecisionCoroutine(BaseAgent agent, int frames)
    {
        for (int i = 0; i < frames; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        agent.RequestDecision();
    }

}