using UnityEngine;
using System;

public enum LogMessageType {
  Debug,
  Agent,
  World
}

public struct Message {
  public LogMessageType type;
  public string message;
}

[CreateAssetMenu(menuName="ML/Logger")]
public class Logger : QuarkEventListener {
    public bool IsLoggingDebug;
    public bool IsLoggingAgent;
    public bool IsLoggingWorld;

    public override void OnEnable() {
      base.Type = QuarkEventType.All;
    }

    public virtual void Log(string message) {
      Log(CreateMessage(LogMessageType.Debug, message));
    }

    public virtual void Log(Message message) {
      Debug.Log("Logging root");
        if(IsLoggingDebug) {
            Debug.Log(message.message);
        }
    }

    public virtual void Log(Message message, Canvas WorldCanvas) {
      Log(message);
    }

    public virtual void Log(Message message, BaseAgent agent) {
      Log(message);
    }

    public static Message CreateMessage(LogMessageType type, string text) {
      Message message;
      message.type = type;
      message.message = text;
      return message;
    }

    protected override void HandleEvent(QuarkEvent e) {
      switch(e.Type) {
        case QuarkEventType.Transform:
          TransformEvent ev = (TransformEvent)e;
          Debug.Log(String.Concat(e.Id, " moved to ", ev.Position));
          break;
      }
    }

}
