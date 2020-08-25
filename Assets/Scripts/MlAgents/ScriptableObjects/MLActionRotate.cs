using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Actions/Rotate")]
class MLActionRotate : MLAction
{
    public string RigidbodyName;
    public int turnIdx = 0;
    public float TurnSpeed = 1f;
    public float TurnSpeedVariance = 0f;
    public float TurnMin = -90f;
    public float TurnMax = 90f;
    public int Axis = 0;
    private Rigidbody rigidbody;

    public override void Initialize(BaseAgent agent)
    {
        if (RigidbodyName == "")
        {
            rigidbody = agent.GetComponent<Rigidbody>();
        }
        else
        {
            rigidbody = agent.transform.Find(RigidbodyName).GetComponent<Rigidbody>();
        }
    }

    public override void RunAction(BaseAgent agent, float[] act)
    {
        GameObject gameObject = rigidbody.gameObject;
        Transform transform = rigidbody.transform;

        var rotateAmount = act[turnIdx];
        float TurnSpeedRand = Random.Range(TurnSpeed - TurnSpeedVariance, TurnSpeed + TurnSpeedVariance);
        Vector3 dir = Axis == 1 ? transform.forward : Axis == 2 ? transform.right : transform.up;
        rigidbody.AddTorque(dir * Time.fixedDeltaTime * TurnSpeedRand * rotateAmount, ForceMode.VelocityChange);

        Vector3 angles = transform.localEulerAngles;
        if (Axis == 0)
        {
            angles.y = Mathf.Clamp(angles.y, TurnMin, TurnMax);
        }
        else if (Axis == 1)
        {
            angles.x = Mathf.Clamp(angles.x, TurnMin, TurnMax);
        }
        else if (Axis == 2)
        {
            angles.z = Mathf.Clamp(angles.z, TurnMin, TurnMax);
        }
        transform.localEulerAngles = angles;

    }
}
