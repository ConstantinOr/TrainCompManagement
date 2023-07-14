using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrainCompManagement.DAL.Entities;

[Serializable]
[PrimaryKey(nameof(TrainId))]
public class TrainInformation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column("Id", Order=0)]
    public long TrainId { get; set; }
    [Required] 
    public string Name { get; set; }
    [Required] 
    public string UniqueNumber { get; set; }
    [Required] 
    public bool IsQuantityAllowed { get; set; }
    [DefaultValue(0)]
    public long Quantity { get; set; }
    
    public virtual IEnumerable<TrainTreePath> Parent { get; }
    public virtual IEnumerable<TrainTreePath> Children { get; }
}
