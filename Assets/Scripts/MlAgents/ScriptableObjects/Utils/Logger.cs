using UnityEngine;


[CreateAssetMenu(menuName="ML/Logger")]
public class Logger : ScriptableObject {
    public bool IsLogging;

    public void OnEnable() {
        IsLogging = Debug.isDebugBuild && IsLogging;
    }

    public virtual void Log(string l) {
        if(IsLogging) {
            Debug.Log(l);
        }
    }
}
