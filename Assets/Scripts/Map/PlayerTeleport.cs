using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // ���ڳ����л�


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
        // ��ȡ�Ի����Canvas���
        dialogCanvas = dialogPrefab.GetComponent<Canvas>();
        // ���öԻ����Canvas
        dialogCanvas.enabled = false;// ��ȡPlayerMovement�ű�
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

                // ���öԻ����Canvas
                dialogCanvas.enabled = true;
                selfMaTile = mapTile;
            }

        }
    }

    public void OnYesButtonClick()
    {
        // �л�����
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
// ��ȡ�ؿ��ϵĹؿ���Ϣ
MaTile mapTile = other.GetComponent<MaTile>();
if (mapTile != null)
{
string targetScene = mapTile.targetScene;
// �л���Ŀ��ؿ�
SceneManager.LoadScene(targetScene);
}
}
}*/