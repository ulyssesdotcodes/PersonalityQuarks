using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ML/Actions/Collect")]
public class MLActionCollect : MLAction
{
    public int ActionIndex = 0;
    public string ColliderName;
    private TagPickup tagPickup;
    public override void Initialize(BaseAgent agent)
    {
        tagPickup = agent.transform.Find(ColliderName).GetComponent<TagPickup>();
    }

    public override void RunAction(BaseAgent agent, float[] vectorActions)
    {
        if (vectorActions[ActionIndex] > 0.5f)
        {
            tagPickup.Drop();
        }
    }
}