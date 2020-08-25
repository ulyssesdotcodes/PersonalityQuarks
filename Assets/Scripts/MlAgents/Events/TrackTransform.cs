using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    private PersonalityQuarksArea area;
    public void Start()
    {
        area = GetComponentInParent<PersonalityQuarksArea>();
    }
    public void Update()
    {
        area.EventSystem.RaiseEvent(TransformEvent.Create(gameObject));
    }
}