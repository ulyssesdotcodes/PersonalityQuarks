using UnityEngine;

public abstract class MLReset : ScriptableObject {
    public void Initialize(BaseAgent agent) {

    }

    public abstract void Reset(BaseAgent agent);
}