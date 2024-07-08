using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "OutOfBody3", menuName = "Incident/IncidentPageData/OutOfBody/OutOfBody3")]
public class OutOfBody3 : IncidentPageData
{
    public TextMeshProUGUI eventContentText; // ��ʾ�¼����ݵ�TextMeshProUGUI���
    public Transform cardContainer; // ���ڷ��ÿ�����Ϣ��UI������
    public float temp = 1.0f; // ���ű���
    public override void Resolve()
    {
        GameObject incidentCanvas = GameObject.Find("Incident");
        if (incidentCanvas != null)
        {
            Transform otherContainer = incidentCanvas.transform.Find("Other");
            if (otherContainer != null)
            {
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

                    playerHealth.currentHealth -= 20;
                }
                else
                {
                    Debug.LogError("Player������δ�ҵ� PlayerHealth ���");
                }
                DisplayRecoveryInfo(playerHealth.currentHealth);
            }
        }
    }
    private void DisplayRecoveryInfo(float currentHealth)
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
            healthText.text = $"Lose 20 HP. Now HP: {currentHealth}";
            healthText.color = Color.red;
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