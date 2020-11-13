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
    /// 兑换码表
    /// </summary>
    public List<SnacksTableItem> SnacksTable { get; set; }
    /// <summary>
    /// 体力商店表
    /// </summary>
    public List<DrinksTableItem> DrinksTable { get; set; }
    /// <summary>
    /// 敌人BOSS固定敌人表
    /// </summary>
    public List<IceTableItem> IceTable { get; set; }
    /// <summary>
    /// 文本内容表
    /// </summary>
    public List<SogoTableItem> SogoTable { get; set; }
    /// <summary>
    /// 游戏数值表
    /// </summary>
    public List<AwardTableItem> AwardTable { get; set; }
}