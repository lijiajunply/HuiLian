using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiLianMedical.Share.Data;

[Serializable]
public class AidModel : DataModel
{
    [Key]
    [Column(TypeName = "varchar(32)")]
    public string Key { get; set; } = "";

    [Column(TypeName = "varchar(18)")] public string IdCard { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Num { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Image { get; set; } = "";
}