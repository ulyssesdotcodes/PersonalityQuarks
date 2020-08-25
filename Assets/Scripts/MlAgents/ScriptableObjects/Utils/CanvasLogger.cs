using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLogger : QuarkEventListener
{
    Queue<string> WorldText = new Queue<string>();
    public Font LoggingFont;
    Text WorldTextComponent;

    // public override int Id {
    //   get { return -1; }
    // }

    private void Log(Message message, Canvas WorldCanvas)
    {
        if (message.type == LogMessageType.Debug)
        {
            Debug.Log(message.message);
        }

        if (message.type == LogMessageType.World && WorldCanvas != null)
        {
            WorldText.Enqueue(message.message);
            if (WorldText.Count > 20)
            {
                WorldText.Dequeue();
            }

            if (WorldTextComponent == null)
            {
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

    public override void OnEvent(QuarkEvent e)
    {
        // TODO: implement
    }
}
