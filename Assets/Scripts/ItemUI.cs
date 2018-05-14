using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Item Item { get; set; }
    public int Amount { get; set; }

    #region 图形控件 感觉有点山寨

    private Image itemImage;
    private Text amoutText;

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private Text AmountText
    {
        get
        {
            if (amoutText == null)
            {
                amoutText = GetComponentInChildren<Text>();
            }
            return amoutText;
        }
    }
#endregion

    public void Init(Item item, int amount = 1)
    {
        Item = item;
        Amount = amount;
        //更新UI显示
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        AmountText.text = Amount.ToString();
    }

    public void AddAmount(int increment=1)
    {
        Amount += increment;
        AmountText.text = Amount.ToString();
        //更新UI显示
    }
}
