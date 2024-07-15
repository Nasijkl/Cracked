using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AncientLibrary2", menuName = "Incident/IncidentPageData/AncientLibrary/AncientLibrary2")]

public class AncientLibrary2 : IncidentPageData
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
                        eventContentText.text = "You find some consumption. (Discover a card)";
                        Transform otherContainer = incidentCanvas.transform.Find("Other");
                        if (otherContainer != null)
                        {
                            cardContainer = otherContainer;

                            foreach (Transform child in otherContainer)
                            {
                                GameObject.Destroy(child.gameObject);
                            }

                            // 创建一个专门用于放置卡牌的子容器
                            cardDisplayContainer = new GameObject("CardDisplayContainer").transform;
                            cardDisplayContainer.SetParent(cardContainer, false);
                        }
                        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
                        if (deckManager != null)
                        {
                            // 从all_kind_card中随机抽取一张卡牌
                            if (deckManager.all_kind_card.Count > 0)
                            {
                                int randomIndex = Random.Range(0, deckManager.all_kind_card.Count);
                                CrackedCardData randomCard = deckManager.all_kind_card[randomIndex];

                                // 将随机抽取的卡牌添加到卡组中
                                deckManager.addCard(randomCard);

                                Debug.Log("抽取了一张卡牌 " + randomCard.name);

                                // 显示卡牌的图像和名字
                                DisplayCard(randomCard);
                            }
                            else
                            {
                                Debug.Log("all_kind_card 列表为空");
                            }
                        }
                    }
                    else
                    {
                        eventContentText.text = "You find a fairy.(Heal your health and stamina to full)";
                        Transform otherContainer = incidentCanvas.transform.Find("Other");
                        if (otherContainer != null)
                        {
                            cardContainer = otherContainer;

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

                                playerHealth.currentHealth = playerHealth.maxHealth;
                            }
                            else
                            {
                                Debug.LogError("Player对象上未找到 PlayerHealth 组件");
                            }

                            InfiniteMapGenerator mapGenerator = FindObjectOfType<InfiniteMapGenerator>();
                            if (mapGenerator != null)
                            {

                                mapGenerator.stamina = 100f; // 假设最大耐力为100
                            }
                            else
                            {
                                Debug.LogError("未找到 InfiniteMapGenerator 组件");
                            }
                            DisplayRecoveryInfo(playerHealth.currentHealth,mapGenerator.stamina);
                        }
                        else
                        {
                            Debug.LogError("未找到名为 'Player' 的对象");
                        }
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
    private void DisplayCard(CrackedCardData card)
    {
        // 清空cardDisplayContainer中的所有子对象
        foreach (Transform child in cardDisplayContainer)
        {
            Destroy(child.gameObject);
        }

        // 为卡牌创建一个容器，并使用构造的名称
        string cardName = $"{card.name}";
        GameObject cardObject = new GameObject(cardName);

        // 设置父对象为cardDisplayContainer
        cardObject.transform.SetParent(cardDisplayContainer, false);
        cardObject.transform.localScale = new Vector3(temp * 10, temp * 10, 1f);

        // 使用RectTransform来设置位置
        RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1, 1, 1); // 保持原始UI比例

        for (int j = 0; j < 4; j++)
        {
            // 为每个CardPieceData创建一个Image组件
            GameObject pieceObject = new GameObject($"Piece_{j}");
            pieceObject.transform.SetParent(cardObject.transform, false);
            if (card.card_pieces[j] != null)
            {
                Image pieceImage = pieceObject.AddComponent<Image>();
                pieceImage.sprite = card.card_pieces[j].sprite;
                // 设置RectTransform以适应父对象
                RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                pieceRectTransform.anchoredPosition = Vector2.zero;
                pieceRectTransform.sizeDelta = new Vector2(10 * 15, 15 * 15); // 根据需要调整大小
            }
        }
    }
    private void DisplayRecoveryInfo(float currentHealth,float currentStamina)
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
            healthText.text = $"Now HP: {currentHealth}";
            healthText.color = Color.red;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

            // 创建并设置显示精力值的TextMeshPro对象
            GameObject staminaTextObject = new GameObject("StaminaText");
            staminaTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI staminaText = staminaTextObject.AddComponent<TextMeshProUGUI>();
            staminaText.text = $"Now Stamina: {currentStamina}";
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
