using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float Tolerence = 0.05f;
    public Vector3 Target;
    public bool IsDebug = false;

    public void FixedUpdate()
    {
        if (IsDebug)
        {
            Debug.DrawLine(transform.position, Target);
        }
    }
}