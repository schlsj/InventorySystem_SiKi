using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    protected Slot[] arrSlot;
    private CanvasGroup canvasGroup;
    private float targetAlpha = 1;
    public int HideSpeed = 8;
	// Use this for initialization
    public virtual void Start()
    {
        arrSlot = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
	void Update () {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * HideSpeed);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }	
	}

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    //没有解决要一次存储多个item的情况
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            print("item null,无法存储item,因为id为空");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                print("Capacity=1,无法存储item,因为id为空");
                return false;
            }
            slot.Store(item);
            return true;
        }
        else
        {
            Slot slot = FindTheSlot(item);
            if (slot == null)
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                    print("Capacity!=1无法存储item,因为id为空");
                    return false;
                }
                slot.Store(item);
                return true;
            }
            slot.Store(item);
            return true;
        }
    }

    private Slot FindEmptySlot()
    {
        foreach (Slot slot in arrSlot)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }

    private Slot FindTheSlot(Item item)
    {
        foreach (Slot slot in arrSlot)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.Id && slot.GetItemAmount() < item.Capacity)
            {
                return slot;
            }
        }
        return null;
    }

    public void DisplaySwitch()
    {
        if (targetAlpha == 1)
        {
            targetAlpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            targetAlpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
