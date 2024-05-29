using Application.Dtos;
using Core;

namespace Application.Interfaces;

public interface ITicketService
{
    public int AddTicket(AddTicketDto addTicketDto);
    public void Edit(int id, EditTicketDto editTicketDto);
    public Ticket GetTicket(int id);
    public void Delete(int id);
    public List<Ticket> GetTickets(TicketSearchArguments searchArguments);
    Task<List<EmployeeShortInfoDto>> GetEmployeesForSelectListAsync();
}