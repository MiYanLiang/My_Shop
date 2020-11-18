using System;
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
    Text goodsInfoBackPackText;


    [SerializeField]
    Text playerNameText;
    [SerializeField]
    Text shopNameText;
    [SerializeField]
    Text goldNumText;
    [SerializeField]
    Text levelText;

    string[] backPackTitleNameStr = new string[4] {
        "柜台",
        "冰箱",
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

        //给市场抬头栏按钮添加方法
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
    /// <param name="indexType">0，柜台；1，冰箱；2，冰柜;3，货架；</param>
    public void OpenBackPackOnClick(int indexType)
    {
        if (isOpenBackPack)
        {
            return;
        }

        //打开任意一个柜台后详细描述重置
        goodsInfoBackPackText.text = "暂无描述";

        backPackTitleText.text = backPackTitleNameStr[indexType];

        for (int i = 0; i < PlayerSaveDataCS.instance.gdsData.goodsDataClasses.Count; i++)
        {
            if (LoadJsonFile.gameDataBase.GoodsTable[PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].goodsId].TypeId == indexType)
            {
                if (!PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].isArrivaled)
                {
                    if (long.Parse(PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].arrivalTime) <= TimerControll.nowTimeLong)
                    {
                        PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].isArrivaled = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                ShowOneGoodsForBackPack(
                            LoadJsonFile.gameDataBase.GoodsTable[PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].goodsId].GoodsName,
                            LoadJsonFile.gameDataBase.GoodsTable[PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].goodsId].ImageId,
                            PlayerSaveDataCS.instance.gdsData.goodsDataClasses[i].goodsNum);
            }
        }

        backPackObj.SetActive(true);
        isOpenBackPack = true;
    }

    /// <summary>
    /// 展示设置一个背包物品的内容
    /// </summary>
    /// <param name="goodsName">货物名</param>
    /// <param name="goodsImgId">图片id</param>
    /// <param name="num">拥有数量</param>
    private void ShowOneGoodsForBackPack(string goodsName, int goodsImgId, float num)
    {
        GameObject obj = Instantiate(goodsObj, backPackContentObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + goodsImgId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Text>().text = num.ToString();
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            goodsInfoBackPackText.text = goodsName;
        });
    }

    /// <summary>
    /// 关闭背包
    /// </summary>
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
    GameObject goodsObj; //货物obj
    [SerializeField]
    Text goodsInfoMarketText;  //市场货物介绍
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

        for (int i = 0; i < LoadJsonFile.gameDataBase.GoodsTable.Count; i++)
        {
            if (LoadJsonFile.gameDataBase.GoodsTable[i].GoodsName != "" && LoadJsonFile.gameDataBase.GoodsTable[i].TypeId == indexType)
            {
                int goodsId = i;
                ShowOneGoodsForMarket(goodsId);
            }
        }

        marketWinObj.SetActive(true);
        isOpenBackPack = true;
    }

    /// <summary>
    /// 展示设置一个市场物品的内容
    /// </summary>
    /// <param name="goodsId">货物id</param>
    private void ShowOneGoodsForMarket(int goodsId)
    {

        GameObject obj = Instantiate(goodsObj, marketPackObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + LoadJsonFile.gameDataBase.GoodsTable[goodsId].ImageId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Text>().text = LoadJsonFile.gameDataBase.GoodsTable[goodsId].PurchasingPrice + "￥";
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            Debug.Log("goodsId: " + goodsId);
            goodsInfoMarketText.text = LoadJsonFile.gameDataBase.GoodsTable[goodsId].GoodsName;
            choosedNums = 1;
            choosedGoodsIndex = goodsId;
            InitUIForBuyGoods();
            buyNumsObj.SetActive(true);
        });
    }

    /// <summary>
    /// 关闭市场
    /// </summary>
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
        buyNumsObj.SetActive(false);
        marketWinObj.SetActive(false);
        isOpenBackPack = false;
    }

    [SerializeField]
    GameObject buyNumsObj;  //购买部分的控件

    [SerializeField]
    InputField buyNumInputText;

    [SerializeField]
    Text totalPriceText;    //市场总价text

    private int choosedGoodsIndex;  //记录市场所选货物id
    private int choosedNums;        //记录市场所选货物的数量

    /// <summary>
    /// 初始化购买货物
    /// </summary>
    private void InitUIForBuyGoods()
    {
        buyNumInputText.text = choosedNums.ToString();
        totalPriceText.text = LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].PurchasingPrice * choosedNums + "￥";
    }

    /// <summary>
    /// 点击数量加
    /// </summary>
    public void NumAddOnClick()
    {
        if (choosedNums > 99999)
        {
            choosedNums = 99999;
            return;
        }
        choosedNums++;
        InitUIForBuyGoods();
    }

    /// <summary>
    /// 点击数量减
    /// </summary>
    public void NumSubtractCarOnClick()
    {
        if (choosedNums < 1)
        {
            choosedNums = 1;
            return;
        }
        choosedNums--;
        InitUIForBuyGoods();
    }

    /// <summary>
    /// 输入购买数量后的方法
    /// </summary>
    public void EndInputBuyNumTextFun()
    {
        int getNums = int.Parse(buyNumInputText.text);
        if (getNums < 1)
        {
            choosedNums = 1;
        }
        else
        {
            if (getNums > 99999)
            {
                choosedNums = 99999;
            }
            else
            {
                choosedNums = getNums;
            }
        }
        InitUIForBuyGoods();
    }

    /// <summary>
    /// 点击尝试购买
    /// </summary>
    public void PayMoneyOnClick()
    {
        //总价
        float allPrice = LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].PurchasingPrice * choosedNums;

        if (PlayerSaveDataCS.instance.pyData.money < allPrice)
        {
            Debug.Log("money is not enouy");
            return;
        }
        else
        {
            ChangeMoneyNum(false, allPrice);

            //进货时间
            long nowTimeLong = TimerControll.nowTimeLong;
            //到货时间
            long arrivalTimeLong = nowTimeLong + (long)LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].PurchaseTime * 60000; //分钟 * 60 * 1000

            GoodsDataClass goodsDataClass = new GoodsDataClass();
            goodsDataClass.goodsId = choosedGoodsIndex;
            goodsDataClass.goodsNum = choosedNums;
            goodsDataClass.goodsPrice = LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].PurchasingPrice;
            goodsDataClass.isExpired = false;
            goodsDataClass.purchaseTime = nowTimeLong.ToString();
            goodsDataClass.isArrivaled = false;
            goodsDataClass.arrivalTime = arrivalTimeLong.ToString();
            PlayerSaveDataCS.instance.gdsData.goodsDataClasses.Add(goodsDataClass);

            string purchaseRecordStr = "{0}月{1}日 采购:{2}✖{3} 预计{4}到货;";
            DateTime dateTime_Now = TimerControll.GetStrBackTime();
            DateTime dateTime_Arrived = TimerControll.GetStrBackTime(arrivalTimeLong);

            purchaseRecordStr = string.Format(purchaseRecordStr, dateTime_Now.Month, dateTime_Now.Day, LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].GoodsName, choosedNums, dateTime_Arrived);
            PlayerSaveDataCS.instance.SetPurchaseLedgerForMonth(dateTime_Now.Year, dateTime_Now.Month, purchaseRecordStr);

            Debug.Log("真好，钱够");
        }
    }

    /// <summary>
    /// 改变金钱数量
    /// </summary>
    /// <param name="isAdd"></param>
    /// <param name="moneyNum"></param>
    private void ChangeMoneyNum(bool isAdd, float moneyNum)
    {
        if (isAdd)
        {
            PlayerSaveDataCS.instance.pyData.money += moneyNum;
        }
        else
        {
            PlayerSaveDataCS.instance.pyData.money -= moneyNum;
        }
        goldNumText.text = PlayerSaveDataCS.instance.pyData.money.ToString();
    }

    [SerializeField]
    GameObject zhangBenWinObj;  //账本obj
    [SerializeField]
    GameObject zhangBenViewObj;  //账本ViewObj
    [SerializeField]
    Transform zhangBenContentTran;  //账本内容父类
    [SerializeField]
    GameObject oneBillObj;  //账本单条记账obj

    /// <summary>
    /// 打开账本界面
    /// </summary>
    public void OpenZhangBenObjToShow()
    {
        if (isOpenBackPack)
        {
            return;
        }
        zhangBenWinObj.SetActive(true);
        isOpenBackPack = true;
    }

    /// <summary>
    /// 关闭账本界面
    /// </summary>
    public void CloseZhangBenObjToShow()
    {
        if (!isOpenBackPack)
        {
            return;
        }
        zhangBenViewObj.SetActive(false);
        zhangBenWinObj.SetActive(false);
        isOpenBackPack = false;
    }

    /// <summary>
    /// 打开进货账单
    /// </summary>
    public void OpenPurchaseRecordWinFun()
    {
        for (int i = 0; i < zhangBenContentTran.childCount; i++)
        {
            Destroy(zhangBenContentTran.GetChild(i).gameObject);
        }

        DateTime dateTime = TimerControll.GetStrBackTime();

        string recordStr = PlayerSaveDataCS.instance.GetPurchaseLedgerForMonth(dateTime.Year, dateTime.Month);

        string[] records = recordStr.Split(';');
        for (int i = 0; i < records.Length - 1; i++)
        {
            GameObject obj = Instantiate(oneBillObj, zhangBenContentTran);
            obj.GetComponentInChildren<Text>().text = records[i].ToString();
        }

        if (!zhangBenViewObj.activeSelf)
        {
            zhangBenViewObj.SetActive(true);
        }
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

}