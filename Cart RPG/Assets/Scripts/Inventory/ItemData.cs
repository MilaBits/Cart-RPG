using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount = 1;
    public int slot;
    
    public Inventory inventory;
    public Inventory Target;
    private ToolTip tooltip;

    public void Start()
    {
        inventory = transform.parent.parent.parent.GetComponent<Inventory>();
        tooltip = GameObject.Find("Game").GetComponent<ToolTip>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (item != null)
        {
            //Raise item in hierarchy so it remains on top of everything while dragging
            this.transform.SetParent(this.transform.parent.parent.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (item != null) {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(Target.slots[slot].transform);
        this.transform.position = Target.slots[slot].transform.position;
        this.inventory = Target;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Deactivate();
    }
}
