using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkeletalPath2", menuName = "Incident/IncidentPageData/SkeletalPath/SkeletalPath2")]
public class SkeletalPath2 : IncidentPageData
{
    public Transform cardContainer; // ���ڷ��ÿ�����Ϣ��UI������
    public float temp = 1.0f; // ���ű���

    private Transform cardDisplayContainer; // ר�����ڷ��ÿ��Ƶ�������

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

                // ����һ��ר�����ڷ��ÿ��Ƶ�������
                cardDisplayContainer = new GameObject("CardDisplayContainer").transform;
                cardDisplayContainer.SetParent(cardContainer, false);
            }
            else
            {
                Debug.LogError("δ�ҵ���Ϊ 'Other' ���Ӷ���");
                return;
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Incident' �� Canvas");
            return;
        }

        // ������ʱ����GlobalDeckManager��ʵ��
        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null)
        {
            // ��all_kind_card�������ȡһ�ſ���
            if (deckManager.all_kind_card.Count > 0)
            {
                int randomIndex = Random.Range(0, deckManager.all_kind_card.Count);
                CrackedCardData randomCard = deckManager.all_kind_card[randomIndex];

                // �������ȡ�Ŀ�����ӵ�������
                deckManager.addCard(randomCard);

                Debug.Log("SkeletalPath2 Resolve ����������: ��ȡ��һ�ſ��� " + randomCard.name);

                // ��ʾ���Ƶ�ͼ�������
                DisplayCard(randomCard);
            }
            else
            {
                Debug.Log("SkeletalPath2 Resolve ����������: all_kind_card �б�Ϊ��");
            }
        }
        else
        {
            Debug.Log("SkeletalPath2 Resolve ����������: δ�ҵ� GlobalDeckManager ʵ��");
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
        cardObject.transform.localScale = new Vector3(temp*10, temp*10, 1f);

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
                pieceRectTransform.sizeDelta = new Vector2(60*15, 75*15); // ������Ҫ������С
            }
        }
    }
}