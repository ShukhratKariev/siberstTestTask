using System.ComponentModel.DataAnnotations;
using Core;
using Core.Enums;

namespace SibersTestTask.Models.Tickets;

public class AddTicketForm
{
    [Required]public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Comment is required")] [MaxLength(300)]
    public string Comment { get; set; }
    public TicketPriorityEnum Priority { get; set; }
    
    [Required(ErrorMessage = "Creator is required")]
    public int CreatorId { get; set; }
}