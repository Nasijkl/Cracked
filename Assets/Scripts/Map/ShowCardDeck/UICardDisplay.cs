using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UICardDisplay : MonoBehaviour
{
    public List<CrackedCardData> card_deck = new List<CrackedCardData>();// ��Ӷ�CardBank������
    public Transform cardContainer; // �������������ã�Ӧָ��ScrollView��Content����
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
        int cardIndex = 0; // ����׷�ٵ�ǰ���Ƶ�����
        foreach (var card in card_deck)
        {
            // Ϊÿ�ſ��ƴ���һ����������ʹ�ù��������
            string cardName = $"{card.name}{cardIndex + 1}";
            GameObject cardObject = new GameObject(cardName);

            // ���ø�����ΪcardContainer
            cardObject.transform.SetParent(cardContainer, false);
            cardObject.transform.localScale = new Vector3(temp, temp, 1f);

            // ʹ��RectTransform������λ��
            RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1); // ����ԭʼUI����

            // ���㿨�Ƶ�λ��
            int row = cardIndex / 4; // ���㵱ǰ�������ڵ���
            int column = cardIndex % 4; // ���㵱ǰ�������ڵ���
            float xPosition = -160 + column * (2 + rectTransform.sizeDelta.x); // ����xλ��
            float yPosition = 380 - row * (2 + rectTransform.sizeDelta.y); // ����yλ��
            rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

            for (int j = 0; j < card.card_pieces.Length; j++)
            {
                // Ϊÿ��CardPieceData����һ��Image���
                GameObject pieceObject = new GameObject($"Piece_{j}");
                pieceObject.transform.SetParent(cardObject.transform, false);
                Image pieceImage = pieceObject.AddComponent<Image>();
                pieceImage.sprite = card.card_pieces[j].sprite;

                // ����RectTransform����Ӧ������
                RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                pieceRectTransform.anchoredPosition = Vector2.zero;
                pieceRectTransform.sizeDelta = new Vector2(60, 75); // ������Ҫ������С
            }

            cardIndex++; // ���¿�������
        }
    }
    /*void DisplayCards()
    {
        int cardIndex = 0; // ����׷�ٵ�ǰ���Ƶ�����
        foreach (var item in cardBank.Items)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                // Ϊÿ�ſ��ƴ���һ����������ʹ�ù��������
                string cardName = $"{item.Card.name}{i + 1}";
                GameObject cardObject = new GameObject(cardName);

                // ���ø�����ΪcardContainer
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(temp, temp, 1f);

                // ʹ��RectTransform������λ��
                RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1, 1, 1); // ����ԭʼUI����

                // ���㿨�Ƶ�λ��
                int row = cardIndex / 4; // ���㵱ǰ�������ڵ���
                int column = cardIndex % 4; // ���㵱ǰ�������ڵ���
                float xPosition =-160+column * (2 + rectTransform.sizeDelta.x); // ����xλ��
                float yPosition =380- row * (2 + rectTransform.sizeDelta.y); // ����yλ��
                rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // Ϊÿ��CardPieceData����һ��Image���
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    Image pieceImage = pieceObject.AddComponent<Image>();
                    pieceImage.sprite = item.Card.card_pieces[j].sprite;

                    // ����RectTransform����Ӧ������
                    RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                    pieceRectTransform.anchoredPosition = Vector2.zero;
                    pieceRectTransform.sizeDelta = new Vector2(60, 75); // ������Ҫ������С
                }

                cardIndex++; // ���¿�������
            }
        }
    }
    /*
    void DisplayCards()
    {
        int cardIndex = 0; // ����׷�ٵ�ǰ���Ƶ�����
        int uiLayer = LayerMask.NameToLayer("UI");
        foreach (var item in cardBank.Items)
        {
            for (int i = 0; i < item.Amount; i++)
            {
                // Ϊÿ�ſ��ƴ���һ����������ʹ�ù��������
                string cardName = $"{item.Card.name}{i + 1}";
                GameObject cardObject = new GameObject(cardName);

                // ���ø�����ΪcardContainer�����ﲻ��Ҫ����localScale����ΪGridLayoutGroup����ƴ�С
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(temp, temp,1f);
                // ����GameObject�Ĳ㼶ΪUI
                cardObject.layer = uiLayer;

                // ���㿨�Ƶ�λ��
                int row = cardIndex / 4; // ���㵱ǰ�������ڵ���
                int column = cardIndex % 4; // ���㵱ǰ�������ڵ���

                float xPosition = 50 + column * (80 + cardObject.transform.localScale.x); // ����xλ��
                float yPosition = -50 - row * (70 + cardObject.transform.localScale.y); // ����yλ��

                cardObject.transform.localPosition = new Vector3(xPosition, yPosition, -10f);

                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // Ϊÿ��CardPieceData����һ��SpriteRenderer
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    SpriteRenderer pieceSprite = pieceObject.AddComponent<SpriteRenderer>();
                    pieceSprite.sprite = item.Card.card_pieces[j].sprite;
                    pieceSprite.sortingOrder = 3;
                }

                cardIndex++; // ���¿�������
            }
        }
    }*/

}
