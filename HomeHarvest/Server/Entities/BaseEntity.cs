
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHarvest.Server.Entities;
public class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
