public class Consumable : Item
{
    public int HP;
    public int MP;

    public Consumable(int id, string name, ItemType itemType, ItemQuality quality, string description, int capacity,
        int buyPrice,int sellPrice,string sprite, int hp, int mp) :
        base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        HP = hp;
        MP = mp;
    }
}
