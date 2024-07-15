using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

[CreateAssetMenu(fileName = "LandofReiki3", menuName = "Incident/IncidentPageData/LandofReiki/LandofReiki3")]
public class LandofReiki3 : IncidentPageData
{

    public override void Resolve()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // 获取PlayerHealth组件
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            float healthGain = playerHealth.maxHealth * 0.1f;
            float staminaGain = 10f;
            if (playerHealth != null)
            {
                // 增加25%最大生命值
                
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healthGain, playerHealth.maxHealth);
            }
            else
            {
                Debug.LogError("Player对象上未找到 PlayerHealth 组件");
            }

            InfiniteMapGenerator mapGenerator = FindObjectOfType<InfiniteMapGenerator>();
            if (mapGenerator != null)
            {
                // 增加25点耐力
                
                mapGenerator.stamina = Mathf.Min(mapGenerator.stamina + staminaGain, 100f); // 假设最大耐力为100
                Debug.Log($"Gained {staminaGain} stamina. Current stamina: {mapGenerator.stamina}");
            }
            else
            {
                Debug.LogError("未找到 InfiniteMapGenerator 组件");
            }
            DisplayRecoveryInfo(healthGain, playerHealth.currentHealth, staminaGain, mapGenerator.stamina);
        }
        else
        {
            Debug.LogError("未找到名为 'Player' 的对象");
        }
    }
    private void DisplayRecoveryInfo(float healthGain, float currentHealth, float staminaGain, float currentStamina)
    {
        // 获取Incident Canvas下的Other子对象
        Transform otherContainer = GameObject.Find("Incident").transform.Find("Other");
        if (otherContainer != null)
        {
            foreach (Transform child in otherContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            // 创建并设置显示生命值的TextMeshPro对象
            GameObject healthTextObject = new GameObject("HealthText");
            healthTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI healthText = healthTextObject.AddComponent<TextMeshProUGUI>();
            healthText.text = $"HP {healthGain.ToString("F2")} Recovered , Now HP: {currentHealth}";
            healthText.color = Color.red;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

            // 创建并设置显示精力值的TextMeshPro对象
            GameObject staminaTextObject = new GameObject("StaminaText");
            staminaTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI staminaText = staminaTextObject.AddComponent<TextMeshProUGUI>();
            staminaText.text = $"Stamina {staminaGain.ToString("F2")} Recovered , Now Stamina: {currentStamina}";
            staminaText.color = Color.blue;
            staminaText.fontSize = 50;
            staminaText.fontStyle = FontStyles.Bold;
            staminaText.alignment = TextAlignmentOptions.Center;

            // 设置Text框的大小和位置
            RectTransform healthRectTransform = healthText.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta = new Vector2(1000, 100);
            healthRectTransform.anchoredPosition = new Vector2(0, 100);

            RectTransform staminaRectTransform = staminaText.GetComponent<RectTransform>();
            staminaRectTransform.sizeDelta = new Vector2(2000, 100);
            staminaRectTransform.anchoredPosition = new Vector2(0, -100);
        }
        else
        {
            Debug.LogError("未找到名为 'Other' 的子对象");
        }
    }

}