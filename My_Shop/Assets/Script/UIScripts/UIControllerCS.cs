using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerCS : MonoBehaviour
{
    public static UIControllerCS instance;

    [HideInInspector]
    public bool isOpenBackPack; //是否开启背包界面
    [SerializeField]
    GameObject backPackObj; //背包界面
    [SerializeField]
    Text backPackTitleText;
    [SerializeField]
    GameObject backPackContentObj;  //背包obj
    [SerializeField]
    Text woodInfoBackPackText;


    [SerializeField]
    Text playerNameText;
    [SerializeField]
    Text shopNameText;
    [SerializeField]
    Text goldNumText;
    [SerializeField]
    Text levelText;

    string[] backPackTitleNameStr = new string[4] {
        "冰箱",
        "柜台",
        "冰柜",
        "货架"
    };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        InitUIForGameProp();
    }

    //初始化相关内容
    private void InitUIForGameProp()
    {
        isOpenBackPack = false;

        for (int i = 0; i < marketTitleBtnsObj.transform.childCount; i++)
        {
            int index = i;
            marketTitleBtnsObj.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                OpenMarketToShow(index);
            });
        }
    }

    private void Start()
    {
        LoadSaveData.instance.LoadByJson();

        InitUIForGameData();
    }

    private void InitUIForGameData()
    {
        //playerNameText.text = PlayerSaveDataCS.instance.pyData.playerName;
        //shopNameText.text = PlayerSaveDataCS.instance.pyData.shopName;
        goldNumText.text = PlayerSaveDataCS.instance.pyData.money.ToString();
        levelText.text = PlayerSaveDataCS.instance.pyData.level.ToString();
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    /// <param name="indexType">0，冰箱；1，；2，3；</param>
    public void OpenBackPackOnClick(int indexType)
    {
        if (isOpenBackPack)
        {
            return;
        }

        backPackTitleText.text = backPackTitleNameStr[indexType];

        for (int i = 0; i < PlayerSaveDataCS.instance.wdData.woodDataClasses.Count; i++)
        {
            switch ((MarketWoodsType)PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodType)
            {
                case MarketWoodsType.lingShi:
                    ShowOneWoodForBackPack(
                        LoadJsonFile.gameDataBase.SnacksTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].SnacksName,
                        LoadJsonFile.gameDataBase.SnacksTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].Image_id,
                        PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodPrice);
                    break;
                case MarketWoodsType.yinLiao:
                    ShowOneWoodForBackPack(
                        LoadJsonFile.gameDataBase.DrinksTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].DrinksName,
                        LoadJsonFile.gameDataBase.DrinksTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].Image_id,
                        PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodPrice);
                    break;
                case MarketWoodsType.xueGao:
                    ShowOneWoodForBackPack(
                        LoadJsonFile.gameDataBase.IceTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].IceName,
                        LoadJsonFile.gameDataBase.IceTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].Image_id,
                        PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodPrice);
                    break;
                case MarketWoodsType.baiHuo:
                    ShowOneWoodForBackPack(
                        LoadJsonFile.gameDataBase.SogoTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].SogoName,
                        LoadJsonFile.gameDataBase.SogoTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].Image_id,
                        PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodPrice);
                    break;
                case MarketWoodsType.jiangChi:
                    ShowOneWoodForBackPack(
                        LoadJsonFile.gameDataBase.AwardTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].AwardName,
                        LoadJsonFile.gameDataBase.AwardTable[PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodId].Image_id,
                        PlayerSaveDataCS.instance.wdData.woodDataClasses[i].woodPrice);
                    break;
                default:
                    break;
            }
        }

        backPackObj.SetActive(true);
        isOpenBackPack = true;
    }

    //展示设置一个背包物品的内容
    private void ShowOneWoodForBackPack(string woodName, int woodImgId, float num)
    {
        GameObject obj = Instantiate(woodObj, backPackContentObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + woodImgId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Text>().text = num.ToString();
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            woodInfoBackPackText.text = woodName;
        });
    }

    //关闭背包
    public void CloseBackePackOnClick()
    {
        if (!isOpenBackPack)
        {
            return;
        }
        for (int i = 0; i < backPackContentObj.transform.childCount; i++)
        {
            Destroy(backPackContentObj.transform.GetChild(i).gameObject);
        }
        backPackObj.SetActive(false);
        isOpenBackPack = false;
    }

    [SerializeField]
    GameObject marketWinObj;    //市场界面
    [SerializeField]
    GameObject marketPackObj;   //市场背包
    [SerializeField]
    GameObject woodObj; //货物obj
    [SerializeField]
    Text woodInfoMarketText;  //货物介绍
    [SerializeField]
    GameObject marketTitleBtnsObj;  //市场顶部按钮obj

    /// <summary>
    /// 打开市场
    /// </summary>
    public void OpenMarketToShow(int indexType)
    {
        for (int i = 0; i < marketPackObj.transform.childCount; i++)
        {
            Destroy(marketPackObj.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < marketTitleBtnsObj.transform.childCount; i++)
        {
            if (indexType == i)
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(1.5f, 1.5f);
            }
            else
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(1f, 1f);
            }
        }

        switch ((MarketWoodsType)indexType)
        {
            case MarketWoodsType.lingShi:
                for (int i = 0; i < LoadJsonFile.gameDataBase.SnacksTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.SnacksTable[i].SnacksName, LoadJsonFile.gameDataBase.SnacksTable[i].Image_id, LoadJsonFile.gameDataBase.SnacksTable[i].Purchasing_price);
                }
                break;
            case MarketWoodsType.yinLiao:
                for (int i = 0; i < LoadJsonFile.gameDataBase.DrinksTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.DrinksTable[i].DrinksName, LoadJsonFile.gameDataBase.DrinksTable[i].Image_id, LoadJsonFile.gameDataBase.DrinksTable[i].Purchasing_price);
                }
                break;
            case MarketWoodsType.xueGao:
                for (int i = 0; i < LoadJsonFile.gameDataBase.IceTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.IceTable[i].IceName, LoadJsonFile.gameDataBase.IceTable[i].Image_id, LoadJsonFile.gameDataBase.IceTable[i].Purchasing_price);
                }
                break;
            case MarketWoodsType.baiHuo:
                for (int i = 0; i < LoadJsonFile.gameDataBase.SogoTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.SogoTable[i].SogoName, LoadJsonFile.gameDataBase.SogoTable[i].Image_id, LoadJsonFile.gameDataBase.SogoTable[i].Purchasing_price);
                }
                break;
            case MarketWoodsType.jiangChi:
                for (int i = 0; i < LoadJsonFile.gameDataBase.AwardTable.Count; i++)
                {
                    ShowOneWoodForMarket(LoadJsonFile.gameDataBase.AwardTable[i].AwardName, LoadJsonFile.gameDataBase.AwardTable[i].Image_id, LoadJsonFile.gameDataBase.AwardTable[i].Purchasing_price);
                }
                break;
            default:
                break;
        }
        marketWinObj.SetActive(true);
        InitUIForBuyGoods();
        isOpenBackPack = true;
    }

    //关闭市场
    public void CloseMarketOnClick()
    {
        if (!isOpenBackPack)
        {
            return;
        }
        for (int i = 0; i < marketPackObj.transform.childCount; i++)
        {
            Destroy(marketPackObj.transform.GetChild(i).gameObject);
        }
        marketWinObj.SetActive(false);
        isOpenBackPack = false;
    }

    //展示设置一个市场物品的内容
    private void ShowOneWoodForMarket(string woodName, int woodImgId, float moneyNum)
    {
        GameObject obj = Instantiate(woodObj, marketPackObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + woodImgId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Text>().text = moneyNum + "￥";
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            woodInfoMarketText.text = woodName;
        });
    }


    [SerializeField]
    GameObject catLevelObj;  //猫咪升级面板
    /// <summary>
    /// 打开猫咪升级面板
    /// </summary>
    public void OpenCatLevelOnClick()
    {
        catLevelObj.SetActive(true);
        isOpenBackPack = true;

    }

    /// <summary>
    /// 关闭猫咪升级面板
    /// </summary>
    public void CloseCatLevelOnClick()
    {
        if (!isOpenBackPack)
        {
            return;
        }
        catLevelObj.SetActive(false);
        isOpenBackPack = false;

    }


    [SerializeField]
    InputField buyNum;


    /// <summary>
    /// 初始化购买货物数据(transition:过渡变量)
    /// </summary>
    private void InitUIForBuyGoods()
    { 
        int transition_initNum = 1;
        buyNum.text = transition_initNum.ToString();
    }

    /// <summary>
    /// 点击加入购物车
    /// </summary>
    public void BuyInCarOnClick()
    {
        print("..."+buyNum.text.ToString());
    }
    /// <summary>
    /// 点击数量加
    /// </summary>
    public void NumAddOnClick()
    {
        if (int.Parse(buyNum.text.ToString()) > 0)
        {
            buyNum.text = (int.Parse(buyNum.text.ToString()) + 1).ToString();
        }
    }
    /// <summary>
    /// 点击数量减
    /// </summary>
    public void NumSubtractCarOnClick()
    {
        if (int.Parse(buyNum.text.ToString()) > 1)
        {
            buyNum.text = (int.Parse(buyNum.text.ToString()) - 1).ToString();
        }
    }

    /// <summary>
    /// 点击购买打开支付界面
    /// </summary>
    public void PayMoneyOnClick()
    {

    }
}