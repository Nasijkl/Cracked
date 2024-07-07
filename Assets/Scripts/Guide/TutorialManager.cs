using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance;

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
        ShowStep(currentStep);
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
    }
}
