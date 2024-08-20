using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiLianMedical.Share.Data;

[Serializable]
public class CategoryModel : DataModel
{
    [Key]
    [Column(TypeName = "varchar(32)")]
    public string Key { get; set; } = "";
    
    [Column(TypeName = "varchar(32)")] 
    public string Name { get; set; } = "";

    /// <summary>
    /// 商品介绍
    /// </summary>
    [Column(TypeName = "varchar(32)")] 
    public string Description { get; set; } = "";
    public List<CommodityModel> Commodities { get; set; } = [];
}