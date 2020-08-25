using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class LabelCanvas : MonoBehaviour
{
    public bool Enable;
    public float offsetY = 1.5f;
    private GameObject textGameObject;
    private Dictionary<string, string> textItems;
    private Dictionary<string, float> lastAction;
    private Canvas TargetCanvas;

    public void Start()
    {
    }


    public void Speak(string text, Font font)
    {
        AddText(gameObject.name, text, font);
    }

    public void AddText(string id, string text, Font font)
    {
        Dictionary<string, string> textGameObjectItems;

        if (textGameObject == null)
        {
            TargetCanvas = GetComponent<BaseAgent>().area.WorldCanvas;
            textItems = new Dictionary<string, string>();
            lastAction = new Dictionary<string, float>();
            textGameObject = new GameObject(String.Concat(gameObject.name, "_name"));
            textGameObject.transform.SetParent(TargetCanvas.transform);
            Text textComponent = textGameObject.AddComponent<Text>();
            textComponent.font = font;
            textComponent.fontSize = 12;
            textComponent.material = font.material;
            RectTransform rect = textGameObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(256, 128);
        }

        // Store new value

        if (textItems.ContainsKey(id))
        {
            textItems.Remove(id);
            lastAction.Remove(id);
        }

        textItems.Add(id, text);
        lastAction.Add(id, Time.time);

        RefreshText();
    }

    private void RefreshText()
    {
        string finalText = "";
        // Write to text
        foreach (KeyValuePair<string, string> val in textItems)
        {
            finalText += val.Key + ": " + val.Value;
        }

        textGameObject.GetComponent<Text>().text = finalText;
    }

    public void Update()
    {
        if (textGameObject == null)
        {
            return;
        }

        List<string> RemoveLastAction = new List<string>();

        foreach (KeyValuePair<string, float> lastActionTime in lastAction)
        {
            if (Time.time - lastActionTime.Value > 4)
            {
                textItems.Remove(lastActionTime.Key);
                RemoveLastAction.Add(lastActionTime.Key);
            }
        }

        foreach (string key in RemoveLastAction)
        {
            lastAction.Remove(key);
        }

        if (RemoveLastAction.Count > 0)
        {
            RefreshText();
        }

        Vector3 offsetPos = gameObject.transform.position;
        offsetPos.y += offsetY;

        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetCanvas.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

        textGameObject.transform.localPosition = canvasPos;
    }
}
