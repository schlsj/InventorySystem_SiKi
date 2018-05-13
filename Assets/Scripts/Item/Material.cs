public class Material : Item {
    public Material(int id, string name, ItemType itemType, ItemQuality quality, string description, int capacity, int buyPrice,
        int sellPrice, string sprite) : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        
    }
}
