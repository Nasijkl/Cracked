using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UICardDisplay : MonoBehaviour
{
    public List<CrackedCardData> card_deck = new List<CrackedCardData>();// 添加对CardBank的引用
    public Transform cardContainer; // 卡牌容器的引用，应指向ScrollView的Content对象
    public float temp;

    void Start()
    {
        GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
        if (globalManager != null)
        {
            card_deck = globalManager.card_deck;
        }
        DisplayCards();
    }
    public void DisplayCards()
    {
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
        int cardIndex = 0; // 用于追踪当前卡牌的索引
        foreach (var card in card_deck)
        {
            // 为每张卡牌创建一个容器，并使用构造的名称
            string cardName = $"{card.name}{cardIndex + 1}";
            GameObject cardObject = new GameObject(cardName);

            // 设置父对象为cardContainer
            cardObject.transform.SetParent(cardContainer, false);
            cardObject.transform.localScale = new Vector3(temp, temp, 1f);

            // 使用RectTransform来设置位置
            RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1); // 保持原始UI比例

            // 计算卡牌的位置
            int row = cardIndex / 4; // 计算当前卡牌所在的行
            int column = cardIndex % 4; // 计算当前卡牌所在的列
            float xPosition = -160 + column * (2 + rectTransform.sizeDelta.x); // 计算x位置
            float yPosition = 380 - row * (2 + rectTransform.sizeDelta.y); // 计算y位置
            rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

            for (int j = 0; j < card.card_pieces.Length; j++)
            {
                // 为每个CardPieceData创建一个Image组件
                GameObject pieceObject = new GameObject($"Piece_{j}");
                pieceObject.transform.SetParent(cardObject.transform, false);
                Image pieceImage = pieceObject.AddComponent<Image>();
                pieceImage.sprite = card.card_pieces[j].sprite;

                // 设置RectTransform以适应父对象
                RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                pieceRectTransform.anchoredPosition = Vector2.zero;
                pieceRectTransform.sizeDelta = new Vector2(60, 75); // 根据需要调整大小
            }

            cardIndex++; // 更新卡牌索引
        }
    }
    /*void DisplayCards()
    {
        int cardIndex = 0; // 用于追踪当前卡牌的索引
        foreach (var item in cardBank.Items)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                // 为每张卡牌创建一个容器，并使用构造的名称
                string cardName = $"{item.Card.name}{i + 1}";
                GameObject cardObject = new GameObject(cardName);

                // 设置父对象为cardContainer
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(temp, temp, 1f);

                // 使用RectTransform来设置位置
                RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1, 1, 1); // 保持原始UI比例

                // 计算卡牌的位置
                int row = cardIndex / 4; // 计算当前卡牌所在的行
                int column = cardIndex % 4; // 计算当前卡牌所在的列
                float xPosition =-160+column * (2 + rectTransform.sizeDelta.x); // 计算x位置
                float yPosition =380- row * (2 + rectTransform.sizeDelta.y); // 计算y位置
                rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // 为每个CardPieceData创建一个Image组件
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    Image pieceImage = pieceObject.AddComponent<Image>();
                    pieceImage.sprite = item.Card.card_pieces[j].sprite;

                    // 设置RectTransform以适应父对象
                    RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                    pieceRectTransform.anchoredPosition = Vector2.zero;
                    pieceRectTransform.sizeDelta = new Vector2(60, 75); // 根据需要调整大小
                }

                cardIndex++; // 更新卡牌索引
            }
        }
    }
    /*
    void DisplayCards()
    {
        int cardIndex = 0; // 用于追踪当前卡牌的索引
        int uiLayer = LayerMask.NameToLayer("UI");
        foreach (var item in cardBank.Items)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                // 为每张卡牌创建一个容器，并使用构造的名称
                string cardName = $"{item.Card.name}{i + 1}";
                GameObject cardObject = new GameObject(cardName);

                // 设置父对象为cardContainer，这里不需要调整localScale，因为GridLayoutGroup会控制大小
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(temp, temp,1f);
                // 设置GameObject的层级为UI
                cardObject.layer = uiLayer;

                // 计算卡牌的位置
                int row = cardIndex / 4; // 计算当前卡牌所在的行
                int column = cardIndex % 4; // 计算当前卡牌所在的列

                float xPosition = 50 + column * (80 + cardObject.transform.localScale.x); // 计算x位置
                float yPosition = -50 - row * (70 + cardObject.transform.localScale.y); // 计算y位置

                cardObject.transform.localPosition = new Vector3(xPosition, yPosition, -10f);

                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // 为每个CardPieceData创建一个SpriteRenderer
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    SpriteRenderer pieceSprite = pieceObject.AddComponent<SpriteRenderer>();
                    pieceSprite.sprite = item.Card.card_pieces[j].sprite;
                    pieceSprite.sortingOrder = 3;
                }

                cardIndex++; // 更新卡牌索引
            }
        }
    }*/

}
