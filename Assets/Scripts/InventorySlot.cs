using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button removeBtn;
    private Item item;
    private Text text;
    private bool hovering;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void UpdateSlot (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeBtn.interactable = true;
    }

    public void ClearSlot ()
    {
        if (item != null)
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            removeBtn.interactable = false;
            if (text != null)
                text.text = "";
        }
    }

    public void RemoveClicked()
    {
        Inventory.Instance.Remove(item);
    }

    public void ItemClicked()
    {
        if (item != null)
        {
            item.Effect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        //StartCoroutine(Tooltip(2f));
        if (item != null)
        {
            text.text = item.description;
            text.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        text.enabled = false;
    }

    private void OnDisable()
    {
        text.enabled = false;
    }

    private IEnumerator Tooltip(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (item != null && hovering)
        {
            text.text = item.description;
            text.enabled = true;
        }
    }
}
