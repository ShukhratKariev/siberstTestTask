using Application.Dtos;
using Core;

namespace SibersTestTask.Models.Tickets;

public class TicketListViewModel
{
    public List<Ticket> Tickets { get; set; }
    
    public TicketSearchArguments TicketSearchArguments { get; set; }
}