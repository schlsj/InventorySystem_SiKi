using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region 基础属性
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAligity = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength
    {
        get { return basicStrength;}
    }

    public int BasicIntellect
    {
        get { return basicIntellect; }
    }

    public int BasicAligity
    {
        get { return basicAligity; }
    }

    public int BasicStamina
    {
        get { return basicStamina; }
    }

    public int BasicDamage
    {
        get { return basicDamage; }
    }
#endregion


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.A))
        {
            int id = Random.Range(1, 18);
            Knapsack.Instance.StoreItem(id);
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            Knapsack.Instance.DisplaySwitch();
        }else if (Input.GetKeyUp(KeyCode.T))
        {
            Chest.Instance.DisplaySwitch();
        }else if (Input.GetKeyUp(KeyCode.C))
        {
            CharacterPanel.Instance.DisplaySwitch();
        }
	}
}
