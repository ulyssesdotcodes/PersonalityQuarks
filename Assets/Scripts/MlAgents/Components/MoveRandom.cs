
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{
    public List<string> StopTags = new List<string>();
    public EnvironmentParameter Speed = 1f;
    public bool MoveY = false;
    private bool running = true;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (string tag in StopTags)
        {
            if (gameObject.CompareTag(tag))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        if (running)
        {
            rb.AddForce(new Vector3(
                (Random.value - 0.5f) * 2f,
                MoveY ? (Random.value - 0.5f) * 2f : 0,
                (Random.value - 0.5f) * 2f
            ) * Speed, ForceMode.Impulse);
        }
    }
}
