using Microsoft.AspNetCore.Mvc.Rendering;

namespace SibersTestTask.Models.Tickets;

public class AddTicketsViewModel
{
    public AddTicketForm Form { get; set; }
    public List<SelectListItem> EmployeeSelectListItem { get; set; }
}