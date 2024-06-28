using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities;

public class TaskEntity {

  [Key]
  [Column("id")]
  public string Id { get; init;}= Guid.NewGuid().ToString();
      [Column("title")]
      [Required]
      public string? Title { get; set;}
      [Column("description")]
      [Required]
      public string? Description { get; set;}
      [Column("date_create")]
      public DateTime DateCreate { get; set;}
      [Column("date_upgrade")]
      public DateTime DateUpgrade {get;set;}
      [ForeignKey("user_id")]
      [Column("user_id")]
      public string? UserId {get;init;}

      public UserEntity? User {get;set;}
}