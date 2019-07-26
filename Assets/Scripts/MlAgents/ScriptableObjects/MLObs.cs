using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

public abstract class MLObs : ScriptableObject {
    public virtual void Initialize() {

    }

    public virtual Option<int> IntObs(Agent agent){
        return Option.None<int>();
    }

    public virtual Option<float> FloatObs(Agent agent){
        return Option.None<float>();
    }

    public virtual Option<Vector2> Vec2Obs(Agent agent){
        return Option.None<Vector2>();
    }

    public virtual Option<Vector3> Vec3Obs(Agent agent){
        return Option.None<Vector3>();
    }

    public virtual Option<float[]> FloatArrObs(Agent agent){
        return Option.None<float[]>();
    }

    public virtual Option<List<float>> FloatListObs(Agent agent){
        return Option.None<List<float>>();
    }

    public virtual Option<Quaternion> QuatObs(Agent agent){
        return Option.None<Quaternion>();
    }
}