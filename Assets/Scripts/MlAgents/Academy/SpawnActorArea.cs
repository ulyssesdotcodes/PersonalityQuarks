using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName="ML/AreaResets/SpawnActors")]
public class SpawnActorArea : AreaReset
{
    public GameObject Actor;
    public string SpawnNumberKeyVal;
    public float SpawnRange;
    private List<GameObject> Spawned = new List<GameObject>();

    // Start is called before the first frame update
    public override void Init(PersonalityQuarksArea area)
    {
        ResetArea(area);
    }

    public override void ResetArea(PersonalityQuarksArea area) {
        foreach(GameObject go in Spawned) {
            GameObject.Destroy(go);
        }

        Spawned.Clear();

        int SpawnNumber = (int) AcademyParameters.FetchOrParse(area.academy, SpawnNumberKeyVal);
        for(int i = 0; i < SpawnNumber; i++) {
            GameObject gob =
                GameObject.Instantiate(Actor, 
                    new Vector3(
                        Random.Range(-SpawnRange, SpawnRange), 
                        area.StartY + 0.5f, 
                        Random.Range(-SpawnRange, SpawnRange)), 
                    Quaternion.identity,
                    area.gameObject.transform);

            if (area.EventSystem != null) {
              area.EventSystem.RaiseEvent(CreateEvent.Create(Actor.name, gob));
            }
            Spawned.Add(gob);
        }
    }
}
