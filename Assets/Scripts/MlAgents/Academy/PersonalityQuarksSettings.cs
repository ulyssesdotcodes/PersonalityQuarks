using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PersonalityQuarksSettings : MonoBehaviour
{
    /* public float PlayAreaDistance; */
    /* public float PositionY; */

    /* private GameObject RedTarget; */
    /* private GameObject BlueTarget; */

    private List<PersonalityQuarksArea> Areas;
    public bool RecordReplay = false;
    public bool Replay = false;
    public float TimeScale = 1f;

    public void Awake()
    {
        PersonalityQuarksArea[] listArea = FindObjectsOfType<PersonalityQuarksArea>();
        foreach (PersonalityQuarksArea ba in listArea)
        {
            ba.ResetArea();
        }
    }

    public void Start()
    {
        if (TimeScale > 0)
        {
            Time.timeScale = TimeScale;
        }
    }

    public void EnvironmentReset()
    {
        PersonalityQuarksArea[] listArea = FindObjectsOfType<PersonalityQuarksArea>();
        foreach (PersonalityQuarksArea ba in listArea)
        {
            ba.ResetArea();
        }
    }
}
