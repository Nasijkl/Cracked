using UnityEngine;
using UnityEngine.UI;

public class DecompositionSystem : MonoBehaviour
{
    public DropSlot cardSlot;
    public DropSlot[] fragmentSlots;
    public GameObject fragmentPrefab;
    public string cardTag = "Card";

    public void Update(){
        DecomposeCard();
    }
    public void DecomposeCard()
    {
        Transform cardTransform = cardSlot.transform.GetChild(0);
        Debug.Log(11212);

        if (cardTransform != null)
        {
            
            GameObject card = cardTransform.gameObject;

            Transform[] cardChildren = new Transform[card.transform.childCount];
            
            for (int i = 0; i < card.transform.childCount; i++)
            {
                cardChildren[i] = card.transform.GetChild(i);
            }

            if (card.CompareTag(cardTag))
            {
                
                // 分解成四个碎片
                for (int i = 0; i < fragmentSlots.Length; i++)
                {
                    GameObject fragment = Instantiate(cardChildren[i].gameObject, fragmentSlots[i].transform);
                    fragment.transform.localPosition = Vector3.zero;
                    Image fragmentImage = fragment.GetComponent<Image>();
                    fragmentImage.raycastTarget = true;
                    fragment.tag = "Piece" + (i+1);
                    
                }

                // 移除原始卡片
                Destroy(card);
            }
        }
    }
}
