using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 用于场景切换
using TMPro;

public class PlayerTeleport : MonoBehaviour
{
    public GameObject dialogPrefab; // 对话框的Prefab
    public GameObject incidentCanvasPrefab; // Incident的Canvas Prefab
    public string targetScene;
    private Canvas dialogCanvas;
    private Canvas incidentCanvas; // Incident的Canvas组件
    private MaTile selfMaTile;
    private PlayerMovement playerMovement;
    public TextMeshProUGUI incidentNameText;
    public InfiniteMapGenerator map;
    public PlayerHealth playerhp;

    private void Start()
    {
        //originalPosition = transform.position;
        // 获取对话框的Canvas组件
        dialogCanvas = dialogPrefab.GetComponent<Canvas>();
        // 禁用对话框的Canvas
        dialogCanvas.enabled = false;// 获取PlayerMovement脚本
        incidentCanvas = incidentCanvasPrefab.GetComponent<Canvas>();
        // 禁用Incident的Canvas
        incidentCanvas.enabled = false;
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapTile"))
        {
            MaTile mapTile = other.GetComponent<MaTile>();
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            if (mapTile != null)
            {
                targetScene = mapTile.targetScene;

                // 启用对话框的Canvas
                dialogCanvas.enabled = true;
                selfMaTile = mapTile;
            }

        }
        if (other.CompareTag("Incident"))
        {
            incidentCanvas.enabled = true;
            if (incidentNameText != null)
            {
                // 直接获取other对象的名字

                //incidentNameText.text = other.gameObject.name.Replace("(Clone)", "");
                string incidentName = other.gameObject.name.Replace("(Clone)", "");
                incidentNameText.text = incidentName;

                IncidentManager incidentManager = incidentCanvas.GetComponent<IncidentManager>();
                if (incidentManager != null)
                {
                    incidentManager.DisplayIncidentBasedOnName(incidentName);
                }
                //Destroy(other.gameObject);
            }
            // 处理Incident标签的逻辑
            // 例如，您可以在这里显示不同的对话框，或者执行其他特定于Incident的操作
            //Debug.Log("Incident发生了！");
            // 这里添加您的逻辑
        }
    }

    public void OnYesButtonClick()
    {
        // 切换场景
        Debug.Log("Loading scene: " + targetScene);



        SceneManager.LoadScene(targetScene);

    }

    public void OnNoButtonClick()
    {
        dialogCanvas.enabled = false;
        Destroy(selfMaTile.gameObject);
    }
}