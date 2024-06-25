using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public abstract class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }
}