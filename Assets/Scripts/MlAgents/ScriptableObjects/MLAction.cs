using UnityEngine;

public abstract class MLAction : ScriptableObject {
    public virtual void Initialize(BaseAgent agent) {

    }

    public abstract void RunAction(float[] vectorActions, GameObject go);
}
