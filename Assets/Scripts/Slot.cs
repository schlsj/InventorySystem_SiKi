using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    public GameObject prefabItem;

    public void Store(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject goItem = Instantiate(prefabItem);
            goItem.transform.SetParent(transform);
            goItem.transform.localPosition = Vector3.zero;
            goItem.GetComponent<ItemUI>().Init(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }

    public int GetItemId()
    {
        if (transform.childCount == 0)
        {
            return -1;
        }
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Id;
    }

    public int GetItemAmount()
    {
        if (transform.childCount == 0)
        {
            return -1;
        }
        return transform.GetChild(0).GetComponent<ItemUI>().Amount;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
