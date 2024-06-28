using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class IncidentManager : MonoBehaviour
{
    public List<IncidentPageData> incidentList;
    public TextMeshProUGUI eventNameText; // ��ʾ�¼����ֵ�TextMeshProUGUI���
    public TextMeshProUGUI eventContentText; // ��ʾ�¼����ݵ�TextMeshProUGUI���
    public GameObject buttonContainer; // ��Ű�ť������
    public GameObject buttonPrefab; // ��ť��Ԥ�Ƽ�
    public Canvas incidentCanvas;

    public void DisplayIncidentBasedOnName(string incidentName)
    {
        // ��������һ�����������������ҵ���Ӧ��IncidentPageData
        string targetEventName = incidentName + "1";
        DisplayIncidentByName(targetEventName);
    }

    // �����¼�������ʾ�¼����ݵķ���
    private void DisplayIncidentByName(string eventName)
    {
        Debug.Log($"������ʾ�¼���Ϊ: {eventName}");
        IncidentPageData incident = null;

        incident = incidentList.Find(temp => temp.name == eventName);
        if (incident != null)
        {
            eventContentText.text = incident.text;
            CreateButtonsForIncident(incident);
        }
  
    }
    // ����IncidentPageData��PageList������ť
    public void CreateButtonsForIncident(IncidentPageData incident)
    {
        // �������buttonContainer�е����оɰ�ť
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ���㰴ť��Yλ�ã���Ļ��1/4�߶ȣ����°���Ļ��
        float buttonY = screenHeight / 4;

        // ����PageList�ĳ��ȴ�����Ӧ�����İ�ť
        for (int i = 0; i < incident.pageList.Count; i++)
        {
            // ʵ������ťԤ�Ƽ�
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer.transform);
            // ���ð�ť�ı�Ϊ��Ӧ��PageTextList���ı�
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = incident.pageTextList[i];
            // ������ʱ����ӵ���¼�������
            // ��ȡRectTransform��������ð�ťλ��
            RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();

            // ���㰴ť��Xλ��
            float buttonX = screenWidth * (i + 1) / (incident.pageList.Count + 1);

            // ���ð�ť��λ��
            // ע�⣺�������buttonContainer��Anchorλ����Ļ���ģ����λ������Ϊ��������ĵ�ƫ��
            rectTransform.anchoredPosition = new Vector2(buttonX - screenWidth / 2, -buttonY);

            // ���ð�ť��Zλ��Ϊ0
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
            // ���ð�ť�����ű���Ϊ0.5��
            rectTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

            Button btn = buttonObj.GetComponent<Button>();
            if (incident.pageList.Count == 1 && incident.pageTextList[i].Equals("Leave", StringComparison.OrdinalIgnoreCase))
            {
                // ���ֻ��һ����ť���ı���"Leave"������ӹر�Canvas�����ص�ͼ���¼�������
                btn.onClick.AddListener(CloseCanvasAndReturnToMap);
            }
            else
            {
                int nextPageId = incident.pageList[i]; // ʹ�õ�ǰ��ť��Ӧ��pageList�е�ֵ��ΪnextPageId
                btn.onClick.AddListener(() => OnOptionSelected(nextPageId));
            }

        }
    }

    private void CloseCanvasAndReturnToMap()
    {
        
     incidentCanvas.enabled = false;

        // ���÷��ص�ͼ�ķ���
    }
    // �����������ѡ����ĳ��ѡ���Ĵ�����
    public void OnOptionSelected(int optionIndex)
    {
        IncidentPageData incident = incidentList.Find(inc => inc.id == optionIndex);
        if (incident != null)
        {
            CreateButtonsForIncident(incident);
            eventContentText.text = incident.text;
            // ִ���¼�Ч��
            incident.Resolve();
        }
    }
}
