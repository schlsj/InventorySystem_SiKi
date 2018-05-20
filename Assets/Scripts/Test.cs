using UnityEngine;
using UnityEngine.UI;

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

    private int coin=100;
    private Text txtCoin;

    // Use this for initialization
    void Start ()
    {
        txtCoin = GameObject.Find("ImgCoin").GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.I))
        {
            int id = Random.Range(1, 18);
            KnapsackPanel.Instance.StoreItem(id);
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            EarnCoin(100);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            KnapsackPanel.Instance.DisplaySwitch();
        }else if (Input.GetKeyUp(KeyCode.T))//treasure,珍宝
        {
            ChestPanel.Instance.DisplaySwitch();
        }else if (Input.GetKeyUp(KeyCode.C))
        {
            CharacterPanel.Instance.DisplaySwitch();
        }else if (Input.GetKeyUp(KeyCode.V))
        {
            VendorPanel.Instance.DisplaySwitch();   
        }else if (Input.GetKeyUp(KeyCode.F))
        {
            ForgePanel.Instance.DisplaySwitch();
        }
	}

    public bool CanConsumeCoin(int amount)
    {
        return coin >= amount;
    }

    public void ConsumeCoin(int amount)
    {
        coin -= amount;
        txtCoin.text = coin.ToString();
    }

    public void EarnCoin(int amount)
    {
        coin += amount;
        txtCoin.text = coin.ToString();
    }
}
