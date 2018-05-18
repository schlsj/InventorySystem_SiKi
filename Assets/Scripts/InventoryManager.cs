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
    private ToolTip toolTip;
    private bool isToolTipShow = false;
    private Canvas canvas;
    private Vector2 toolTipOffset = new Vector2(7, -6);

    #region  pickedItem

    private bool isPickedItem = false;

    public bool IsPickedItem
    {
        get { return isPickedItem;}
        //set { isPickedItem = value; }
    }

    private ItemUI pickedItemUI;

    public ItemUI PickedItemUI
    {
        get { return pickedItemUI;}   
    }
    #endregion

    void Start()
    {
        ParseItemsFromJson();
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItemUI = GameObject.Find("PickedItem").GetComponent<ItemUI>();
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
            string description = sonJson["Description"].str.Replace("\\n", "\n");
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
                case ItemType.Equipment:
                    int strength = (int) sonJson["Strength"].n;
                    int intelligent = (int)sonJson["Intellect"].n;
                    int agility = (int)sonJson["Agility"].n;
                    int stamina = (int)sonJson["Stamina"].n;
                    EquipmentType equipmentType =
                        (EquipmentType) Enum.Parse(typeof(EquipmentType), sonJson["EquipmentType"].str);
                    item = new Equipment(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,
                        sprite, strength, intelligent, agility, stamina, equipmentType);
                    break;
                case ItemType.Weapon:
                    int damage = (int) sonJson["Damage"].n;
                    WeaponType weaponType = (WeaponType) Enum.Parse(typeof(WeaponType), sonJson["WeaponType"].str);
                    item = new Weapon(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,
                        sprite, damage, weaponType);
                    break;
                case ItemType.Material:
                    item =new Material(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,
                        sprite);
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

    public void Update()
    {
        if (isPickedItem)
        {
            Vector2 targetItemUIPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out targetItemUIPosition);
            pickedItemUI.transform.localPosition=targetItemUIPosition;
        }else if (isToolTipShow)
        {
            //忘了游戏是怎么显示了，但是在unity3d里面，一旦tooltip出来了，显示位置是不会出现了的。
            //并且显示位置要第一个鼠标的高度，避免与鼠标重叠
            //如果要显示的内容超出屏幕外了又要怎么处理？
            Vector2 targetTooltipPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out targetTooltipPosition);
            toolTip.SetPosition(targetTooltipPosition+toolTipOffset);
        }
        if(isPickedItem && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            isPickedItem = false;
            PickedItemUI.Hide();
        }
    }

    public void ShowToolTip(string content)
    {
        isToolTipShow = true;
        toolTip.Show(content);
    }

    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();  
    }

    public void PickedItem(Item item, int amount)
    {
        pickedItemUI.Set(item, amount);
        isPickedItem = true;
        pickedItemUI.Show();
        HideToolTip();
    }

    public void PlacedItem(int amount=1)
    {
        pickedItemUI.ReduceAmount(amount);
        if (PickedItemUI.Amount <= 0)
        {
            isPickedItem = false;
            pickedItemUI.Hide();
        }
    }
}
