using UnityEngine;
using UnityEngine.SceneManagement; // ���ڳ����л�

public class PlayerTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapTile"))
        {
            // ��ȡ�ؿ��ϵĹؿ���Ϣ
            MaTile mapTile = other.GetComponent<MaTile>();
            if (mapTile != null)
            {
                string targetScene = mapTile.targetScene;
                // �л���Ŀ��ؿ�
                SceneManager.LoadScene(targetScene);
            }
        }
    }
}