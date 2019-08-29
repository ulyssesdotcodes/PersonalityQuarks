using System;
using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class LabelCanvas : MonoBehaviour {
    public bool Enable;
    public Canvas TargetCanvas;
    public float offsetY = 1.5f;
    private GameObject textGameObject;
    private Dictionary<string, string> textItems;

    public void Start() {
        textItems = new Dictionary<string, string>();
    }

    public void AddText(string id, string text) {
        Dictionary<string, string> textGameObjectItems;

        if(textGameObject == null) {
            textGameObject = new GameObject(String.Concat(gameObject.name, "_name"));
            textGameObject.transform.SetParent(TargetCanvas.transform);
            textGameObject.AddComponent<Text>();
            Font Arial = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textGameObject.GetComponent<Text>().font = Arial;
            textGameObject.GetComponent<Text>().material = Arial.material;
        }

        // Store new value

        if(textItems.ContainsKey(id)) {
            textItems.Remove(id);
        }

        textItems.Add(id, text);

        string finalText = "";
        // Write to text
        foreach(KeyValuePair<string, string> val in textItems) {
            finalText += val.Key + ": " + val.Value;
        }

        textGameObject.GetComponent<Text>().text = finalText;
    }

    public void Update() {
        if(textGameObject == null) {
            return;
        }

        Vector3 offsetPos = gameObject.transform.position;
        offsetPos.y += offsetY;

        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetCanvas.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

        textGameObject.transform.localPosition = canvasPos;
    }
}
