using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Role : BaseEntity
{
    [Column("id")]
    public new int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}