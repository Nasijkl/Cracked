using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncidentPageData", menuName = "Incident/IncidentPageData")]
public class IncidentPageData : ScriptableObject
{
    public int id;
    public string text;
    public Sprite sprite; // 先不实现
    public List<int> pageList;
    public List<string> pageTextList;

    public virtual void Resolve()
    {
        // 这里实现进入事件后的效果，子类可以覆盖这个方法来实现具体效果
    }
}
