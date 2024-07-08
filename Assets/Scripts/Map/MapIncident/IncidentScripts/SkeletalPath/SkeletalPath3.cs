using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间

[CreateAssetMenu(fileName = "SkeletalPath3", menuName = "Incident/IncidentPageData/SkeletalPath/SkeletalPath3")]
public class SkeletalPath3 : IncidentPageData
{
    public override void Resolve()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // 获取PlayerHealth组件
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // 减少5点health
                playerHealth.TakeDamage(5f);
                Debug.Log("SkeletalPath3 Resolve 方法被调用: 减少了5点health");

                // 显示“-5”的红色文字
                ShowDamageText(player.transform.position, "HP -5");
            }
            else
            {
                Debug.LogError("Player对象上未找到 PlayerHealth 组件");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'Player' 的对象");
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
                // 创建TextMeshPro对象
                GameObject damageTextObject = new GameObject("DamageText");
                damageTextObject.transform.SetParent(otherContainer);

                // 添加TextMeshPro组件
                TextMeshProUGUI damageText = damageTextObject.AddComponent<TextMeshProUGUI>();
                damageText.text = text;
                damageText.color = Color.red;
                damageText.fontSize = 100;
                damageText.fontStyle = FontStyles.Bold; // 设置字体加粗
                damageText.alignment = TextAlignmentOptions.Center;

                // 设置Text框的大小
                RectTransform rectTransform = damageText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 4f, rectTransform.sizeDelta.y * 3f);

                // 设置位置
                damageText.transform.position = Camera.main.WorldToScreenPoint(position);
            }
            else
            {
                Debug.LogError("未找到名为 'Other' 的子对象");
            }
        }
        else
        {
            Debug.LogError("未找到名为 'Incident' 的 Canvas");
        }
    }
}