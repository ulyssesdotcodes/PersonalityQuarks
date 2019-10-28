using UnityEngine;

public abstract class QuarkEventListener : ScriptableObject {
  public QuarkEvents EventSystem;
  public QuarkEventType Type;

  public virtual void Start(QuarkEvents e) {
    EventSystem = e;
    EventSystem.AddTypeListener(this);
  }

  public virtual void OnDisable() {
    EventSystem.RemoveTypeListener(this);
  }

  public void OnEvent(QuarkEvent e) {
    if(e.Type != Type) {
      throw new UnityException("something went really wrong");
    }

    HandleEvent(e);
  }

  protected abstract void HandleEvent(QuarkEvent e);
}
