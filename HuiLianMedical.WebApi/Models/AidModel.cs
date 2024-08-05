using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiLianMedical.WebApi.Models;

[Serializable]
public class AidModel : DataModel
{
    [Key]
    [Column(TypeName = "varchar(32)")]
    public string Key { get; set; } = "";

    [Column(TypeName = "varchar(32)")] public string Name { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Image { get; set; } = "";
}