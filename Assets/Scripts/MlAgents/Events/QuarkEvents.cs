using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class QuarkEvents : MonoBehaviour {

  private Queue<QuarkEvent> Events = new Queue<QuarkEvent>();

  private Dictionary<QuarkEventType, HashSet<QuarkEventListener>> Listeners = 
    new Dictionary<QuarkEventType, HashSet<QuarkEventListener>>();
 
  public void Start()
  {

  }

  public void Update() {
    foreach(QuarkEvent e in Events) {
      if(Listeners.ContainsKey(e.Type)) {
        foreach(QuarkEventListener l in Listeners[e.Type]) {
          l.OnEvent(e);
        }
      }

      foreach(QuarkEventListener l in Listeners[QuarkEventType.All]) {
        l.OnEvent(e);
      }
    }
  }

  public void RaiseEvent(QuarkEvent e) {
    Events.Enqueue(e);
  }

  public void AddTypeListener(QuarkEventListener listener) {
     if(Listeners.ContainsKey(listener.Type)) {
       Listeners[listener.Type].Add(listener);
     } else {
       Listeners.Add(listener.Type, new HashSet<QuarkEventListener>(new [] { listener }));
     }
  }

  public void RemoveTypeListener(QuarkEventListener listener) {
     if(Listeners.ContainsKey(listener.Type)) {
       HashSet<QuarkEventListener> TypedListeners = Listeners[listener.Type];
       if(TypedListeners.Contains(listener)) {
         TypedListeners.Remove(listener);
       }
     }
  }
}
