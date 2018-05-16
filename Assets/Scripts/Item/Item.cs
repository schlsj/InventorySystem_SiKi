using System;

public class Item  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    public Item()
    {
        Id = -1;
    }

    public Item(int id, string name, ItemType itemType, ItemQuality quality, string description, int capacity, int buyPrice,
        int sellPrice,string sprite)
    {
        Id = id;
        Name = name;
        ItemType = itemType;
        Quality = quality;
        Description = description;
        Capacity = capacity;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }

    /// <summary>
    /// 这里难道不是显示description吗？
    /// </summary>
    /// <returns></returns>
    public virtual string GetTooltip()
    {
        string strColor = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                strColor = "lime";
                break;
            case ItemQuality.Rare:
                strColor = "navy";
                break;
            case ItemQuality.Epic:
                strColor = "magenta";
                break;
            case ItemQuality.Legendary:
                strColor = "orange";
                break;
            case ItemQuality.Artifact:
                strColor = "red";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        string tooltip = string.Format("<size=16><color={0}>{1}</color></size>\n购买价格:{2}  卖出价格:{3}\n{4}",
            strColor, Name, BuyPrice, SellPrice, Description);
        return tooltip;
    }
}
