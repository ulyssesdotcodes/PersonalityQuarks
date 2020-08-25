using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/AreaResets/SpawnWalls")]
public class SpawnWallsArea : AreaReset
{
    public string SpawnNumberKeyVal = "5";
    public string SpawnDistanceKeyVal = "4";
    public GameObject[] Walls;
    private List<GameObject> Spawned = new List<GameObject>();


    private int SpawnNumber;
    private float SpawnDistance;

    // Start is called before the first frame update
    public override void Init(PersonalityQuarksArea area)
    {
        SpawnNumber = (int)AcademyParameters.FetchOrParse(Academy.Instance, SpawnNumberKeyVal);
        SpawnDistance = AcademyParameters.FetchOrParse(Academy.Instance, SpawnDistanceKeyVal);

        ResetArea(area);
    }

    public override void ResetArea(PersonalityQuarksArea area)
    {
        Clear();
        SpawnWalls(area);
    }

    public void Clear()
    {
        foreach (GameObject go in Spawned)
        {
            GameObject.Destroy(go);
        }

        Spawned.Clear();
    }

    public void SpawnWalls(PersonalityQuarksArea area)
    {
        SpawnNumber = (int)AcademyParameters.Update(Academy.Instance, SpawnNumberKeyVal, (int)SpawnNumber);
        SpawnDistance = AcademyParameters.Update(Academy.Instance, SpawnDistanceKeyVal, SpawnDistance);

        for (int i = 0; i < SpawnNumber; i++)
        {
            SpawnWall(area, new Vector2(Random.Range(-SpawnDistance, SpawnDistance), Random.Range(-SpawnDistance, SpawnDistance)));
        }
    }

    private void SpawnWall(PersonalityQuarksArea area, Vector2 position)
    {
        GameObject prefab = Walls[(int)Random.Range(0, Walls.Length)];
        float SpawnHeight = prefab.transform.position.y;
        Quaternion rot = prefab.transform.rotation * Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);

        GameObject wall = GameObject.Instantiate(
            prefab, new Vector3(position.x, area.StartY + SpawnHeight, position.y),
            rot,
            area.gameObject.transform);

        if (area.EventSystem != null && Application.isEditor)
        {
            area.EventSystem.RaiseEvent(CreateEvent.Create(wall, AssetDatabase.GetAssetPath(prefab)));
        }

        Spawned.Add(wall);
    }
}
