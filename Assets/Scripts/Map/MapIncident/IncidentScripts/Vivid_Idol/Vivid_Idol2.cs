using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Vivid_Idol2", menuName = "Incident/IncidentPageData/Vivid_Idol/Vivid_Idol2")]
public class Vivid_Idol2 : IncidentPageData
{
    public Transform cardContainer; // 用于放置卡牌信息的UI父对象
    public float temp = 1.0f; // 缩放比例

    private Transform cardDisplayContainer; // 专门用于放置卡牌的子容器
    public override void Resolve()
    {
        GameObject incidentCanvas = GameObject.Find("Incident");
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
        // 在运行时查找GlobalDeckManager的实例
        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null)
        {
            // 从all_kind_card中查找名为"SoulSteal"的卡牌
            CrackedCardData soulStealCard = deckManager.all_kind_card.Find(card => card.name == "SoulSteal");
            if (soulStealCard != null)
            {
                // 如果找到了名为"SoulSteal"的卡牌，则将其添加到卡组中
                //deckManager.card_deck.Add(soulStealCard);
                deckManager.addCard(soulStealCard);
                DisplayCard(soulStealCard);
            }
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
}



