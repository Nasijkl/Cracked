using UnityEngine;
using UnityEngine.EventSystems;

public class Potion : MonoBehaviour, IPointerClickHandler
{
    private int potionID;
    private System.Action<int> usePotionCallback;

    public void Init(int id, System.Action<int> callback)
    {
        potionID = id;
        usePotionCallback = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        usePotionCallback?.Invoke(potionID);
    }
}
