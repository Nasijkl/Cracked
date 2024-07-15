using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AncientLibrary4", menuName = "Incident/IncidentPageData/AncientLibrary/AncientLibrary4")]

public class AncientLibrary4 : IncidentPageData
{
    public TextMeshProUGUI eventContentText; // 显示事件内容的TextMeshProUGUI组件
    public Transform cardContainer; // 用于放置卡牌信息的UI父对象
    public float temp = 1.0f; // 缩放比例

    private Transform cardDisplayContainer; // 专门用于放置卡牌的子容器
    public override void Resolve()
    {
        // 查找Incident Canvas下的EventDescription的TMP UI组件
        GameObject incidentCanvas = GameObject.Find("Incident");
        if (incidentCanvas != null)
        {
            Transform eventDescriptionTransform = incidentCanvas.transform.Find("EventDescription");
            if (eventDescriptionTransform != null)
            {
                eventContentText = eventDescriptionTransform.GetComponent<TextMeshProUGUI>();
                if (eventContentText != null)
                {
                    float randomValue = UnityEngine.Random.value;
                    // 根据随机数决定text的内容
                    if (randomValue > 0.5f)
                    {
                        eventContentText.text = "Make a fortune! (Gain 100 gold coins)";
                        Transform otherContainer = incidentCanvas.transform.Find("Other");
                        if (otherContainer != null)
                        {
                            cardContainer = otherContainer;

                            foreach (Transform child in otherContainer)
                            {
                                GameObject.Destroy(child.gameObject);
                            }
                        }
                        GameObject coinObject = GameObject.Find("Coin");
                        if (coinObject != null)
                        {
                            // 获取Game脚本组件
                            Game gameScript = coinObject.GetComponent<Game>();
                            if (gameScript != null)
                            {
                                gameScript.Coins += 100;
                                // 读取Coins数值并显示
                                int coins = gameScript.Coins;
                                DisplayRecoveryInfo(coins.ToString());
                            }
                            else
                            {
                                Debug.LogError("在'coin'对象上未找到Game脚本组件");
                            }
                        }
                        else
                        {
                            Debug.LogError("未找到名为'coin'的GameObject");
                        }
                    }
                    else
                    {
                        eventContentText.text = "It seems not suitable for discovering today. (Nothing happens.)";
                    }
                }
                else
                {
                    Debug.LogError("EventDescription对象上未找到TextMeshProUGUI组件");
                }
            }
            else
            {
                Debug.LogError("未找到名为'EventDescription'的子对象");
            }
        }
        else
        {
            Debug.LogError("未找到名为'Incident'的Canvas");
        }
    }
    private void DisplayRecoveryInfo(string text)
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
            GameObject coinTextObject = new GameObject("CoinNumText");
            coinTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI healthText = coinTextObject.AddComponent<TextMeshProUGUI>();
            healthText.text = $"Gain 100 gold coins. Now Coins Num: {text}";
            healthText.color = Color.yellow;
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

