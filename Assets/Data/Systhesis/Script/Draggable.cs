using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        startPosition = transform.position;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        image.raycastTarget = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<DropSlot>() != null)
        {
            DropSlot dropSlot = eventData.pointerEnter.GetComponent<DropSlot>();
            transform.SetParent(dropSlot.transform);
            transform.localPosition = Vector3.zero;
            image.raycastTarget = true;
            return;
        }

        // Return to original position if not dropped on a valid slot
        transform.SetParent(parentAfterDrag);
        transform.position = startPosition;
        
    }
}
