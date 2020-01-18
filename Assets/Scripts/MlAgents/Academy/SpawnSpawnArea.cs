using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName="ML/AreaResets/SpawnSpawn")]
public class SpawnSpawnArea : AreaReset
{
    public List<SpawnWallsArea> Spawns;
    private SpawnWallsArea last;

    private List<SpawnWallsArea> mySpawns = new List<SpawnWallsArea>();

    // Start is called before the first frame update
    public override void Init(Area area)
    {
      foreach(SpawnWallsArea spawn in Spawns) {
        SpawnWallsArea mySpawn = Object.Instantiate(spawn);
        mySpawns.Add(mySpawn);
        mySpawn.ResetArea(area);
        mySpawn.Clear();
      }

      ResetArea(area);
    }

    public override void ResetArea(Area area) {
      if(last != null) {
        last.Clear();
      }

      SpawnWallsArea prefab = mySpawns[(int)Random.Range(0, Spawns.Count)];
      prefab.SpawnWalls(area);
      last = prefab;
    }
}
