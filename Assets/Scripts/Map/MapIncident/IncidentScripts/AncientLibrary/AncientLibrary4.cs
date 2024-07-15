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
                            // ��ȡGame�ű����
                            Game gameScript = coinObject.GetComponent<Game>();
                            if (gameScript != null)
                            {
                                gameScript.Coins += 100;
                                // ��ȡCoins��ֵ����ʾ
                                int coins = gameScript.Coins;
                                DisplayRecoveryInfo(coins.ToString());
                            }
                            else
                            {
                                Debug.LogError("��'coin'������δ�ҵ�Game�ű����");
                            }
                        }
                        else
                        {
                            Debug.LogError("δ�ҵ���Ϊ'coin'��GameObject");
                        }
                    }
                    else
                    {
                        eventContentText.text = "It seems not suitable for discovering today. (Nothing happens.)";
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
    private void DisplayRecoveryInfo(string text)
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
            GameObject coinTextObject = new GameObject("CoinNumText");
            coinTextObject.transform.SetParent(otherContainer);
            TextMeshProUGUI healthText = coinTextObject.AddComponent<TextMeshProUGUI>();
            healthText.text = $"Gain 100 gold coins. Now Coins Num: {text}";
            healthText.color = Color.yellow;
            healthText.fontSize = 50;
            healthText.fontStyle = FontStyles.Bold;
            healthText.alignment = TextAlignmentOptions.Center;

         

            // ����Text��Ĵ�С��λ��
            RectTransform healthRectTransform = healthText.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta = new Vector2(1000, 100);
            healthRectTransform.anchoredPosition = new Vector2(0, 100);
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Other' ���Ӷ���");
        }
    }
}

