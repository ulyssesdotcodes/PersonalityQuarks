using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEachotherArea : Area
{
    public string SpawnNumberKeyVal = "5";
    public string SpawnDistanceKeyVal = "4";
    public GameObject CubeWall;
    private List<GameObject> Spawned = new List<GameObject>();
    private List<GameObject> ToDestroy = new List<GameObject>();


    private int SpawnNumber;
    private float SpawnDistance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        SpawnNumber = (int)AcademyParameters.FetchOrParse(academy, SpawnNumberKeyVal);
        SpawnDistance = AcademyParameters.FetchOrParse(academy, SpawnDistanceKeyVal);
    }

    public override void ResetArea() {
        SpawnNumber = (int) AcademyParameters.Update(academy, SpawnNumberKeyVal, (int)SpawnNumber);
        SpawnDistance = AcademyParameters.Update(academy, SpawnDistanceKeyVal, SpawnDistance);

        foreach(GameObject go in Spawned) {
            /* ToDestroy.Add(go); */
            Destroy(go);
        }

        Spawned.Clear();

        Debug.Log(SpawnNumber);
        for(int i = 0; i < SpawnNumber; i++) {
            SpawnCubeWall(new Vector2(Random.Range(-SpawnDistance, SpawnDistance), Random.Range(-SpawnDistance, SpawnDistance)));
        }
        /* StartCoroutine(CleanUp()); */
    }

    private IEnumerator CleanUp() {
        yield return new WaitForSeconds(0.016f);

        foreach(GameObject go in ToDestroy) {
            Destroy(go);
        }

        ToDestroy.Clear();
    }

    private void SpawnCubeWall(Vector2 position) {
        SpawnWall(position, CubeWall);
    }

    private void SpawnWall(Vector2 position, GameObject prefab) {
        GameObject wall = GameObject.Instantiate(prefab, new Vector3(position.x, base.StartY + 1f, position.y), Quaternion.identity);
        wall.transform.SetParent(gameObject.transform);
        Spawned.Add(wall);
    }
}
