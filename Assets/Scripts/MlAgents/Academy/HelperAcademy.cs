using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class HelperAcademy : Academy
{
    /* public float PlayAreaDistance; */ 
    /* public float PositionY; */

    /* private GameObject RedTarget; */
    /* private GameObject BlueTarget; */

    private List<Area> Areas;

    public override void InitializeAcademy() {
    }


    public override void AcademyReset()
    {
        base.AcademyReset();
        Area[] listArea = FindObjectsOfType<Area>();
        foreach (Area ba in listArea)
        {
            Debug.Log("Resettinga rea");
            ba.ResetArea();
        }
    }
}
