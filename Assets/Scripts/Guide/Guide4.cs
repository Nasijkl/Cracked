using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide4 : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    private const string SetFCalledKey1 = "SetFCalled1"; // 用于标记 SetF 是否被调用过的键

    void Start()
    {
        // 检查 SetF 是否已被调用过
        if (PlayerPrefs.GetInt(SetFCalledKey1, 0) == 0)
        {
            //SetF();
        }
        else
        {
            Debug.Log("SetFCalledKey set to: " + PlayerPrefs.GetInt(SetFCalledKey1));
            g1.SetActive(false);
        }
    }

    public void SetF()
    {
        g1.SetActive(false);
        g2.SetActive(true);

        // 标记 SetF 为已调用
        PlayerPrefs.SetInt(SetFCalledKey1, 1);
        PlayerPrefs.Save(); // 确保更改被保存

        // 调试输出
        Debug.Log("SetFCalledKey set to: " + PlayerPrefs.GetInt(SetFCalledKey1));
    }
}
