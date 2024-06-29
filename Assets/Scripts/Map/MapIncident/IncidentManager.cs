using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class IncidentManager : MonoBehaviour
{
    public List<IncidentPageData> incidentList;
    public TextMeshProUGUI eventNameText; // 显示事件名字的TextMeshProUGUI组件
    public TextMeshProUGUI eventContentText; // 显示事件内容的TextMeshProUGUI组件
    public GameObject buttonContainer; // 存放按钮的容器
    public GameObject buttonPrefab; // 按钮的预制件
    public Canvas incidentCanvas;

    public void DisplayIncidentBasedOnName(string incidentName)
    {
        // 假设你有一个方法来根据名字找到对应的IncidentPageData
        string targetEventName = incidentName + "1";
        DisplayIncidentByName(targetEventName);
    }

    // 根据事件名字显示事件内容的方法
    private void DisplayIncidentByName(string eventName)
    {
        Debug.Log($"尝试显示事件名为: {eventName}");
        IncidentPageData incident = null;

        incident = incidentList.Find(temp => temp.name == eventName);
        if (incident != null)
        {
            eventContentText.text = incident.text;
            CreateButtonsForIncident(incident);
        }
  
    }
    // 根据IncidentPageData的PageList创建按钮
    public void CreateButtonsForIncident(IncidentPageData incident)
    {
        // 首先清除buttonContainer中的所有旧按钮
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 计算按钮的Y位置（屏幕的1/4高度，在下半屏幕）
        float buttonY = screenHeight / 4;

        // 根据PageList的长度创建对应数量的按钮
        for (int i = 0; i < incident.pageList.Count; i++)
        {
            // 实例化按钮预制件
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer.transform);
            // 设置按钮文本为对应的PageTextList的文本
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = incident.pageTextList[i];
            // 这里暂时不添加点击事件监听器
            // 获取RectTransform组件以设置按钮位置
            RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();

            // 计算按钮的X位置
            float buttonX = screenWidth * (i + 1) / (incident.pageList.Count + 1);

            // 设置按钮的位置
            // 注意：这里假设buttonContainer的Anchor位于屏幕中心，因此位置设置为相对于中心的偏移
            rectTransform.anchoredPosition = new Vector2(buttonX - screenWidth / 2, -buttonY);

            // 设置按钮的Z位置为0
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
            // 设置按钮的缩放比例为0.5倍
            rectTransform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

            Button btn = buttonObj.GetComponent<Button>();
            if (incident.pageList.Count == 1 && incident.pageTextList[i].Equals("Leave", StringComparison.OrdinalIgnoreCase))
            {
                // 如果只有一个按钮且文本是"Leave"，则添加关闭Canvas并返回地图的事件监听器
                btn.onClick.AddListener(CloseCanvasAndReturnToMap);
            }
            else
            {
                int nextPageId = incident.pageList[i]; // 使用当前按钮对应的pageList中的值作为nextPageId
                btn.onClick.AddListener(() => OnOptionSelected(nextPageId));
            }

        }
    }

    private void CloseCanvasAndReturnToMap()
    {
        
     incidentCanvas.enabled = false;

        // 调用返回地图的方法
    }
    // 假设这是玩家选择了某个选项后的处理方法
    public void OnOptionSelected(int optionIndex)
    {
        IncidentPageData incident = incidentList.Find(inc => inc.id == optionIndex);
        if (incident != null)
        {
            CreateButtonsForIncident(incident);
            eventContentText.text = incident.text;
            // 执行事件效果
            incident.Resolve();
        }
    }
}
