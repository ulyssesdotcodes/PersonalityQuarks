using UnityEngine;
public abstract class MLReset : ScriptableObject
{
    public virtual void Initialize(BaseAgent agent)
    {

    }

    public abstract void Reset(BaseAgent agent);

}