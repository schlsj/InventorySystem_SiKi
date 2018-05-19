using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot {

    public EquipmentType EquipmentType;
    public WeaponType WeaponType;

    //这个方法右键也会被调用！！
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!InventoryManager.Instance.IsPickedItem&&eventData.button == PointerEventData.InputButton.Right)
        {
            Item tempItem = itemUI.Item;
            DestroyImmediate(itemUI.gameObject);
            CharacterPanel.Instance.UpdateInfo();
            Knapsack.Instance.StoreItem(tempItem);
            InventoryManager.Instance.HideToolTip();
        }
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        bool needUpdateCharaterInfo = false;
        if (InventoryManager.Instance.IsPickedItem)
        {
            if (CanShipItem(InventoryManager.Instance.PickedItemUI.Item))
            {
                if (transform.childCount == 0)
                {
                    Store(InventoryManager.Instance.PickedItemUI.Item);
                    InventoryManager.Instance.PlacedItem();
                    needUpdateCharaterInfo = true;
                }
                else
                {
                    InventoryManager.Instance.PickedItemUI.Exchange(itemUI);
                    needUpdateCharaterInfo = true;
                }
            }
        }
        else
        {
            InventoryManager.Instance.PickedItem(itemUI.Item, itemUI.Amount);
            Destroy(itemUI.gameObject);
            needUpdateCharaterInfo = true;
        }
        if (needUpdateCharaterInfo)
        {
            CharacterPanel.Instance.UpdateInfo();
        }
    }

    public bool CanShipItem(Item item)
    {
        if (item is Equipment && ((Equipment)item).EquipmentType == EquipmentType)
        {
            return true;
        }
        if (item is Weapon && ((Weapon)item).WeaponType == WeaponType)
        {
            return true;
        }
        return false;
    }
}
