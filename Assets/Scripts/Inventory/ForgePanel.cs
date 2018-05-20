using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgePanel :Inventory
{
    private static ForgePanel _instance;

    public static ForgePanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ForgePanel").GetComponent<ForgePanel>();
            }
            return _instance;
        }
    }


    private List<Formula> listFormula;

    public override void Start()
    {
        base.Start();
        ParseFormulasJson();
    }

    private void ParseFormulasJson()
    {
        listFormula = new List<Formula>();
        TextAsset textAsset = Resources.Load<TextAsset>("Formulas");
        JSONObject rootJson = new JSONObject(textAsset.text);
        foreach (JSONObject sonJson in rootJson.list)
        {
            int scrollId = (int)sonJson["ScrollId"].n;
            int scrollAmount = (int)sonJson["ScrollAmount"].n;
            int materialId = (int)sonJson["MaterialId"].n;
            int materialAmount = (int)sonJson["MaterialAmount"].n;
            int resultId = (int)sonJson["ResultId"].n;
            Formula formula = new Formula(scrollId, scrollAmount, materialId, materialAmount, resultId);
            listFormula.Add(formula);
        }
        print(listFormula[1].ResultId);
    }

    public void Forge()
    {
        List<int> listInputItem = new List<int>();
        foreach (KnapsackSlot slot in ArrKnapsackSlot)
        {
            if (slot.transform.childCount != 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < itemUI.Amount; i++)
                {
                    listInputItem.Add(itemUI.Item.Id);
                }
            }
        }
        //这里没有考虑到背包全满的时候，放不小的情况吧。
        foreach (Formula formula in listFormula)
        {
            if (formula.Match(listInputItem))
            {
                bool storeResult=KnapsackPanel.Instance.StoreItem(formula.ResultId);
                if (storeResult)
                {
                    foreach (int id in formula.ListNeccesary)
                    {
                        RemoveItem(id);
                    }
                }
                else
                {
                    print("物品槽已满！");
                }
                break;
            }
        }
    }

    private void RemoveItem(int itemId)
    {
        foreach (KnapsackSlot slot in ArrKnapsackSlot)
        {
            if (slot.transform.childCount != 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                if (itemUI.Item.Id == itemId)
                {
                    itemUI.ReduceAmount();
                    if (itemUI.Amount == 0)
                    {
                        DestroyImmediate(itemUI.gameObject);
                    }
                    return;
                }
            }
        }
    }
}
