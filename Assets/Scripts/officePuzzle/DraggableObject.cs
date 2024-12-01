using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; // Reference to the UI object's RectTransform
    private Canvas canvas; // Reference to the parent canvas
    private CanvasGroup canvasGroup; // To manage raycast blocking during dragging

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Allow dragging over other objects
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the object relative to the canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable raycasting
    }
}
