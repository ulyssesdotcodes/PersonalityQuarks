using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public abstract class AreaReset : ScriptableObject
{
  public abstract void Init(Area area);

  public abstract void ResetArea(Area area);
}
