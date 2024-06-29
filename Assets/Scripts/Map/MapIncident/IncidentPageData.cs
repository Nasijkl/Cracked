using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncidentPageData", menuName = "Incident/IncidentPageData")]
public class IncidentPageData : ScriptableObject
{
    public int id;
    public string text;
    public Sprite sprite; // �Ȳ�ʵ��
    public List<int> pageList;
    public List<string> pageTextList;

    public virtual void Resolve()
    {
        // ����ʵ�ֽ����¼����Ч����������Ը������������ʵ�־���Ч��
    }
}
