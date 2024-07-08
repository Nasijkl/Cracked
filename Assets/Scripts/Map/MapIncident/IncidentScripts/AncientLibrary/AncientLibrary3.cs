using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AncientLibrary3", menuName = "Incident/IncidentPageData/AncientLibrary/AncientLibrary3")]

public class AncientLibrary3 : IncidentPageData
{
    public TextMeshProUGUI eventContentText; // ��ʾ�¼����ݵ�TextMeshProUGUI���
    public Transform cardContainer; // ���ڷ��ÿ�����Ϣ��UI������
    public float temp = 1.0f; // ���ű���

    private Transform cardDisplayContainer; // ר�����ڷ��ÿ��Ƶ�������
    public override void Resolve()
    {
        // ����Incident Canvas�µ�EventDescription��TMP UI���
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
                    // �������������text������
                    if (randomValue > 0.5f)
                    {
                        eventContentText.text = "You find a yellowing card. (Discover a card)";
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

                                Debug.Log("��ȡ��һ�ſ��� " + randomCard.name);

                                // ��ʾ���Ƶ�ͼ�������
                                DisplayCard(randomCard);
                            }
                            else
                            {
                                Debug.Log("all_kind_card �б�Ϊ��");
                            }
                        }
                    }
                    else
                    {
                        eventContentText.text = "You find a magical book.(Copy a card in your deck)";
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
                        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
                        if (deckManager != null)
                        {
                            // ��card_deck�������ȡһ�ſ��Ʋ�����
                            if (deckManager.card_deck.Count > 0)
                            {
                                int randomIndex = Random.Range(0, deckManager.card_deck.Count);
                                CrackedCardData randomCard = deckManager.card_deck[randomIndex];

                                // ���ƿ��Ʋ���ӵ�������
                                CrackedCardData copiedCard = randomCard.deepCopy();
                                copiedCard.name = randomCard.name;
                                deckManager.addCard(copiedCard);

                                Debug.Log("������һ�ſ��� " + copiedCard.name);

                                // ��ʾ���Ƶ�ͼ�������
                                DisplayCard(copiedCard);
                            }
                            else
                            {
                                Debug.Log("card_deck �б�Ϊ��");
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("EventDescription������δ�ҵ�TextMeshProUGUI���");
                }
            }
            else
            {
                Debug.LogError("δ�ҵ���Ϊ'EventDescription'���Ӷ���");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ'Incident'��Canvas");
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
                pieceRectTransform.sizeDelta = new Vector2(60 * 15, 75 * 15); // ������Ҫ������С
            }
        }
    }
}

