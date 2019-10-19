using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTags : MonoBehaviour
{
  public string[] tags;
  public Color[] colors;
    Renderer Renderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }

    protected virtual void Update() {
      for(int i = 0; i < tags.Length; i++) {
        if(gameObject.CompareTag(tags[i])) {
          Renderer.material.color = colors[i];
        }
      }
    }
}
