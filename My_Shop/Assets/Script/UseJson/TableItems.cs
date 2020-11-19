public class ShopTableItem
{
    public int TableId { get; set; }
    public string TableName1 { get; set; }
    public string TableName2 { get; set; }
    public string TableDescribe { get; set; }
}
public class OTCTableItem
{
    public int OTCid { get; set; }
    public string OTCname { get; set; }
    public string OTCdescribe { get; set; }
}
public class AwardTableItem
{
    public int AwardId { get; set; }
    public string AwardName { get; set; }
    public int One_probability { get; set; }
    public int Two_probability { get; set; }
    public int Three_probability { get; set; }
    public float Purchasing_price { get; set; }
    public int Image_id { get; set; }
}
public class GoodsTableItem
{
    /// <summary>
    /// 货物id
    /// </summary>
    public int GoodsId { get; set; }
    /// <summary>
    /// 货物名
    /// </summary>
    public string GoodsName { get; set; }
    /// <summary>
    /// 进货所需时间/分钟
    /// </summary>
    public float PurchaseTime { get; set; }
    /// <summary>
    /// 保质期/分钟
    /// </summary>
    public float ShelfLife { get; set; }
    /// <summary>
    /// 进价
    /// </summary>
    public float PurchasingPrice { get; set; }
    /// <summary>
    /// 图片索引
    /// </summary>
    public int ImageId { get; set; }
    /// <summary>
    /// 货物种类0零食1饮料2冷饮3百货
    /// </summary>
    public int TypeId { get; set; }
    /// <summary>
    /// 解锁销量等级
    /// </summary>
    public int SalesLevel { get; set; }
}
public class SalesRatioTableItem
{
    /// <summary>
    /// 销售等级
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// 销售额
    /// </summary>
    public string xse { get; set; }
}