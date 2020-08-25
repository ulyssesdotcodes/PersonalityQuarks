using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConstant : MonoBehaviour
{
    public string StopTag = "";
    public float Speed = 1f;
    private bool running = true;

    private Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 target = new Vector3(Random.Range(-0.1f, 0.1f), transform.localPosition.y, Random.Range(-0.1f, 0.1f));
        Direction = (target - transform.localPosition.normalized) * Random.Range(0.5f, 2f) * Speed;
        GetComponent<Rigidbody>().velocity = Direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag(StopTag))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (running)
        {
            Vector3 pos = transform.position;
            pos += Direction * Time.deltaTime;
            transform.position = pos;
        }
    }
}
