using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPanel : Inventory
{
    private static VendorPanel _instance;

    public static VendorPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorPanel").GetComponent<VendorPanel>();
            }
            return _instance;
        }
    }


    private int[] arrVendorId = new[] {1, 2, 3, 4, 5};
    private Test test;

    public override void Start()
    {
        base.Start();
        InitVendor();
        test = GameObject.FindGameObjectWithTag("Player").GetComponent<Test>();
        DisplaySwitch();
    }

    public void InitVendor()
    {
        foreach (var id in arrVendorId)
        {
            StoreItem(id);
        }
    }

    public void BuyItem(Item item)
    {
        if (test.CanConsumeCoin(item.BuyPrice))
        {
            test.ConsumeCoin(item.BuyPrice);
            KnapsackPanel.Instance.StoreItem(item);
        }  
    }

    public void SellItem()
    {
        int sellAmout = 1;
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            sellAmout = InventoryManager.Instance.PickedItemUI.Amount;
        }
        int sellCoin = sellAmout * InventoryManager.Instance.PickedItemUI.Item.SellPrice;
        test.EarnCoin(sellCoin);
        InventoryManager.Instance.PlacedItem(sellAmout);
    }
}
