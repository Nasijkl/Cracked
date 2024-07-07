using UnityEngine;
using UnityEngine.UI;

public class AssemblySystem : MonoBehaviour
{
    public DropSlot[] assemblySlots;
    public DropSlot resultSlot;
    public GameObject cardPrefab;

    public void Start()
    {
        
    }
    public void Update()
    {
        AssembleCard();
    }
    public void AssembleCard()
    {
        foreach (var slot in assemblySlots)
        {
            Transform fragmentTransform = slot.transform.GetChild(0);
            if (fragmentTransform == null)
            {
                Debug.Log("需要四个碎片来拼装卡片！");
                return;
            }
        }

        // 拼装卡片
        GameObject newCard = Instantiate(cardPrefab, resultSlot.transform);

        foreach (var slot in assemblySlots)
        {
            Transform fragmentTransform = slot.transform.GetChild(0);
            GameObject fragment = Instantiate(fragmentTransform.gameObject, newCard.transform);
            fragment.transform.localPosition = Vector3.zero;
            Image fragmentImage = fragment.GetComponent<Image>();
            fragmentImage.raycastTarget = false;
        }
        newCard.transform.localPosition = Vector3.zero;
        newCard.tag = "Card"; // 设置新卡片的Tag

        // 移除原始碎片
        foreach (var slot in assemblySlots)
        {
            Destroy(slot.transform.GetChild(0).gameObject);
        }
    }
}
