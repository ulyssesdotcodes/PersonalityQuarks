using UnityEngine;
using System;
using System.Collections.Generic;

public enum LogMessageType
{
    Debug,
    Agent,
    World
}

public struct Message
{
    public LogMessageType type;
    public string message;
}

public class Logger : QuarkEventListener
{
    public List<QuarkEventType> LogTypes = new List<QuarkEventType>();

    // public override int Id
    // {
    //     get { return -1; }
    // }

    public override void OnEvent(QuarkEvent e)
    {
        switch (e.Type)
        {
            case QuarkEventType.Transform:
                TransformEvent ev = (TransformEvent)e;
                break;
        }
    }

}
