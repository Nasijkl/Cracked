using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 用于场景切换


public class PlayerTeleport : MonoBehaviour
{
    public GameObject dialogPrefab;
    public string targetScene;
    private Canvas dialogCanvas;
    private MaTile selfMaTile;
    //private Vector3 originalPosition;
    private PlayerMovement playerMovement;

    private void Start()
    {
        //originalPosition = transform.position;
        // 获取对话框的Canvas组件
        dialogCanvas = dialogPrefab.GetComponent<Canvas>();
        // 禁用对话框的Canvas
        dialogCanvas.enabled = false;// 获取PlayerMovement脚本
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
/*private void OnTriggerEnter2D(Collider2D other)
{
if (other.CompareTag("MapTile"))
{
// 获取地块上的关卡信息
MaTile mapTile = other.GetComponent<MaTile>();
if (mapTile != null)
{
string targetScene = mapTile.targetScene;
// 切换到目标关卡
SceneManager.LoadScene(targetScene);
}
}
}*/