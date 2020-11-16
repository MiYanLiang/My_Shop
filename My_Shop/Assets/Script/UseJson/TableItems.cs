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
    public int GoodsId { get; set; }
    public string GoodsName { get; set; }
    public float PurchaseTime { get; set; }
    public float ShelfLife { get; set; }
    public float PurchasingPrice { get; set; }
    public int ImageId { get; set; }
    public int TypeId { get; set; }
}