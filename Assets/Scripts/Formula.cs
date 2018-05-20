using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
    public int ScrollId { get; private set; }
    public int ScrollAmount { get; private set; }
    public int MaterialId { get; private set; }
    public int MaterialAmount { get; private set; }
    public int ResultId { get; private set; }

    private List<int> listNeccesary;

    public List<int> ListNeccesary
    {
        get { return listNeccesary;}
    }

    public Formula(int scrollId, int scrollAmount, int materialId, int materialAmount, int resultId)
    {
        ScrollId = scrollId;
        ScrollAmount = scrollAmount;
        MaterialId = materialId;
        MaterialAmount = materialAmount;
        ResultId = resultId;
        listNeccesary = new List<int>();
        for (int i = 0; i < scrollAmount; i++)
        {
            listNeccesary.Add(scrollId);
        }
        for (int i = 0; i < materialAmount; i++)
        {
            listNeccesary.Add(materialId);
        }
    }

    public bool Match(List<int> listInputItem)
    {
        List<int> listTempItem = new List<int>(listInputItem);
        foreach (int itemId in listNeccesary)
        {
            bool removeResult = listTempItem.Remove(itemId);
            if (!removeResult)
            {
                return false;
            }
        }
        return true;
    }
}
