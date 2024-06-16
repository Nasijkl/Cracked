using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI explorationText;
    public TextMeshProUGUI healthText;

    public Slider staminaSlider;
    public Slider explorationSlider;
    public Slider healthSlider;

    private PlayerHealth playerHealth;
    private InfiniteMapGenerator mapGenerator;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        mapGenerator = FindObjectOfType<InfiniteMapGenerator>();

        if (playerHealth == null || mapGenerator == null)
        {
            Debug.LogError("PlayerHealth or InfiniteMapGenerator component is missing in the scene.");
        }
    }

    void Update()
    {
        if (playerHealth != null && mapGenerator != null)
        {
            staminaText.text = "Stamina: " + mapGenerator.stamina.ToString("F1");
            explorationText.text = "Exploration: " + mapGenerator.explorationValue.ToString("F1");
            healthText.text = "Health: " + playerHealth.currentHealth.ToString("F1");

            staminaSlider.value = mapGenerator.stamina;
            explorationSlider.value = mapGenerator.explorationValue;
            healthSlider.value = playerHealth.currentHealth;
        }
    }
}
