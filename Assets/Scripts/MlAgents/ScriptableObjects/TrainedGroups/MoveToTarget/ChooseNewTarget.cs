using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MoveToTarget))]
public class ChooseNewTarget : StartEpisodeAction
{
    public string TargetTag = "target";
    public Vector2 TolerenceRange = new Vector2(0.01f, 0.1f);
    private PersonalityQuarksArea area;
    private MoveToTarget moveToTarget;
    public Transform target;

    public void Start()
    {
        area = GetComponentInParent<PersonalityQuarksArea>();
        moveToTarget = GetComponent<MoveToTarget>();
    }

    public void FixedUpdate()
    {
        if (target == null && area.FindGameObjectsWithTagInChildren(TargetTag).Count > 0)
        {
            List<GameObject> gos = area.FindGameObjectsWithTagInChildren(TargetTag);
            int idx = Random.Range(0, gos.Count - 1);
            target = gos[idx].transform;

            moveToTarget.Tolerence = Random.Range(TolerenceRange.x, TolerenceRange.y);
        }

        if (target != null)
        {
            moveToTarget.Target = target.position;
        }
    }
}