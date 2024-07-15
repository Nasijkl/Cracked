using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;
    private const string TutorialCompletedKey = "TutorialCompleted"; // 新增：用于标记新手引导是否完成的键

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
        // 修改：在开始时检查新手引导是否已完成
        if (PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 0)
        {
            ShowStep(currentStep); // 只有当新手引导未完成时才显示
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
        // 结束新手引导逻辑
        PlayerPrefs.SetInt(TutorialCompletedKey, 1); // 新增：标记新手引导为已完成
        PlayerPrefs.Save(); // 确保更改被保存
    }
}
