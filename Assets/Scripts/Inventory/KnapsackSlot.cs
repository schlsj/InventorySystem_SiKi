using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class KnapsackSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    public GameObject prefabItem;
    protected ItemUI itemUI;

    public void Store(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject goItem = Instantiate(prefabItem);
            goItem.transform.SetParent(transform);
            goItem.transform.localPosition = Vector3.zero;
            goItem.transform.localScale=Vector3.one;
            itemUI = goItem.GetComponent<ItemUI>();
            itemUI.Set(item);
        }
        else
        {
            itemUI.AddAmount();
        }
    }

    public int GetItemId()
    {
        if (transform.childCount == 0)
        {
            return -1;
        }
        return itemUI.Item.Id;
    }

    public int GetItemAmount()
    {
        if (transform.childCount == 0)
        {
            return -1;
        }
        return itemUI.Amount;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount != 0 && !InventoryManager.Instance.IsPickedItem)
        {
            string toolTipContent = itemUI.Item.GetTooltip();
            InventoryManager.Instance.ShowToolTip(toolTipContent);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!InventoryManager.Instance.IsPickedItem&&eventData.button == PointerEventData.InputButton.Right)
        {
            Item item = itemUI.Item;
            if (item is Equipment || item is Weapon)
            {
                InventoryManager.Instance.HideToolTip();
                DestroyImmediate(itemUI.gameObject);
                CharacterPanel.Instance.DressItem(item);
            }
            return;
        }
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        //1 被点击的Slot上没有ItemUI；
        //1.a 鼠标上挂有PickedItemUI； 
        //1.a.1 ctrl按下，一次放置一个物品;                            END1!
        //1.a.2 ctrl没有按下，放置所有物品；                           END2!
        //1.b 鼠标上没有PickedItemUI；                                END3!
        //2 被点击的Slot上有ItemUI；
        //2.a 鼠标上挂有PickedItemUI； 
        //2.a.1 Slot中物品与鼠标上挂的PickedItemUI的ID不同；           END4!
        //2.a.2 Slot中物品与鼠标上挂的PickedItemUI的ID相同；
        //2.a.2.a ctrl被按下，一次放置一个物品；                         END5!
        //2.a.2.b ctrl没有按下；
        //2.a.2.b.1 如果可以完全放下                                    END6!
        //2.a.2.b.2 如果不能完全放下                                    END7!
        //2.b 鼠标上没有PickedItemUI；
        //2.b.1 ctrl按下，拾取一半物品；                                  END8!
        //2.b.2 ctrl没有按下，拾取所有物品；                            END9!
        if (transform.childCount != 0)
        {
            if (!InventoryManager.Instance.IsPickedItem)
            {
                if (Input.GetKey(KeyCode.LeftControl)) //2.b.1
                {
                    int pickedAmout = (itemUI.Amount + 1) / 2;
                    InventoryManager.Instance.PickedItem(itemUI.Item, pickedAmout);
                    int remainAmout = itemUI.Amount - pickedAmout;
                    if (remainAmout == 0)
                    {
                        Destroy(itemUI);
                    }
                    else
                    {
                        itemUI.SetAmount(remainAmout);
                    }
                }
                else //2.b.2
                {
                    InventoryManager.Instance.PickedItem(itemUI.Item, itemUI.Amount);
                    Destroy(itemUI.gameObject);
                }
            }
            else
            {
                if (itemUI.Item.Id == InventoryManager.Instance.PickedItemUI.Item.Id)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        //2.a.2.a ctrl被按下，一次放置一个物品；
                        if (itemUI.Item.Capacity > itemUI.Amount)
                        {
                            InventoryManager.Instance.PlacedItem();
                            itemUI.AddAmount();
                        }
                    }
                    else
                    {
                        int remainCapacity = itemUI.Item.Capacity - itemUI.Amount;
                        if (remainCapacity > 0)
                        {
                            if (remainCapacity >= InventoryManager.Instance.PickedItemUI.Amount)
                            {
                                itemUI.AddAmount(InventoryManager.Instance.PickedItemUI.Amount);
                                InventoryManager.Instance.PlacedItem(InventoryManager.Instance.PickedItemUI.Amount);
                                
                            }
                            else
                            {
                                itemUI.AddAmount(remainCapacity);
                                InventoryManager.Instance.PlacedItem(remainCapacity);
                            }
                        }
                    }
                }
                else
                {
                    InventoryManager.Instance.PickedItemUI.Exchange(itemUI);
                }
            }
        }
        else
        {
            if (InventoryManager.Instance.IsPickedItem)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Store(InventoryManager.Instance.PickedItemUI.Item);
                    InventoryManager.Instance.PlacedItem();
                }
                else
                {
                    for(int i = 0; i < InventoryManager.Instance.PickedItemUI.Amount;i++)
                    {
                        Store(InventoryManager.Instance.PickedItemUI.Item);
                    }
                    InventoryManager.Instance.PlacedItem(InventoryManager.Instance.PickedItemUI.Amount);
                }
            }
        }
    }
}
