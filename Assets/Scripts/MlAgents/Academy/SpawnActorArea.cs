using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.MLAgents;


[CreateAssetMenu(menuName = "ML/AreaResets/SpawnActors")]
public class SpawnActorArea : AreaReset
{
    public GameObject Actor;
    public string SpawnNumberKeyVal;
    public float SpawnRange;
    public float SpawnHeight = 0f;
    public bool SpawnY = false;
    private List<GameObject> Spawned = new List<GameObject>();
    private List<ClosestTagTarget> TagTargets = new List<ClosestTagTarget>();

    // Start is called before the first frame update
    public override void Init(PersonalityQuarksArea area)
    {
        ResetArea(area);
    }

    public override void ResetArea(PersonalityQuarksArea area)
    {
        foreach (GameObject go in Spawned)
        {
            GameObject.Destroy(go);
        }

        Spawned.Clear();

        int SpawnNumber = (int)AcademyParameters.FetchOrParse(Academy.Instance, SpawnNumberKeyVal);
        for (int i = 0; i < SpawnNumber; i++)
        {
            CreateEvent createEvent = CreateEvent.Create(
                AssetDatabase.GetAssetPath(Actor),
                Actor.name + "_" + i
            );
            GameObject gob = area.SpawnPrefab(createEvent);
            gob.transform.localPosition =
                new Vector3(
                    Random.Range(-SpawnRange, SpawnRange),
                    SpawnY ?
                        Random.Range(-SpawnRange, SpawnRange) :
                        area.StartY + SpawnHeight,
                    Random.Range(-SpawnRange, SpawnRange)
                );

            if (area.EventSystem != null && Application.isEditor)
            {
                area.EventSystem.RaiseEvent(createEvent);
                area.EventSystem.RaiseEvent(TransformEvent.Create(gob));
            }

            Spawned.Add(gob);
        }
    }
}