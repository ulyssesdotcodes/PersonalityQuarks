using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Actions/Move")]
class MLActionMove : MLAction {
    public int forwardIdx = 0;
    public int rightIdx = 0;
    public int turnIdx = 0;
    public float MoveSpeed = 3f;
    public float MoveSpeedVariance = 0f;
    public float MoveMin = -0.6f;
    public float MoveMax = 1f;
    public float TurnSpeed = 180f;
    public float TurnSpeedVariance = 0f;
    public float TurnMin = -1f;
    public float TurnMax = 1f;
    public float TagSpeedChange = 1f;
    public string Tag = "";

    public override void RunAction(BaseAgent agent, float[] act) {
        GameObject gameObject = agent.gameObject;
        Transform transform = gameObject.transform;
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        if (rigidbody == null) return;

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        var forwardAxis = (int)act[forwardIdx];
        var rightAxis = (int)act[rightIdx];
        var rotateAxis = (int)act[turnIdx];
        
        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.forward;
                break;
            case 2:
                dirToGo = -transform.forward;
                break;
        }
        
        switch (rightAxis)
        {
            case 1:
                dirToGo = transform.right;
                break;
            case 2:
                dirToGo = -transform.right;
                break;
        }

        switch (rotateAxis)
        {
            case 1:
                rotateDir = -transform.up;
                break;
            case 2:
                rotateDir = transform.up;
                break; 
        }

        float MoveSpeedRand = Random.Range(MoveSpeed - MoveSpeedVariance, MoveSpeed + MoveSpeedVariance);
        float TurnSpeedRand = Random.Range(TurnSpeed - TurnSpeedVariance, TurnSpeed + TurnSpeedVariance);
        float TagMod = Tag != "" && agent.gameObject.CompareTag(Tag) ? TagSpeedChange : 1f;

        rigidbody.AddForce(dirToGo * MoveSpeedRand * TagMod, ForceMode.VelocityChange);
        gameObject.transform.Rotate(rotateDir, Time.fixedDeltaTime * TurnSpeedRand);

        agent.area.EventSystem.RaiseEvent(TransformEvent.Create(gameObject));
    }
}
