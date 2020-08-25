using UnityEngine;

public abstract class MLAction : ScriptableObject
{
    public virtual void Initialize(BaseAgent agent)
    {

    }

    public abstract void RunAction(BaseAgent agent, float[] vectorActions);

    public virtual void Reset(BaseAgent agent)
    {

    }

}
