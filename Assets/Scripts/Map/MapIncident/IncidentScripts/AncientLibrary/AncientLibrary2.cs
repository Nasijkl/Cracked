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
                        eventContentText.text = "You find some consumption. (Discover a card)";
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
                            // ��ȡPlayerHealth���
                            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                            if (playerHealth != null)
                            {

                                playerHealth.currentHealth = playerHealth.maxHealth;
                            }
                            else
                            {
                                Debug.LogError("Player������δ�ҵ� PlayerHealth ���");
                            }

                            InfiniteMapGenerator mapGenerator = FindObjectOfType<InfiniteMapGenerator>();
                            if (mapGenerator != null)
                            {

                                mapGenerator.stamina = 100f; // �����������Ϊ100
                            }
                            else
                            {
                                Debug.LogError("δ�ҵ� InfiniteMapGenerator ���");
                            }
                            DisplayRecoveryInfo(playerHealth.currentHealth,mapGenerator.stamina);
                        }
                        else
                        {
                            Debug.LogError("δ�ҵ���Ϊ 'Player' �Ķ���");
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
                pieceRectTransform.sizeDelta = new Vector2(10 * 15, 15 * 15); // ������Ҫ������С
            }
        }
    }
    private void DisplayRecoveryInfo(float currentHealth,float currentStamina)
    {
        // ��ȡIncident Canvas�µ�Other�Ӷ���
        Transform otherContainer = GameObject.Find("Incident").transform.Find("Other");
        if (otherContainer != null)
        {
            foreach (Transform child in otherContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
            // ������������ʾ����ֵ��TextMeshPro����
            GameObject healthTextObject = new GameObject("HealthText");
            healthTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI healthText = healthTextObject.AddComponent<TextMeshProUGUI>();
            healthText.text = $"Now HP: {currentHealth}";
            healthText.color = Color.red;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

            // ������������ʾ����ֵ��TextMeshPro����
            GameObject staminaTextObject = new GameObject("StaminaText");
            staminaTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI staminaText = staminaTextObject.AddComponent<TextMeshProUGUI>();
            staminaText.text = $"Now Stamina: {currentStamina}";
            staminaText.color = Color.blue;
            staminaText.fontSize = 50;
            staminaText.fontStyle = FontStyles.Bold;
            staminaText.alignment = TextAlignmentOptions.Center;

            // ����Text��Ĵ�С��λ��
            RectTransform healthRectTransform = healthText.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta = new Vector2(1000, 100);
            healthRectTransform.anchoredPosition = new Vector2(0, 100);

            RectTransform staminaRectTransform = staminaText.GetComponent<RectTransform>();
            staminaRectTransform.sizeDelta = new Vector2(2000, 100);
            staminaRectTransform.anchoredPosition = new Vector2(0, -100);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Other' ���Ӷ���");
        }
    }
}
