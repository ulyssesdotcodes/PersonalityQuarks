using UnityEngine;

public abstract class QuarkEventListener: MonoBehaviour {
  public abstract int Id {
    get;
  }

  public abstract void OnEvent(QuarkEvent e);
}
