using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkeletalPath4", menuName = "Incident/IncidentPageData/SkeletalPath/SkeletalPath4")]
public class SkeletalPath4 : IncidentPageData
{
    public override void Resolve()
    {
        GameObject incidentCanvas = GameObject.Find("Incident");
        if (incidentCanvas != null)
        {
            Transform otherContainer = incidentCanvas.transform.Find("Other");
            if (otherContainer != null)
            {
                foreach (Transform child in otherContainer)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
}