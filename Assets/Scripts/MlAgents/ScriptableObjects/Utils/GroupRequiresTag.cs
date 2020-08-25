using System.Collections.Generic;
using Unity.MLAgents.Sensors;

public class GroupRequiresTag : QuarkGroup
{
    public string Tag = "";

    public override void CollectObservations(BaseAgent agent, VectorSensor sensor)
    {
        if (agent.gameObject.tag == Tag)
        {
            base.CollectObservations(agent, sensor);
        }
    }

    public override void AgentAction(BaseAgent agent, float[] vectorAction)
    {
        if (agent.gameObject.tag == Tag)
        {
            base.AgentAction(agent, vectorAction);
        }
    }

    public override void Reset(BaseAgent agent)
    {
        if (agent.gameObject.tag == Tag)
        {
            base.Reset(agent);
        }
    }
}
