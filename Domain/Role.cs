using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Role : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }
}