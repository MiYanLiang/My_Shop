using System.Collections.Generic;
using UnityEngine;

#region 玩家数据相关类

/// <summary>
/// 单个货物类
/// </summary>
public class GoodsDataClass
{
    /// <summary>
    /// 货物Id
    /// </summary>
    public int goodsId { get; set; }
    /// <summary>
    /// 数量
    /// </summary>
    public int goodsNum { get; set; }
    /// <summary>
    /// 卖价
    /// </summary>
    public float goodsPrice { get; set; }
    /// <summary>
    /// 是否过期
    /// </summary>
    public bool isExpired { get; set; }
    /// <summary>
    /// 进货时间
    /// </summary>
    public string purchaseTime { get; set; }
    /// <summary>
    /// 是否到货
    /// </summary>
    public bool isArrivaled { get; set; }
    /// <summary>
    /// 到货时间
    /// </summary>
    public string arrivalTime { get; set; }
}

/// <summary>
/// 货物存档类
/// </summary>
public class GoodsListDataClass
{
    /// <summary>
    /// 货物列表
    /// </summary>
    public List<GoodsDataClass> goodsDataClasses;
}

/// <summary>
/// 玩家基本信息存档数据类
/// </summary>
public class PlayerDataClass
{
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
