using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public Image[] potionSlots; // Drag and drop the potion slot images in the inspector
    public Sprite[] potionSprites; // Drag and drop the potion sprites in the inspector
    private int[] potionIndices; // To keep track of which potion is in each slot

    void Start()
    {
        potionIndices = new int[potionSlots.Length];

        for (int i = 0; i < potionSlots.Length; i++)
        {
            int index = i;
            Button button = potionSlots[i].GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => UsePotion(index));
            }
            else
            {
                Debug.LogError("No Button component found on potion slot " + i);
            }
        }
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        // 将编号为 1 的药剂添加到第 0 个槽位中
        inventoryManager.AddPotionToSlot(0, 0);
        inventoryManager.AddPotionToSlot(1, 1);
        inventoryManager.AddPotionToSlot(2, 2);
    }

    // This method will be called to add a potion to a specific slot
    public void AddPotionToSlot(int slotIndex, int potionIndex)
    {
        if (slotIndex < 0 || slotIndex >= potionSlots.Length)
        {
            Debug.LogError("Invalid slot index.");
            return;
        }

        if (potionIndex < 0 || potionIndex >= potionSprites.Length)
        {
            Debug.LogError("Invalid potion index.");
            return;
        }

        potionSlots[slotIndex].sprite = potionSprites[potionIndex];
        potionIndices[slotIndex] = potionIndex; // Store the potion index in the slot
    }

    // This method will be called when a potion slot is clicked
    private void UsePotion(int slotIndex)
    {
        if (potionIndices[slotIndex] == -1)
        {
            Debug.Log("No potion in slot " + slotIndex);
            return;
        }

        int potionIndex = potionIndices[slotIndex];
        // Trigger the effect of the potion based on its index
        TriggerPotionEffect(potionIndex);

        // Clear the potion from the slot
        potionSlots[slotIndex].sprite = null;
        potionIndices[slotIndex] = -1;
    }

    // This method will trigger the effect of the potion
    private void TriggerPotionEffect(int potionIndex)
    {
        // Implement the potion effects here
        // For example:
        switch (potionIndex)
        {
            case 0:
                Debug.Log("Healing potion used.");
                SceneManager.LoadScene("Separate");
                //LoadScene("YourSceneName");
                break;
            case 1:
                Debug.Log("Mana potion used.");
                SceneManager.LoadScene("Synthesis");
                // Restore mana
                break;
            // Add more cases for different potion effects
            default:
                Debug.Log("Unknown potion effect.");
                break;
        }
    }
}
