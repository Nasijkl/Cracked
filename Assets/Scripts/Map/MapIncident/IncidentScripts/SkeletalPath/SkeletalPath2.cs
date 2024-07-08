using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkeletalPath2", menuName = "Incident/IncidentPageData/SkeletalPath/SkeletalPath2")]
public class SkeletalPath2 : IncidentPageData
{
    public Transform cardContainer; // 用于放置卡牌信息的UI父对象
    public float temp = 1.0f; // 缩放比例

    private Transform cardDisplayContainer; // 专门用于放置卡牌的子容器

    public override void Resolve()
    {
        GameObject incidentCanvas = GameObject.Find("Incident");
        if (incidentCanvas != null)
        {
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
            else
            {
                Debug.LogError("未找到名为 'Other' 的子对象");
                return;
            }
        }
        else
        {
            Debug.LogError("未找到名为 'Incident' 的 Canvas");
            return;
        }

        // 在运行时查找GlobalDeckManager的实例
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

                Debug.Log("SkeletalPath2 Resolve 方法被调用: 抽取了一张卡牌 " + randomCard.name);

                // 显示卡牌的图像和名字
                DisplayCard(randomCard);
            }
            else
            {
                Debug.Log("SkeletalPath2 Resolve 方法被调用: all_kind_card 列表为空");
            }
        }
        else
        {
            Debug.Log("SkeletalPath2 Resolve 方法被调用: 未找到 GlobalDeckManager 实例");
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
        cardObject.transform.localScale = new Vector3(temp*10, temp*10, 1f);

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
                pieceRectTransform.sizeDelta = new Vector2(60*15, 75*15); // 根据需要调整大小
            }
        }
    }
}