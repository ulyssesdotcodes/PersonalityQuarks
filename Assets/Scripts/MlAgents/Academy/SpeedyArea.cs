using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyArea : Area
{
    public GameObject Actor;
    public string spawnkey;
    public float SpawnRange;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void ResetArea() {
        List<GameObject> gos = FindGameObjectsWithTagInChildren("actor");
        foreach(GameObject go in gos) {
            Destroy(go);
        }

        int SpawnNumber = (int) AcademyParameters.FetchOrParse(academy, spawnkey);
        for(int i = 0; i < SpawnNumber; i++) {
            GameObject speedy =
                GameObject.Instantiate(Actor, 
                    new Vector3(
                        Random.Range(-SpawnRange, SpawnRange), 
                        base.StartY + 0.5f, 
                        Random.Range(-SpawnRange, SpawnRange)), 
                    Quaternion.identity,
                    gameObject.transform);
        }
    }
}
