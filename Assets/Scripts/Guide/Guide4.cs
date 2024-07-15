using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide4 : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    private const string SetFCalledKey1 = "SetFCalled1"; // ���ڱ�� SetF �Ƿ񱻵��ù��ļ�

    void Start()
    {
        // ��� SetF �Ƿ��ѱ����ù�
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

        // ��� SetF Ϊ�ѵ���
        PlayerPrefs.SetInt(SetFCalledKey1, 1);
        PlayerPrefs.Save(); // ȷ�����ı�����

        // �������
        Debug.Log("SetFCalledKey set to: " + PlayerPrefs.GetInt(SetFCalledKey1));
    }
}
