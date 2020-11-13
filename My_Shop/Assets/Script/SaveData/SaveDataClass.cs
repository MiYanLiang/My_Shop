using System.Collections.Generic;
using UnityEngine;

#region 玩家数据相关类

/// <summary>
/// 单个货物类
/// </summary>
public class WoodDataClass
{
    /// <summary>
    /// 货物种类
    /// </summary>
    public int woodType { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public int woodId { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int woodNum { get; set; }

    /// <summary>
    /// 卖价
    /// </summary>
    public int woodPrice { get; set; }

    /// <summary>
    /// 进货时间
    /// </summary>
    public string purchaseTime { get; set; }
}

/// <summary>
/// 货物存档类
/// </summary>
public class WoodsListDataClass
{
    /// <summary>
    /// 货物列表
    /// </summary>
    public List<WoodDataClass> woodDataClasses;
}

/// <summary>
/// 玩家基本信息存档数据类
/// </summary>
public class PlayerDataClass
{
    ///// <summary>
    ///// 姓名
    ///// </summary>
    //public string playerName { get; set; }
    ///// <summary>
    ///// 店铺名
    ///// </summary>
    //public string shopName { get; set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// 金钱数
    /// </summary>
    public float money { get; set; }
    /// <summary>
    /// 交租时间
    /// </summary>
    public string payRentTime { get; set; }
}

#endregion
