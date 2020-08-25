using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using UnityEditor;

public class PersonalityQuarksArea : MonoBehaviour
{
    [HideInInspector]
    public float StartY;
    [HideInInspector]
    public PersonalityQuarksSettings settings;
    [HideInInspector]
    public QuarkEvents EventSystem;
    public DefaultAsset AnimationsPath;
    public AreaReset[] AreaResets;
    public bool Playback = false;
    private List<AreaReset> AreaResetClones;

    public Canvas WorldCanvas;

    private int lastReset;

    protected virtual void Start()
    {
        EventSystem = GetComponent<QuarkEvents>();

        StartY = gameObject.transform.position.y;

        AreaResetClones = new List<AreaReset>();
        foreach (AreaReset areaReset in AreaResets)
        {
            AreaReset ar = Object.Instantiate(areaReset) as AreaReset;
            AreaResetClones.Add(ar);
        }

        if (!Playback)
        {
            GetComponent<Animator>().enabled = false;

            foreach (AreaReset areaReset in AreaResetClones)
            {
                areaReset.ResetArea(this);
            }
        }

        lastReset = Time.frameCount;
    }


    public virtual void ResetArea()
    {
        if (Playback)
        {
            return;
        }

        if (Time.frameCount > 0 && lastReset != Time.frameCount)
        {
            foreach (AreaReset areaReset in AreaResetClones)
            {
                areaReset.Init(this);
            }
        }

        lastReset = Time.frameCount;
    }

    public List<GameObject> FindGameObjectsWithTagInChildren(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        List<GameObject> res = new List<GameObject>();

        foreach (GameObject go in gos)
        {
            if (go.GetComponentInParent<PersonalityQuarksArea>() == this)
            {
                res.Add(go);
            }
        }

        return res;
    }

    public GameObject SpawnPrefab(string jsonEvent)
    {
        return SpawnPrefab(JsonUtility.FromJson<CreateEvent>(jsonEvent));
    }

    public GameObject SpawnPrefab(CreateEvent createEvent)
    {
        if (!Application.isPlaying || transform.Find(createEvent.Name) != null)
        {
            return null;
        }

        GameObject gob = GameObject.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(createEvent.AssetPath, typeof(GameObject)));
        gob.transform.parent = transform;
        gob.name = createEvent.Name;

        Rigidbody rb = gob.GetComponent<Rigidbody>();
        if (rb != null && Playback)
        {
            rb.isKinematic = true;
        }

        return gob;
    }
}