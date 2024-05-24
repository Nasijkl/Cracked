using UnityEngine;
using UnityEngine.SceneManagement; // 用于场景切换

public class PlayerTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
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
    }
}