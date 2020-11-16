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
    /// 酒馆锦囊表
    /// </summary>
    public List<OTCTableItem> OTCTable { get; set; }
    /// <summary>
    /// 游戏数值表
    /// </summary>
    public List<AwardTableItem> AwardTable { get; set; }
    /// <summary>
    /// 游戏数值表
    /// </summary>
    public List<GoodsTableItem> GoodsTable { get; set; }
}