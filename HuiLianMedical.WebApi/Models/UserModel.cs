using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiLianMedical.WebApi.Models;

[Serializable]
public class UserModel : DataModel
{
    [Key]
    [Column(TypeName = "varchar(32)")]
    public string Id { get; set; } = "";

    [Column(TypeName = "varchar(32)")] public string UserName { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Password { get; set; } = "";
    [Column(TypeName = "varchar(64)")] public string? Email { get; set; } = "";
    [Column(TypeName = "varchar(14)")] public string Phone { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Identity { get; set; } = "";
}

[Serializable]
public class LoginModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string? Email { get; set; } = "";
    public string Phone { get; set; } = "";
}