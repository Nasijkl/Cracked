using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    private const string TutorialCompletedKey = "TutorialCompleted"; // ���������ڱ�����������Ƿ���ɵļ�

    public static TutorialManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TutorialManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("TutorialManager");
                    _instance = go.AddComponent<TutorialManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public GameObject[] steps;
    private int currentStep = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �޸ģ��ڿ�ʼʱ������������Ƿ������
        if (PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 0)
        {
            ShowStep(currentStep); // ֻ�е���������δ���ʱ����ʾ
        }
    }

    void ShowStep(int stepIndex)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i].SetActive(i == stepIndex);
        }
    }

    public void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            currentStep++;
            ShowStep(currentStep);
        }
        else
        {
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        // �������������߼�
        PlayerPrefs.SetInt(TutorialCompletedKey, 1); // �����������������Ϊ�����
        PlayerPrefs.Save(); // ȷ�����ı�����
    }
}
