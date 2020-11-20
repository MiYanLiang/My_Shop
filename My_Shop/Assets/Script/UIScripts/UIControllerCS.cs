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

    }

    private void Start()
    {
        LoadSaveData.instance.LoadByJson();

        InitUIForGameProp();
    }

    /// <summary>
    /// 初始化相关内容
    /// </summary>
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

        //playerNameText.text = PlayerSaveDataCS.instance.pyData.playerName;
        //shopNameText.text = PlayerSaveDataCS.instance.pyData.shopName;
        goldNumText.text = PlayerSaveDataCS.instance.pyData.money.ToString();
        levelText.text = PlayerSaveDataCS.instance.pyData.level.ToString();

        detaildAvatarImg = detaildWinObj.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        detaildNameText = detaildWinObj.transform.GetChild(1).GetComponent<Text>();
        detaildSpecialText = detaildWinObj.transform.GetChild(2).GetComponent<Text>();
        detaildInfoText = detaildWinObj.transform.GetChild(3).GetComponent<Text>();
        detaildShelfLifeText = detaildWinObj.transform.GetChild(4).GetComponent<Text>();
        detaildPurchasePText = detaildWinObj.transform.GetChild(5).GetComponent<Text>();
        detaildSellingP = detaildWinObj.transform.GetChild(6).GetChild(0).GetComponent<InputField>();
        detaildConfirmBtn = detaildWinObj.transform.GetChild(7).GetComponent<Button>();
        detaildConfirmBtn.onClick.AddListener(CloseDetaildWinOnClick);
        detaildThrowAwayBtn = detaildWinObj.transform.GetChild(8).GetComponent<Button>();
    }

    [SerializeField]
    GameObject goodsInfoBtn;    //货物详情btn的obj

    private int chooseBackPackGoodsId;  //记录背包里选择的物品id
    private GameObject goodsSelectImgObj;  //记录背包里选择的物品Obj

    [SerializeField]
    GameObject detaildWinObj;   //货物详情WinObj
    private Image detaildAvatarImg;     //货物详情中的头像Img
    private Text detaildNameText;       //货物详情中的货物名
    private Text detaildSpecialText;    //货物详情中的特殊货物名
    private Text detaildInfoText;       //货物详情中的详细介绍
    private Text detaildShelfLifeText;  //货物详情中的保质期内容
    private Text detaildPurchasePText;  //货物详情中的进价
    private InputField detaildSellingP; //货物详情中的售价
    private Button detaildConfirmBtn;   //货物详情中的确认按钮
    private Button detaildThrowAwayBtn; //货物详情中的丢弃按钮

    /// <summary>
    /// 打开物品详情窗口
    /// </summary>
    public void OpenDetaildWinOnClick()
    {
        detaildAvatarImg.sprite = Resources.Load("Image/InventoryImg/" + LoadJsonFile.gameDataBase.GoodsTable[chooseBackPackGoodsId].ImageId, typeof(Sprite)) as Sprite;
        detaildNameText.text = LoadJsonFile.gameDataBase.GoodsTable[chooseBackPackGoodsId].GoodsName;
        detaildSpecialText.text = "特描";
        detaildInfoText.text = "详介";

        int goodsNums = 0;
        for (int i = 0; i < PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId].Count; i++)
        {
            if (PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId][i].isArrivaled)
            {
                goodsNums += PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId][i].goodsNum;
            }
        }
        detaildShelfLifeText.text = "数量: " + goodsNums + "\n保质期: " + LoadJsonFile.gameDataBase.GoodsTable[chooseBackPackGoodsId].ShelfLife + "分钟";

        detaildPurchasePText.text = "进价: " + LoadJsonFile.gameDataBase.GoodsTable[chooseBackPackGoodsId].PurchasingPrice + "￥";

        detaildSellingP.text = PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId][0].goodsPrice.ToString();

        detaildWinObj.SetActive(true);
    }

    /// <summary>
    /// 确认后关闭物品详情窗口
    /// </summary>
    private void CloseDetaildWinOnClick()
    {
        float sellPrice = float.Parse(detaildSellingP.text);
        if (sellPrice!= PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId][0].goodsPrice)
        {
            for (int i = 0; i < PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId].Count; i++)
            {
                PlayerSaveDataCS.instance.gdsDataForGame[chooseBackPackGoodsId][i].goodsPrice = sellPrice;
            }
            LoadSaveData.instance.SaveGameData(2);
        }
        detaildWinObj.SetActive(false);
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    /// <param name="indexType">0，柜台；1，冰箱；2，冰柜;3，货架；</param>
    public void OpenBackPackOnClick(int indexType)
    {
        if (BackMove_PC.isBackMoveNow)
        {
            return;
        }
        if (isOpenBackPack)
        {
            return;
        }

        //打开任意一个柜台后详细描述重置
        goodsInfoBackPackText.text = "暂无描述";
        goodsInfoBtn.SetActive(false);
        chooseBackPackGoodsId = -1;
        goodsSelectImgObj = null;

        backPackTitleText.text = backPackTitleNameStr[indexType];
        bool isNeedSave = false;
        //遍历背包所有id的货物
        for (int i = 0; i < PlayerSaveDataCS.instance.gdsDataForGame.Count; i++)
        {
            //该id是否有货物
            if (PlayerSaveDataCS.instance.gdsDataForGame[i].Count > 0)
            {
                //判断是否是当前柜台类型的货物
                if (LoadJsonFile.gameDataBase.GoodsTable[PlayerSaveDataCS.instance.gdsDataForGame[i][0].goodsId].TypeId == indexType)
                {
                    int arrivaledNum = 0;   //记录总共到货的数量
                    for (int j = 0; j < PlayerSaveDataCS.instance.gdsDataForGame[i].Count; j++)
                    {
                        //判断是否到货
                        if (!PlayerSaveDataCS.instance.gdsDataForGame[i][j].isArrivaled)
                        {
                            //未到货则刷新是否到货
                            if (long.Parse(PlayerSaveDataCS.instance.gdsDataForGame[i][j].arrivalTime) <= TimerControll.nowTimeLong)
                            {
                                PlayerSaveDataCS.instance.gdsDataForGame[i][j].isArrivaled = true;
                                isNeedSave = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        arrivaledNum += PlayerSaveDataCS.instance.gdsDataForGame[i][j].goodsNum;
                    }
                    //有到货的货物
                    if (arrivaledNum > 0)
                    {
                        int indexGoodId = i;
                        ShowOneGoodsForBackPack(indexGoodId, arrivaledNum);
                    }
                }
            }
        }
        if (isNeedSave)
            LoadSaveData.instance.SaveGameData(2);

        backPackObj.SetActive(true);
        isOpenBackPack = true;
    }

    /// <summary>
    /// 展示设置一个背包物品的内容
    /// </summary>
    /// <param name="goodsName">货物名</param>
    /// <param name="goodsImgId">图片id</param>
    /// <param name="num">拥有数量</param>
    private void ShowOneGoodsForBackPack(int goodsId, int goodsNum)
    {
        GameObject obj = Instantiate(goodsObj, backPackContentObj.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("Image/InventoryImg/" + LoadJsonFile.gameDataBase.GoodsTable[goodsId].ImageId, typeof(Sprite)) as Sprite;
        obj.GetComponentInChildren<Text>().text = goodsNum + "个";
        obj.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            ChangeGoodsSelectImgFun(obj);
            chooseBackPackGoodsId = goodsId;
            goodsInfoBackPackText.text = LoadJsonFile.gameDataBase.GoodsTable[goodsId].GoodsName;
            goodsInfoBtn.SetActive(true);
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
        if (BackMove_PC.isBackMoveNow)
        {
            return;
        }
        for (int i = 0; i < marketPackObj.transform.childCount; i++)
        {
            Destroy(marketPackObj.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < marketTitleBtnsObj.transform.childCount; i++)
        {
            if (indexType == i)
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(1.2f, 1.2f);
            }
            else
            {
                marketTitleBtnsObj.transform.GetChild(i).transform.localScale = new Vector3(1f, 1f);
            }
        }

        for (int i = 0; i < LoadJsonFile.gameDataBase.GoodsTable.Count; i++)
        {
                //货物不为空
            if (LoadJsonFile.gameDataBase.GoodsTable[i].GoodsName != "" && 
                //是否是该种类货物
                LoadJsonFile.gameDataBase.GoodsTable[i].TypeId == indexType && 
                //销售等级是否可以解锁该货物
                PlayerSaveDataCS.instance.goodsSalesLevel[indexType] >= LoadJsonFile.gameDataBase.GoodsTable[i].SalesLevel)
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
            print("goodsId: " + goodsId);
            ChangeGoodsSelectImgFun(obj);
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

    /// <summary>
    /// 改变货物选择图片的展示
    /// </summary>
    /// <param name="goodsObj"></param>
    private void ChangeGoodsSelectImgFun(GameObject goodsObj)
    {
        if (goodsSelectImgObj != null)
        {
            goodsSelectImgObj.SetActive(false);
        }
        goodsSelectImgObj = goodsObj.transform.GetChild(3).gameObject;
        goodsSelectImgObj.SetActive(true);
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
            goodsDataClass.goodsPrice = LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].PurchasingPrice * 2;
            goodsDataClass.isExpired = false;
            goodsDataClass.purchaseTime = nowTimeLong.ToString();
            goodsDataClass.isArrivaled = false;
            goodsDataClass.arrivalTime = arrivalTimeLong.ToString();
            PlayerSaveDataCS.instance.gdsData.goodsDataClasses.Add(goodsDataClass);
            PlayerSaveDataCS.instance.gdsDataForGame[goodsDataClass.goodsId].Add(goodsDataClass);
            LoadSaveData.instance.SaveGameData(2);

            //添加该类货物的销量
            PlayerSaveDataCS.instance.SetSalesVolume((GoodsTypeEnum)LoadJsonFile.gameDataBase.GoodsTable[choosedGoodsIndex].TypeId, choosedNums);

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
        LoadSaveData.instance.SaveGameData(1);
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
        if (BackMove_PC.isBackMoveNow)
        {
            return;
        }
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