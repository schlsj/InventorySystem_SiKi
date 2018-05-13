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
}
