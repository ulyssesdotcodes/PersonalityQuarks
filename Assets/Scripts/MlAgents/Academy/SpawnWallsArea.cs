using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName="ML/AreaResets/SpawnWalls")]
public class SpawnWallsArea : AreaReset
{
    public string SpawnNumberKeyVal = "5";
    public string SpawnDistanceKeyVal = "4";
    public GameObject[] Walls;
    private List<GameObject> Spawned = new List<GameObject>();


    private int SpawnNumber;
    private float SpawnDistance;

    // Start is called before the first frame update
    public override void Init(Area area)
    {
        SpawnNumber = (int)AcademyParameters.FetchOrParse(area.academy, SpawnNumberKeyVal);
        SpawnDistance = AcademyParameters.FetchOrParse(area.academy, SpawnDistanceKeyVal);

        ResetArea(area);
    }

    public override void ResetArea(Area area) {
        SpawnNumber = (int) AcademyParameters.Update(area.academy, SpawnNumberKeyVal, (int)SpawnNumber);
        SpawnDistance = AcademyParameters.Update(area.academy, SpawnDistanceKeyVal, SpawnDistance);

        foreach(GameObject go in Spawned) {
            GameObject.Destroy(go);
        }

        Spawned.Clear();

        for(int i = 0; i < SpawnNumber; i++) {
          SpawnWall(area, new Vector2(Random.Range(-SpawnDistance, SpawnDistance), Random.Range(-SpawnDistance, SpawnDistance)));
        }
    }

    private void SpawnWall(Area area, Vector2 position) {
      GameObject prefab = Walls[(int)Random.Range(0, Walls.Length)];
      GameObject wall = GameObject.Instantiate(prefab, new Vector3(position.x, area.StartY + 1f, position.y), Quaternion.EulerAngles(0, Random.Range(0, 360), 0));
      wall.transform.SetParent(area.gameObject.transform);
      area.EventSystem.RaiseEvent(CreateEvent.Create(AssetDatabase.GetAssetPath(prefab), wall));
      Spawned.Add(wall);
    }
}
