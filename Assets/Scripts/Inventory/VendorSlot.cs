using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : KnapsackSlot {
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!InventoryManager.Instance.IsPickedItem && eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                transform.parent.parent.SendMessage("BuyItem", item);
            }
        }else if (InventoryManager.Instance.IsPickedItem && eventData.button == PointerEventData.InputButton.Left)
        {
            transform.parent.parent.SendMessage("SellItem");
        }
    }
}
