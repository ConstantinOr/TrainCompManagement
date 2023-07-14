using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrainCompManagement.DAL.Entities;

[Serializable]
[PrimaryKey(nameof(PathId))]
public class TrainTreePath
{
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   [Key, Column("Id", Order=0)]
   public long PathId { get; set; } 
   [ForeignKey("TrainInformation")]
   public long? AncestorId { get; set; }
   [ForeignKey("TrainInformation")]
   public long? DescendantId { get; set; }
   public TrainInformation? Ancestor { get; set; }
   public TrainInformation? Descendant { get; set; }
}