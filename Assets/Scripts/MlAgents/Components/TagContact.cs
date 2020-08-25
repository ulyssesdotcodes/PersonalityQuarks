using UnityEngine;
using System.Collections.Generic;
// Maintains a count of the number of objects with a specific tag touching the collider
public class TagContact : MonoBehaviour
{
    public HashSet<GameObject> Touching = new HashSet<GameObject>();
    public string Tag;
    public bool ChangeTag;
    public bool ResetTag;
    public string NewTag;
    public bool ResetPosition;
    public float ResetSpawnDistance = 0f;
    private PersonalityQuarksArea area;

    void Start()
    {
        area = GetComponentInParent<PersonalityQuarksArea>();
    }

    void Update()
    {
        Touching.Remove(null);
    }
    public void Add(GameObject gameObject)
    {
        bool added = Tag == "" || gameObject.CompareTag(Tag);
        if (added)
        {
            Touching.Add(gameObject);
        }

        if (added && ChangeTag)
        {
            gameObject.tag = NewTag;
        }

        if (added && ResetPosition)
        {
            Vector2 pos = new Vector2(Random.Range(-ResetSpawnDistance, ResetSpawnDistance), Random.Range(-ResetSpawnDistance, ResetSpawnDistance));
            gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.y);
            if (area.EventSystem != null)
            {
                area.EventSystem.RaiseEvent(TransformEvent.Create(gameObject));
            }
        }
    }

    public void Remove(GameObject gameObject)
    {
        bool removed = Touching.Remove(gameObject);

        if (removed && ResetTag)
        {
            gameObject.tag = Tag;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Add(collider.gameObject);

        if (area.EventSystem != null)
        {
            area.EventSystem.RaiseEvent(TriggerEnterEvent.Create(gameObject, collider));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Remove(collider.gameObject);

        if (area.EventSystem != null)
        {
            area.EventSystem.RaiseEvent(TriggerExitEvent.Create(gameObject, collider));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Add(collision.collider.gameObject);

        if (area.EventSystem != null)
        {
            area.EventSystem.RaiseEvent(CollisionEnterEvent.Create(gameObject, collision));
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Remove(collision.collider.gameObject);

        if (area.EventSystem != null)
        {
            area.EventSystem.RaiseEvent(CollisionExitEvent.Create(gameObject, collision));
        }
    }
}