using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OutOfBody2", menuName = "Incident/IncidentPageData/OutOfBody/OutOfBody2")]
public class OutOfBody2 : IncidentPageData
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
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // 获取Player的位置
            Vector3 playerPosition = player.transform.position;

            // 定义10x10范围的半径
            float range = 10.0f; // 半径为5，直径为10

            // 使用Physics.OverlapBox来检测范围内的所有对象
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(playerPosition, new Vector2(range, range), 0f);
            Debug.Log("检测到的对象总数: " + hitColliders.Length);
            foreach (Collider2D hitCollider in hitColliders)
            {
                // 检查对象的Tag是否为Fog
                if (hitCollider.CompareTag("Fog"))
                {
                    // 摧毁对象
                    GameObject.Destroy(hitCollider.gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("未找到Tag为'Player'的GameObject");
        }
    }
}
