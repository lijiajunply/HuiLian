using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HuiLianMedical.WebApi.Models;

[Serializable]
public class CommodityModel : DataModel
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

    /// <summary>
    /// 图像路径
    /// </summary>
    [Column(TypeName = "varchar(32)")] 
    public string Image { get; set; } = "";

    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 库存
    /// </summary>
    [Column(TypeName = "varchar(32)")] 
    public int Stock { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [JsonIgnore]
    public List<CategoryModel> Category { get; set; } = [];

    [Column(TypeName = "varchar(32)")] 
    public string CreateTime { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
}