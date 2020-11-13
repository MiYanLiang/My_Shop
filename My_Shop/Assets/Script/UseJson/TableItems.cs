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
public class SnacksTableItem
{
    public int SnacksId { get; set; }
    public string SnacksName { get; set; }
    public string Date_in_produced { get; set; }
    public int Shelf_life { get; set; }
    public float Purchasing_price { get; set; }
}
public class DrinksTableItem
{
    public int DrinksId { get; set; }
    public string DrinksName { get; set; }
    public string Date_in_produced { get; set; }
    public int Shelf_life { get; set; }
    public float Purchasing_price { get; set; }
}
public class IceTableItem
{
    public int IceId { get; set; }
    public string IceName { get; set; }
    public string Date_in_produced { get; set; }
    public int Shelf_life { get; set; }
    public float Purchasing_price { get; set; }
}
public class SogoTableItem
{
    public int SogoId { get; set; }
    public string SogoName { get; set; }
    public string Date_in_produced { get; set; }
    public int Shelf_life { get; set; }
    public float Purchasing_price { get; set; }
}
public class AwardTableItem
{
    public int AwardId { get; set; }
    public string AwardName { get; set; }
    public int One_probability { get; set; }
    public int Two_probability { get; set; }
    public int Three_probability { get; set; }
    public float Purchasing_price { get; set; }
}