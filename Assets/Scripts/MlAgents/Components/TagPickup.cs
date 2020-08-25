using System;
using UnityEngine;
using System.Collections.Generic;
// Picks up objects as a triggered action
[RequireComponent(typeof(TagContact))]
[RequireComponent(typeof(Rigidbody))]
public class TagPickup : MonoBehaviour
{
    public float Force = 20f;
    public int DropTimeLength = 100;
    public bool ChangeTag = false;
    public bool ResetTag = false;
    public string NewTag;
    private TagContact tagContact;
    private DateTime DropTime;
    private HashSet<GameObject> PickedUp = new HashSet<GameObject>();
    public PersonalityQuarksArea area;
    private Rigidbody rigidbody;

    public void Start()
    {
        tagContact = GetComponent<TagContact>();
        rigidbody = GetComponent<Rigidbody>();
        area = GetComponentInParent<PersonalityQuarksArea>();
    }

    public void Update()
    {
        if (!IsDropped())
        {
            foreach (GameObject go in tagContact.Touching)
            {
                if (go == null)
                {
                    continue;
                }

                if (!PickedUp.Contains(go))
                {
                    if (ChangeTag)
                    {
                        go.tag = NewTag;
                    }

                    PickedUp.Add(go);
                    FixedJoint fj = go.AddComponent<FixedJoint>();
                    fj.connectedBody = rigidbody;
                }

                break;
            }
        }
    }

    public void Drop()
    {
        // Make sure we don't overlap drop times repeatedly
        if (!IsDropped())
        {
            DropTime = DateTime.Now;

            foreach (GameObject go in PickedUp)
            {
                if (go == null)
                {
                    continue;
                }

                if (ResetTag)
                {
                    go.tag = tagContact.Tag;
                }

                Destroy(go.GetComponent<FixedJoint>());
            }

            PickedUp.Clear();
        }
    }

    public bool IsDropped()
    {
        return DropTime != null && (DateTime.Now - DropTime).TotalMilliseconds < DropTimeLength;
    }

    public int Count()
    {
        return PickedUp.Count;
    }
}