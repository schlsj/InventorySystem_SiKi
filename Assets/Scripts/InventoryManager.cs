using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }

    private List<Item> listItem;

    void Start()
    {
        ParseItemsFromJson();
    }

    void ParseItemsFromJson()
    {
        listItem = new List<Item>();
        TextAsset textAsset = Resources.Load<TextAsset>("Items");
        string strJson = textAsset.text;
        JSONObject rootJson = new JSONObject(strJson);
        foreach (JSONObject sonJson in rootJson.list)
        {
            int id = (int) sonJson["Id"].n;
            string name = sonJson["Name"].str;
            ItemType itemType = (ItemType) Enum.Parse(typeof(ItemType), sonJson["ItemType"].str);
            ItemQuality quality = (ItemQuality) Enum.Parse(typeof(ItemQuality), sonJson["Quality"].str);
            string description = sonJson["Description"].str;
            int capacity = (int) sonJson["Capacity"].n;
            int buyPrice = (int) sonJson["BuyPrice"].n;
            int sellPrice = (int) sonJson["SellPrice"].n;
            string sprite = sonJson["Sprite"].str;
            Item item = null;
            switch (itemType)
            {
                case ItemType.Consumable:
                    int hp = (int) sonJson["HP"].n;
                    int mp = (int) sonJson["MP"].n;
                    item = new Consumable(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,
                        sprite, hp, mp);
                    break;
            }
            listItem.Add(item);
        }
    }

    public Item GetItemById(int id)
    {
        foreach (var item in listItem)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return null;
    }
}
