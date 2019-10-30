using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class QuarkEvents : MonoBehaviour {

  public List<QuarkEventListener> Listeners;

  private Queue<QuarkEvent> Events = new Queue<QuarkEvent>();

  private Dictionary<int, HashSet<QuarkEventListener>> ListenersById = 
    new Dictionary<int, HashSet<QuarkEventListener>>();

  public void Awake() {
    foreach(QuarkEventListener listener in Listeners) {
      AddListener(listener);
      listener.EventSystem = this;
    }
  }
  
  public void AddListener(QuarkEventListener listener) {
     if(ListenersById.ContainsKey(listener.Id)) {
       ListenersById[listener.Id].Add(listener);
     } else {
       ListenersById.Add(listener.Id, new HashSet<QuarkEventListener>(new [] { listener }));
     }
  }

  public void Update() {
    while(Events.Count > 0) {
      QuarkEvent e = Events.Dequeue();
      if(ListenersById.ContainsKey(e.Id)) {
        foreach(QuarkEventListener l in ListenersById[e.Id]) {
          l.OnEvent(e);
        }
      }

      if(ListenersById.ContainsKey(-1)) {
        foreach(QuarkEventListener l in ListenersById[-1]) {
          l.OnEvent(e);
        }
      }
    }
  }

  public void RaiseEvent(QuarkEvent e) {
    Events.Enqueue(e);
  }
}
