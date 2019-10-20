using UnityEngine;
using MLAgents;
using OptionalUnity;
using System.Collections.Generic;

public abstract class MLObs : ScriptableObject {
    public virtual void Initialize(BaseAgent agent) {

    }

    public virtual List<float> CollectObservations(BaseAgent agent){
        List<float> obs = new List<float>();
        IntObs(agent).MatchSome(io => obs.Add(io));
        FloatObs(agent).MatchSome(io => obs.Add(io));
        Vec2Obs(agent).MatchSome(io => {
            obs.Add(io.x);
            obs.Add(io.y);
        });
        Vec3Obs(agent).MatchSome(io => {
            obs.Add(io.x);
            obs.Add(io.y);
            obs.Add(io.z);
        });
        FloatArrObs(agent).MatchSome(io => obs.AddRange(io));
        FloatListObs(agent).MatchSome(io => obs.AddRange(io));
        QuatObs(agent).MatchSome(io => {
            obs.Add(io.x);
            obs.Add(io.y);
            obs.Add(io.z);
            obs.Add(io.w);
        });
        return obs;
    }

    public virtual Option<int> IntObs(BaseAgent agent){
        return Option.None<int>();
    }

    public virtual Option<float> FloatObs(BaseAgent agent){
        return Option.None<float>();
    }

    public virtual Option<Vector2> Vec2Obs(BaseAgent agent){
        return Option.None<Vector2>();
    }

    public virtual Option<Vector3> Vec3Obs(BaseAgent agent){
        return Option.None<Vector3>();
    }

    public virtual Option<float[]> FloatArrObs(BaseAgent agent){
        return Option.None<float[]>();
    }

    public virtual Option<List<float>> FloatListObs(BaseAgent agent){
        return Option.None<List<float>>();
    }

    public virtual Option<Quaternion> QuatObs(BaseAgent agent){
        return Option.None<Quaternion>();
    }
}
