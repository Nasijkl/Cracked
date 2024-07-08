using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutOfBody4", menuName = "Incident/IncidentPageData/OutOfBody/OutOfBody4")]
public class OutOfBody4 : IncidentPageData
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
