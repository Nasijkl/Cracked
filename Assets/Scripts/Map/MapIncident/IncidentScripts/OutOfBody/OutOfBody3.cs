using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "OutOfBody3", menuName = "Incident/IncidentPageData/OutOfBody/OutOfBody3")]
public class OutOfBody3 : IncidentPageData
{
    public TextMeshProUGUI eventContentText; // 显示事件内容的TextMeshProUGUI组件
    public Transform cardContainer; // 用于放置卡牌信息的UI父对象
    public float temp = 1.0f; // 缩放比例
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
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                // 获取PlayerHealth组件
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {

                    playerHealth.currentHealth -= 20;
                }
                else
                {
                    Debug.LogError("Player对象上未找到 PlayerHealth 组件");
                }
                DisplayRecoveryInfo(playerHealth.currentHealth);
            }
        }
    }
    private void DisplayRecoveryInfo(float currentHealth)
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
            healthText.text = $"Lose 20 HP. Now HP: {currentHealth}";
            healthText.color = Color.red;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

            

            // 设置Text框的大小和位置
            RectTransform healthRectTransform = healthText.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta = new Vector2(1000, 100);
            healthRectTransform.anchoredPosition = new Vector2(0, 100);
        }
        else
        {
            Debug.LogError("未找到名为 'Other' 的子对象");
        }
    }
}