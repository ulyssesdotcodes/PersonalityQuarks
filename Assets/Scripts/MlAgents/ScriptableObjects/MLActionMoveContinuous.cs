using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Actions/Move Continuous")]
class MLActionMoveContinuous : MLAction
{
    public string RigidbodyName;
    public int startIdx = 0;
    public float MoveSpeed = 3f;
    public float MoveSpeedVariance = 0f;
    public float MoveMin = -0.6f;
    public float MoveMax = 1f;
    public float TagSpeedChange = 1f;
    public float MaxVelocity = 5f;
    public string Tag = "";
    private Rigidbody rigidbody;

    public bool MoveX = false;
    public bool MoveY = false;
    public bool MoveZ = true;

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
        int i = startIdx;

        var rightAmount = 0f;
        var upAmount = 0f;
        var forwardAmount = 0f;

        if (MoveX)
        {
            rightAmount = act[i++];
        }
        if (MoveY)
        {
            upAmount = act[i++];
        }
        if (MoveZ)
        {
            forwardAmount = act[i++];
        }

        float MoveSpeedRand = Random.Range(MoveSpeed - MoveSpeedVariance, MoveSpeed + MoveSpeedVariance);
        float TagMod = Tag != "" && agent.gameObject.CompareTag(Tag) ? TagSpeedChange : 1f;

        Vector3 MoveVector = new Vector3(rightAmount, upAmount, forwardAmount);

        rigidbody.AddForce(MoveVector * MoveSpeedRand * TagMod * forwardAmount, ForceMode.VelocityChange);

        if (rigidbody.velocity.sqrMagnitude > MaxVelocity * MaxVelocity) // slow it down
        {
            rigidbody.velocity *= 0.95f;
        }

        if (agent.area.EventSystem != null)
        {
            agent.area.EventSystem.RaiseEvent(TransformEvent.Create(agent.gameObject));
        }

    }
}
