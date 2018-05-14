using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Slot[] arrSlot;
	// Use this for initialization
	public virtual void Start ()
	{
	    arrSlot = GetComponentsInChildren<Slot>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
