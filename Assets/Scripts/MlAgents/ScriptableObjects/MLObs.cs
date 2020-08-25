using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using OptionalUnity;
using System.Collections.Generic;

public abstract class MLObs : ScriptableObject
{
    public virtual void Initialize(BaseAgent agent)
    {

    }

    public abstract void CollectObservations(BaseAgent agent, VectorSensor sensor);

    public virtual void Reset(BaseAgent agent)
    {
    }
}
