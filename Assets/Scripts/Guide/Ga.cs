using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ga : MonoBehaviour
{
    private const string SetFCalledKey = "SetFCalled"; // 与 Guide1.cs 中相同的键
    private const string SetFCalledKey1 = "SetFCalled1";

    void Awake()
    {
        // 在游戏启动时重置 SetFCalledKey 为 0
        PlayerPrefs.SetInt(SetFCalledKey, 0);
        PlayerPrefs.SetInt(SetFCalledKey, 0);
        PlayerPrefs.Save();
    }
}
