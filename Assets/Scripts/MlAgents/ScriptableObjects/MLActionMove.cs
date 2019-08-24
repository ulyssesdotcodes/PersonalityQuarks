using UnityEngine;

[CreateAssetMenu(menuName="ML/Actions/Move")]
class MLActionMove : MLAction {
    public int forwardIdx = 0;
    public int turnIdx = 0;
    public float MoveSpeed = 3f;
    public float MoveMin = -0.6f;
    public float MoveMax = 1f;
    public float TurnSpeed = 180f;
    public float TurnMin = -1f;
    public float TurnMax = 1f;

    public override void RunAction(float[] vectorActions, GameObject gameObject) {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        if (rigidbody == null) return;

        Vector3 dirToGo = 
            Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0) * 
            Vector3.forward* Mathf.Clamp(vectorActions[forwardIdx], MoveMin, MoveMax);
        Vector3 rotateDir = gameObject.transform.up * Mathf.Clamp(vectorActions[turnIdx], TurnMin, TurnMax);

        rigidbody.AddForce(dirToGo * MoveSpeed, ForceMode.VelocityChange);
        gameObject.transform.Rotate(rotateDir, Time.fixedDeltaTime * TurnSpeed);
    }
}
