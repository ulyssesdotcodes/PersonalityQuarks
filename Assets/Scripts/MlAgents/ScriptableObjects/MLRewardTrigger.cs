using UnityEngine;
using MLAgents;
using OptionalUnity;

[CreateAssetMenu(menuName="ML/Rewards/Trigger")]
class MLRewardTrigger : MLReward {
    public string Tag;
    public string Label;
    public float Reward = 0;
    public bool Remove = true;
    public bool Reset = true;

    public override void AddReward(BaseAgent agent, float[] vectorActions) {
        bool allHaveTag = true;

        Option<GameObject> triggerCol = 
            agent.TriggerCollider
                .Map(tc => tc.gameObject)
                .Filter(tc => tc.gameObject.tag == Tag);

        triggerCol.MatchSome(_ => agent.AddReward(Reward));

        triggerCol
            .Filter((GameObject go) => Remove)
            .MatchSome(tc => GameObject.Destroy(tc));

        triggerCol
            .Filter((GameObject go) => Reset)
            .MatchSome(_ => agent.Reset());
        
        triggerCol
            .Filter(_ => Label != "")
            .FlatMap(tc => tc.GetComponent<Labels>().SomeNotNull())
            .MatchSome(lc => {
                lc.LabelsHash.Add(Label);
            });


        /* if (base) { */
        /*     agent.AddReward(reward); */

        /*     if (Remove) { */
        /*         foreach(Labels labels in LabelObjects) { */
        /*             labels.Labels.Remove(Label); */
        /*         } */
        /*     } */

        /*     if (Reset) { */
        /*         agent.Done(); */
        /*         agent.Reset(); */
        /*     } */
        /* } */
    }
}
