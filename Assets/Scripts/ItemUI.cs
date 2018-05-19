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

    public Vector3 AnimationScale = new Vector3(1.4f, 1.4f, 1.4f);
    public int SmoothSpeed = 20;

    void Update()
    {
        if (transform.localScale.x != 1.0f)
        {
            float targetScale = Mathf.Lerp(transform.localScale.x, 1.0f, Time.deltaTime * SmoothSpeed);
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            if (Mathf.Abs(transform.localScale.x - 1.0f) < 0.05f)
            {
                transform.localScale=Vector3.one;
            }
        }
    }

    public void Set(Item item, int amount = 1)
    {
        Item = item;
        Amount = amount;
        //更新UI显示
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        AmountText.text = item.Capacity>1?Amount.ToString():"";
        transform.localScale = AnimationScale;
    }

    public void Exchange(ItemUI changed)
    {
        Item middleItem = changed.Item;
        int middleAmount = changed.Amount;
        changed.Set(Item, Amount);
        Set(middleItem, middleAmount);
    }


    public void AddAmount(int increment=1)
    {
        Amount += increment;
        AmountText.text = Item.Capacity > 1 ? Amount.ToString() : "";
        //更新UI显示
        transform.localScale = AnimationScale;
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
        AmountText.text = Item.Capacity > 1 ? Amount.ToString() : "";
        transform.localScale = AnimationScale;
    }

    public void ReduceAmount(int reduction=1)
    {
        Amount -= reduction;
        AmountText.text = Item.Capacity > 1 ? Amount.ToString() : "";
        //更新UI显示
        transform.localScale = AnimationScale;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.position = position;
    }
}
