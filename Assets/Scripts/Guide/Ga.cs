using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ga : MonoBehaviour
{
    private const string SetFCalledKey = "SetFCalled"; // �� Guide1.cs ����ͬ�ļ�
    private const string SetFCalledKey1 = "SetFCalled1";

    void Awake()
    {
        // ����Ϸ����ʱ���� SetFCalledKey Ϊ 0
        PlayerPrefs.SetInt(SetFCalledKey, 0);
        PlayerPrefs.SetInt(SetFCalledKey, 0);
        PlayerPrefs.Save();
    }
}
