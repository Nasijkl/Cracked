using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide1 : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    public void SetF()
    {
        g1.SetActive(false);
        g2.SetActive(true);
    }
}
