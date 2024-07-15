using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�

[CreateAssetMenu(fileName = "LandofReiki3", menuName = "Incident/IncidentPageData/LandofReiki/LandofReiki3")]
public class LandofReiki3 : IncidentPageData
{

    public override void Resolve()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // ��ȡPlayerHealth���
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            float healthGain = playerHealth.maxHealth * 0.1f;
            float staminaGain = 10f;
            if (playerHealth != null)
            {
                // ����25%�������ֵ
                
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healthGain, playerHealth.maxHealth);
            }
            else
            {
                Debug.LogError("Player������δ�ҵ� PlayerHealth ���");
            }

            InfiniteMapGenerator mapGenerator = FindObjectOfType<InfiniteMapGenerator>();
            if (mapGenerator != null)
            {
                // ����25������
                
                mapGenerator.stamina = Mathf.Min(mapGenerator.stamina + staminaGain, 100f); // �����������Ϊ100
                Debug.Log($"Gained {staminaGain} stamina. Current stamina: {mapGenerator.stamina}");
            }
            else
            {
                Debug.LogError("δ�ҵ� InfiniteMapGenerator ���");
            }
            DisplayRecoveryInfo(healthGain, playerHealth.currentHealth, staminaGain, mapGenerator.stamina);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Player' �Ķ���");
        }
    }
    private void DisplayRecoveryInfo(float healthGain, float currentHealth, float staminaGain, float currentStamina)
    {
        // ��ȡIncident Canvas�µ�Other�Ӷ���
        Transform otherContainer = GameObject.Find("Incident").transform.Find("Other");
        if (otherContainer != null)
        {
            foreach (Transform child in otherContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            // ������������ʾ����ֵ��TextMeshPro����
            GameObject healthTextObject = new GameObject("HealthText");
            healthTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI healthText = healthTextObject.AddComponent<TextMeshProUGUI>();
            healthText.text = $"HP {healthGain.ToString("F2")} Recovered , Now HP: {currentHealth}";
            healthText.color = Color.red;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

            // ������������ʾ����ֵ��TextMeshPro����
            GameObject staminaTextObject = new GameObject("StaminaText");
            staminaTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI staminaText = staminaTextObject.AddComponent<TextMeshProUGUI>();
            staminaText.text = $"Stamina {staminaGain.ToString("F2")} Recovered , Now Stamina: {currentStamina}";
            staminaText.color = Color.blue;
            staminaText.fontSize = 50;
            staminaText.fontStyle = FontStyles.Bold;
            staminaText.alignment = TextAlignmentOptions.Center;

            // ����Text��Ĵ�С��λ��
            RectTransform healthRectTransform = healthText.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta = new Vector2(1000, 100);
            healthRectTransform.anchoredPosition = new Vector2(0, 100);

            RectTransform staminaRectTransform = staminaText.GetComponent<RectTransform>();
            staminaRectTransform.sizeDelta = new Vector2(2000, 100);
            staminaRectTransform.anchoredPosition = new Vector2(0, -100);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Other' ���Ӷ���");
        }
    }

}