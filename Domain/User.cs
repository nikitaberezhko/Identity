using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class User : BaseEntity
{
    [Column("role_id")]
    public Guid RoleId { get; set; }
    
    public Role Role { get; set; }
    
    [Column("login")]
    public string Login { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}