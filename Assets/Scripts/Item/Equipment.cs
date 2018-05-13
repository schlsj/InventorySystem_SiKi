public class Equipment : Item {

    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentType EquipmentType { get; set; }

    public Equipment(int id, string name, ItemType itemType, ItemQuality quality, string description, int capacity,
        int buyPrice,int sellPrice, string sprite, int strength, int intellect, int agility, int stamina, EquipmentType equipmentType) :
        base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipmentType = equipmentType;
    }
}
