using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Vivid_Idol2", menuName = "Incident/IncidentPageData/Vivid_Idol/Vivid_Idol2")]
public class Vivid_Idol2 : IncidentPageData
{
    public Transform cardContainer; // ���ڷ��ÿ�����Ϣ��UI������
    public float temp = 1.0f; // ���ű���

    private Transform cardDisplayContainer; // ר�����ڷ��ÿ��Ƶ�������
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

            // ����һ��ר�����ڷ��ÿ��Ƶ�������
            cardDisplayContainer = new GameObject("CardDisplayContainer").transform;
            cardDisplayContainer.SetParent(cardContainer, false);
        }
        // ������ʱ����GlobalDeckManager��ʵ��
        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null)
        {
            // ��all_kind_card�в�����Ϊ"SoulSteal"�Ŀ���
            CrackedCardData soulStealCard = deckManager.all_kind_card.Find(card => card.name == "SoulSteal");
            if (soulStealCard != null)
            {
                // ����ҵ�����Ϊ"SoulSteal"�Ŀ��ƣ�������ӵ�������
                //deckManager.card_deck.Add(soulStealCard);
                deckManager.addCard(soulStealCard);
                DisplayCard(soulStealCard);
            }
        }
    }
    private void DisplayCard(CrackedCardData card)
    {
        // ���cardDisplayContainer�е������Ӷ���
        foreach (Transform child in cardDisplayContainer)
        {
            Destroy(child.gameObject);
        }

        // Ϊ���ƴ���һ����������ʹ�ù��������
        string cardName = $"{card.name}";
        GameObject cardObject = new GameObject(cardName);

        // ���ø�����ΪcardDisplayContainer
        cardObject.transform.SetParent(cardDisplayContainer, false);
        cardObject.transform.localScale = new Vector3(temp * 10, temp * 10, 1f);

        // ʹ��RectTransform������λ��
        RectTransform rectTransform = cardObject.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1, 1, 1); // ����ԭʼUI����

        for (int j = 0; j < 4; j++)
        {
            // Ϊÿ��CardPieceData����һ��Image���
            GameObject pieceObject = new GameObject($"Piece_{j}");
            pieceObject.transform.SetParent(cardObject.transform, false);
            if (card.card_pieces[j] != null)
            {
                Image pieceImage = pieceObject.AddComponent<Image>();
                pieceImage.sprite = card.card_pieces[j].sprite;
                // ����RectTransform����Ӧ������
                RectTransform pieceRectTransform = pieceObject.GetComponent<RectTransform>();
                pieceRectTransform.anchoredPosition = Vector2.zero;
                pieceRectTransform.sizeDelta = new Vector2(10 * 15, 15 * 15); // ������Ҫ������С
            }
        }
    }
}



