using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="ML/Canvas Logger")]
public class CanvasLogger : Logger {
    public Font LoggingFont;
    Text WorldTextComponent;

    Queue<string> WorldText = new Queue<string>();

    public void OnEnable() {
    }

    public override void Log(Message message, Canvas WorldCanvas) {
      if(IsLoggingDebug && message.type == LogMessageType.Debug) {
          Debug.Log(message.message);
      } 

      if(IsLoggingWorld && message.type == LogMessageType.World && WorldCanvas != null) {
        WorldText.Enqueue(message.message);
        if (WorldText.Count > 20) {
          WorldText.Dequeue();
        }

        if(WorldTextComponent == null) {
          GameObject WorldTextGameObject = new GameObject("World text");
          WorldTextGameObject.transform.SetParent(WorldCanvas.transform);
          AspectRatioFitter aspect = WorldTextGameObject.AddComponent<AspectRatioFitter>();
          aspect.aspectRatio = 1.7777f;
          aspect.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
          WorldTextComponent = WorldTextGameObject.AddComponent<Text>();
          WorldTextComponent.font = LoggingFont;
          WorldTextComponent.material = LoggingFont.material;
        }

        WorldTextComponent.text = string.Join("\n", WorldText);
      } 
    }

    public override void Log(Message message, BaseAgent agent) {
      switch(message.type) {
        case LogMessageType.Debug:
          if(IsLoggingDebug) {
            Debug.Log(message.message);
          }
          break;
        case LogMessageType.Agent:
          if(IsLoggingAgent) {
            LabelCanvas canvas = agent.GetComponent<LabelCanvas>();
            canvas.Speak(message.message, LoggingFont);
          }
          break;
        case LogMessageType.World:
          Log(message, agent.area.WorldCanvas);
          break;
      }
    }
}
