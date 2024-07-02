using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities
{
    public class UserEntity {

      [Key]
      [Column("id")]
      public string Id { get; init;}= Guid.NewGuid().ToString();
      [Column("email")]
      [Required]
      public string? Email { get; set;}
      [Column("password")]
      [Required]
      public string? Password { get; set;}
      [Column("date_create")]
      public DateTime DateCreate { get; set;}
      [Column("date_upgrade")]
      public DateTime DateUpgrade {get;set;}

      public ICollection<TaskEntity>? Tasks {get;set;} = new Collection<TaskEntity>();
    }
}