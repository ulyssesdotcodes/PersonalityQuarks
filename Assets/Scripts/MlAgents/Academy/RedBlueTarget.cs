using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlueTarget : MonoBehaviour
{
    Renderer Renderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }

    protected virtual void Update() {
        if (gameObject.tag == "redtarget") {
            Renderer.material.color = Color.red;
        } else if (gameObject.tag == "bluetarget") {
            Renderer.material.color = Color.blue;
        }
    }
}
