using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventory {

    private static CharacterPanel _instance;

    public static CharacterPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return _instance;
        }
    }

    private Text infoText;
    public override void Start()
    {
        base.Start();
        //infoText = transform.Find("PropertyPanel\\Text").GetComponent<Text>();//错的！！！
        infoText = transform.Find("PropertyPanel/Text").GetComponent<Text>();
    }

    public void DressItem(Item item)
    {
        foreach (var slot in arrSlot)
        {
            if (((EquipmentSlot)slot).CanShipItem(item))
            {
                if (slot.transform.childCount == 0)
                {
                    slot.Store(item);
                }
                else
                {
                    slot.transform.GetChild(0).GetComponent<ItemUI>().Set(item);
                    Knapsack.Instance.StoreItem(item);
                    InventoryManager.Instance.ShowToolTip(item.GetTooltip());
                }
                UpdateInfo();
                return;
            }
        }  
    }

    public void UpdateInfo()
    {
        Test test = GameObject.FindGameObjectWithTag("Player").GetComponent<Test>();
        int strength = test.BasicStrength;
        int intellect = test.BasicIntellect;
        int aligity = test.BasicAligity;
        int stamina = test.BasicStamina;
        int damage = test.BasicDamage;
        foreach (Slot slot in arrSlot)
        {
            if (slot.transform.childCount!=0)
            {
                Item item = slot.GetComponentInChildren<ItemUI>().Item;
                if (item is Equipment)
                {
                    Equipment euipment = (Equipment) item;
                    strength += euipment.Strength;
                    intellect += euipment.Intellect;
                    aligity += euipment.Agility;
                    stamina += euipment.Stamina;
                }else if (item is Weapon)
                {
                    Weapon weapon = (Weapon) item;
                    damage += weapon.Damage;
                }
            }
        }
        string info = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n伤害：{4}", strength, intellect, aligity, stamina,
            damage);
        infoText.text = info;
    }
}
