namespace Lab5estebanpacheco.Models;

using System.ComponentModel.DataAnnotations;

public class user
{
    [Key]
    public ulong id_user { get; set; }
    public string username { get; set; } = null!;
    public string password_hash { get; set; } = null!;
    public string role { get; set; } = "User";
}
