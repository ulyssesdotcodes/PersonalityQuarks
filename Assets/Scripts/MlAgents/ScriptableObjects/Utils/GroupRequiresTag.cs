using System.Collections.Generic;

public class GroupRequiresTag : QuarkGroup {
  public string Tag = "";

  public override List<float> CollectObservations(BaseAgent agent) {
    if (agent.gameObject.tag == Tag) {
      return base.CollectObservations(agent);
    }

    return new List<float>();
  }

  public virtual void AgentAction(BaseAgent agent, float[] vectorAction) {
    if (agent.gameObject.tag == Tag) {
      base.AgentAction(agent, vectorAction);
    }
  }

  public virtual void Reset(BaseAgent agent) {
    if (agent.gameObject.tag == Tag) {
      base.Reset(agent);
    }
  }
}
