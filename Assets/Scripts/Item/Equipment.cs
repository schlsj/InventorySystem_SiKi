using System;

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

    public override string GetTooltip()
    {
        string equipmentType = "";
        switch (EquipmentType)
        {
            case EquipmentType.Head:
                equipmentType = "头盔";
                break;
            case EquipmentType.Neck:
                equipmentType = "项链";
                break;
            case EquipmentType.Ring:
                equipmentType = "指环";
                break;
            case EquipmentType.Leg:
                equipmentType = "裤子";
                break;
            case EquipmentType.Bracer:
                equipmentType = "手套";
                break;
            case EquipmentType.Boots:
                equipmentType = "靴子";
                break;
            case EquipmentType.Shoulder:
                equipmentType = "肩甲";
                break;
            case EquipmentType.Belt:
                equipmentType = "腰带";
                break;
            case EquipmentType.OffHand:
                equipmentType = "副手";
                break;
            case EquipmentType.Chest:
                equipmentType = "铠甲";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return string.Format("{0}\n{1}  力量：{2}  智力：{3}  敏捷：{4}  耐力：{5}",
            base.GetTooltip(), equipmentType, Strength, Intellect, Agility, Stamina);
    }
}
