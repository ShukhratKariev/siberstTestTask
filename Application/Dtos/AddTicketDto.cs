using Core;
using Core.Enums;

namespace Application.Dtos;

public class AddTicketDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CreatorId { get; set; }
    public TicketProgressionEnum Status { get; set; }
    public string Comment { get; set; }
    public TicketPriorityEnum Priority { get; set; }
}