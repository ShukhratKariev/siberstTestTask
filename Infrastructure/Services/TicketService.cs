using Application.Dtos;
using Application.Interfaces;
using Core;
using Core.Enums;
using Infrastructure.EntityFrameworkCore;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _dbContext;

    public TicketService(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    
    public int AddTicket(AddTicketDto addTicketDto)
    {
        var employees = _dbContext.Employees;
        var ticket = new Ticket()
        {
            Id = addTicketDto.Id,
            Name = addTicketDto.Name,
            Comment = addTicketDto.Comment,
            Creator = employees.FirstOrDefault(x => x.Id == addTicketDto.CreatorId) ?? new Employee(){Name = "PLACEHOLDER", Surname = "JOHNSON"},
            Priority = addTicketDto.Priority,
        };
        _dbContext.Tickets.Add(ticket);
        _dbContext.SaveChanges();
        return ticket.Id;
    }

    public void Edit(int id, EditTicketDto editTicketDto)
    {
        var ticketToEdit = _dbContext.Tickets.FirstOrDefault(x => x.Id == id);
        if (ticketToEdit is null)
            throw new ArgumentException("Ticket does not exist!");
        ticketToEdit.Name = editTicketDto.Name;
        ticketToEdit.Id = editTicketDto.Id;
        ticketToEdit.Priority = editTicketDto.Priority;
        ticketToEdit.Assignee = editTicketDto.Assignee;
        ticketToEdit.Creator = editTicketDto.Creator;
        ticketToEdit.Comment = editTicketDto.Comment;
        ticketToEdit.Status = editTicketDto.Status;

        _dbContext.SaveChanges();
    }

    public Ticket GetTicket(int id)
    {
        var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == id);
        if (ticket is null)
            throw new ArgumentException("Ticket does not exist!");
        return ticket;
    }

    public void Delete(int id)
    {
        var ticketToDelete = _dbContext.Tickets.FirstOrDefault(x => x.Id == id);
        if (ticketToDelete is null)
            throw new ArithmeticException("Ticket does not exist!");
        _dbContext.Tickets.Remove(ticketToDelete);
        _dbContext.SaveChanges();
    }

    public List<Ticket> GetTickets(TicketSearchArguments searchArguments)
    {
        var tickets = _dbContext.Tickets;
        var filter = CreateFilter(searchArguments);
        return tickets.Where(filter).ToList();
    }

    public async Task<List<EmployeeShortInfoDto>> GetEmployeesForSelectListAsync()
    {
        var result =  await _dbContext.Employees.Select(x => new EmployeeShortInfoDto
        {
            Id = x.Id,
            Name = x.Name + " " + x.Surname, 
        }).ToListAsync();
        return result;
    }

    private static ExpressionStarter<Ticket> CreateFilter(TicketSearchArguments ticketSearchArguments)
    {
        var filter = PredicateBuilder.New<Ticket>(true);
        if (!string.IsNullOrWhiteSpace(ticketSearchArguments.Name))
        {
            filter = filter.And(x => x.Name.Contains(ticketSearchArguments.Name));
        }
        if (ticketSearchArguments.Id.HasValue)
        {
            filter = filter.And(x => x.Id == ticketSearchArguments.Id);
        }

        return filter;
    }
}