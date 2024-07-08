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
            // ��ȡPlayer��λ��
            Vector3 playerPosition = player.transform.position;

            // ����10x10��Χ�İ뾶
            float range = 10.0f; // �뾶Ϊ5��ֱ��Ϊ10

            // ʹ��Physics.OverlapBox����ⷶΧ�ڵ����ж���
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(playerPosition, new Vector2(range, range), 0f);
            Debug.Log("��⵽�Ķ�������: " + hitColliders.Length);
            foreach (Collider2D hitCollider in hitColliders)
            {
                // �������Tag�Ƿ�ΪFog
                if (hitCollider.CompareTag("Fog"))
                {
                    // �ݻٶ���
                    GameObject.Destroy(hitCollider.gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("δ�ҵ�TagΪ'Player'��GameObject");
        }
    }
}
