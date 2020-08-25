using UnityEngine;
using Unity.MLAgents;

[CreateAssetMenu(menuName = "ML/Reset/Tag Contact")]
class MLResetTagContact : MLReset
{
    public string ColliderName;
    private TagContact tagContact;

    public override void Initialize(BaseAgent agent)
    {
        tagContact = agent.transform.Find(ColliderName).GetComponent<TagContact>();
    }

    public override void Reset(BaseAgent agent)
    {
        tagContact.Touching.Clear();
    }
}
