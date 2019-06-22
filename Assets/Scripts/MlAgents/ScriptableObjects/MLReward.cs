using UnityEngine;
using MLAgents;

public abstract class MLReward : ScriptableObject {
    public void Initialize() {

    }

    public abstract void AddReward(BaseAgent agent, float[] vectorActions);
}