using UnityEngine;

public abstract class MLAction : ScriptableObject {
    public void Initialize() {

    }

    public abstract void RunAction(float[] vectorActions, GameObject go);
}