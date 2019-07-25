using UnityEngine;
using MLAgents;

public abstract class MLReward : ScriptableObject {
    public virtual void Initialize() {

    }

    public abstract void AddReward(BaseAgent agent, float[] vectorActions);
}