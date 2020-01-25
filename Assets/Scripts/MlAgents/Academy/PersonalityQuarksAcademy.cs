using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PersonalityQuarksAcademy : Academy
{
    /* public float PlayAreaDistance; */ 
    /* public float PositionY; */

    /* private GameObject RedTarget; */
    /* private GameObject BlueTarget; */

    private List<PersonalityQuarksArea> Areas;

    public override void InitializeAcademy() {
        PersonalityQuarksArea[] listArea = FindObjectsOfType<PersonalityQuarksArea>();
        foreach (PersonalityQuarksArea ba in listArea)
        {
            ba.ResetArea();
        }
    }

    public override void AcademyReset()
    {
        base.AcademyReset();
        PersonalityQuarksArea[] listArea = FindObjectsOfType<PersonalityQuarksArea>();
        foreach (PersonalityQuarksArea ba in listArea)
        {
            ba.ResetArea();
        }
    }
}
