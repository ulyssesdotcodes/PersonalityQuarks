using UnityEngine;
using MLAgents;

[CreateAssetMenu(menuName="ML/Rewards/Consumable")]
class MLRewardConsumable : MLReward {
    public string SpawnDistanceKeyVal = "4";
    public string MultKeyVal = "0";
    private float Mult;
    private float SpawnDistance;
    Academy academy;

    public override void Initialize(BaseAgent agent) {
        academy = FindObjectOfType<Academy>();
        Mult = AcademyParameters.FetchOrParse(academy, MultKeyVal);
        SpawnDistance = AcademyParameters.FetchOrParse(academy, SpawnDistanceKeyVal);
    }

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        Mult = AcademyParameters.Update(academy, MultKeyVal, Mult);
        agent.TriggerCollider
            .Filter(tc => tc != null)
            .Map(tc => tc.gameObject)
            .Filter(gob => gob.tag == "consumable")
            .MatchSome(go => {
                Consumable consumable = go.GetComponent<Consumable>();
                agent.AddReward(consumable.value * Mult);

                SpawnDistance = AcademyParameters.Update(academy, SpawnDistanceKeyVal, SpawnDistance);
                Vector2 pos = new Vector2(Random.Range(-SpawnDistance, SpawnDistance), Random.Range(-SpawnDistance, SpawnDistance));
                go.transform.position = new Vector3(pos.x, go.transform.position.y, pos.y);
            });
    }
}
