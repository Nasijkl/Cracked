using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�

[CreateAssetMenu(fileName = "SkeletalPath3", menuName = "Incident/IncidentPageData/SkeletalPath/SkeletalPath3")]
public class SkeletalPath3 : IncidentPageData
{
    public override void Resolve()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // ��ȡPlayerHealth���
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // ����5��health
                playerHealth.TakeDamage(5f);
                Debug.Log("SkeletalPath3 Resolve ����������: ������5��health");

                // ��ʾ��-5���ĺ�ɫ����
                ShowDamageText(player.transform.position, "HP -5");
            }
            else
            {
                Debug.LogError("Player������δ�ҵ� PlayerHealth ���");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Player' �Ķ���");
        }
    }

    private void ShowDamageText(Vector3 position, string text)
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
                // ����TextMeshPro����
                GameObject damageTextObject = new GameObject("DamageText");
                damageTextObject.transform.SetParent(otherContainer);

                // ���TextMeshPro���
                TextMeshProUGUI damageText = damageTextObject.AddComponent<TextMeshProUGUI>();
                damageText.text = text;
                damageText.color = Color.red;
                damageText.fontSize = 100;
                damageText.fontStyle = FontStyles.Bold; // ��������Ӵ�
                damageText.alignment = TextAlignmentOptions.Center;

                // ����Text��Ĵ�С
                RectTransform rectTransform = damageText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 4f, rectTransform.sizeDelta.y * 3f);

                // ����λ��
                damageText.transform.position = Camera.main.WorldToScreenPoint(position);
            }
            else
            {
                Debug.LogError("δ�ҵ���Ϊ 'Other' ���Ӷ���");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Incident' �� Canvas");
        }
    }
}