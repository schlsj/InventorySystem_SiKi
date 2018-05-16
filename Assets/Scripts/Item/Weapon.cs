using System;

public class Weapon :Item
{

    public int Damage { get; set; }
    public WeaponType WeaponType { get; set; }

    public Weapon(int id, string name, ItemType itemType, ItemQuality quality, string description, int capacity,
        int buyPrice, int sellPrice, string sprite, int damage, WeaponType weaponType) :
        base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        Damage = damage;
        WeaponType = weaponType;
    }

    public override string GetTooltip()
    {
        string weaponType = "";
        switch (WeaponType)
        {
            case WeaponType.Main:
                weaponType = "双手";
                break;
            case WeaponType.OffHand:
                weaponType = "单手";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return string.Format("{0}\n 类型：{1}  伤害：{2}", base.GetTooltip(), weaponType, Damage);
    }
}
