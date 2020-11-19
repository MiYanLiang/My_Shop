using System.Collections.Generic;

/// <summary>
/// 控制数据表Root
/// </summary>
public class Roots
{
    /// <summary>
    /// 
    /// </summary>
    public List<ShopTableItem> ShopTable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<OTCTableItem> OTCTable { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<AwardTableItem> AwardTable { get; set; }
    /// <summary>
    /// 货物数值表
    /// </summary>
    public List<GoodsTableItem> GoodsTable { get; set; }
    /// <summary>
    /// 销售额表
    /// </summary>
    public List<SalesRatioTableItem> SalesRatioTable { get; set; }
}