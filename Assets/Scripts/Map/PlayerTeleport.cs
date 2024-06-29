using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ���ڳ����л�
using TMPro;

public class PlayerTeleport : MonoBehaviour
{
    public GameObject dialogPrefab; // �Ի����Prefab
    public GameObject incidentCanvasPrefab; // Incident��Canvas Prefab
    public string targetScene;
    private Canvas dialogCanvas;
    private Canvas incidentCanvas; // Incident��Canvas���
    private MaTile selfMaTile;
    private PlayerMovement playerMovement;
    public TextMeshProUGUI incidentNameText;
    public InfiniteMapGenerator map;
    public PlayerHealth playerhp;

    private void Start()
    {
        //originalPosition = transform.position;
        // ��ȡ�Ի����Canvas���
        dialogCanvas = dialogPrefab.GetComponent<Canvas>();
        // ���öԻ����Canvas
        dialogCanvas.enabled = false;// ��ȡPlayerMovement�ű�
        incidentCanvas = incidentCanvasPrefab.GetComponent<Canvas>();
        // ����Incident��Canvas
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

                // ���öԻ����Canvas
                dialogCanvas.enabled = true;
                selfMaTile = mapTile;
            }

        }
        if (other.CompareTag("Incident"))
        {
            incidentCanvas.enabled = true;
            if (incidentNameText != null)
            {
                // ֱ�ӻ�ȡother���������

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
            // ����Incident��ǩ���߼�
            // ���磬��������������ʾ��ͬ�ĶԻ��򣬻���ִ�������ض���Incident�Ĳ���
            //Debug.Log("Incident�����ˣ�");
            // ������������߼�
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