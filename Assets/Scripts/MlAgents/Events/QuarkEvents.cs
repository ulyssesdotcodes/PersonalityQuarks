using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;


public class QuarkEvents : MonoBehaviour
{

    public List<QuarkEventListener> Listeners;

    private Queue<QuarkEvent> Events = new Queue<QuarkEvent>();

    public void Start()
    {
        foreach (QuarkEventListener listener in Listeners)
        {
            // AddListener(listener);
            listener.EventSystem = this;
        }
    }

    public void AddListener(QuarkEventListener listener)
    {
        Listeners.Add(listener);
    }

    public void Update()
    {
        while (Events.Count > 0)
        {
            QuarkEvent e = Events.Dequeue();
            foreach (QuarkEventListener l in Listeners)
            {
                l.OnEvent(e);
            }
        }
    }

    public void RaiseEvent(QuarkEvent e)
    {
        Events.Enqueue(e);
    }
}
