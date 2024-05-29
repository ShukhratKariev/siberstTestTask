using Application.Dtos;
using Application.Interfaces;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SibersTestTask.Models.Tickets;

namespace SibersTestTask.Controllers;

[Route("[controller]/[action]")]
public class TicketController : Controller
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var employees = await _ticketService.GetEmployeesForSelectListAsync();
        var selectListItems = employees.Select(x =>
                new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var viewModel = new AddTicketsViewModel()
        {
            EmployeeSelectListItem = selectListItems,
            Form = new AddTicketForm()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddTicketForm ticketForm)
    {
        if (!ModelState.IsValid)
        {
            var employees = await _ticketService.GetEmployeesForSelectListAsync();
            var selectListItems = employees.Select(x =>
                    new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var viewModel = new AddTicketsViewModel()
            {
                EmployeeSelectListItem = selectListItems,
                Form = ticketForm
            };
            return View(viewModel);
        }

        var addTicketDto = new AddTicketDto()
        {
            Id = ticketForm.Id,
            Name = ticketForm.Name,
            Comment = ticketForm.Comment,
            Priority = ticketForm.Priority,
            CreatorId = ticketForm.CreatorId
        };
        var id = _ticketService.AddTicket(addTicketDto);
        return RedirectToAction("Details", new { id });
    }


    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var ticket = _ticketService.GetTicket(id);
        if (ticket is null)
            return RedirectToAction(nameof(List));
        var viewModel = new TicketDetailsViewModel()
        {
            Ticket = ticket
        };
        return View(viewModel);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _ticketService.Delete(id);
        return Ok();
    }


    [HttpGet("{id:int}")]
    public IActionResult Edit(int id)
    {
        var ticket = _ticketService.GetTicket(id);
        var viewForm = new EditTicketForm()
        {
            Name = ticket.Name,
            Id = ticket.Id,
            Comment = ticket.Comment,
            Status = ticket.Status,
            Priority = ticket.Priority
        };
        return View(viewForm);
    }


    [HttpPost("{id:int}")]
    public IActionResult Edit(int id, EditTicketForm form)
    {
        if (!ModelState.IsValid)
            return View();

        var editTicketDto = new EditTicketDto()
        {
            Name = form.Name,
            Id = form.Id,
            Comment = form.Comment,
            Creator = form.Creator,
            Assignee = form.Assignee,
            Status = form.Status,
            Priority = form.Priority
        };
        _ticketService.Edit(id, editTicketDto);
        return RedirectToAction("Details", new { id });
    }


    [HttpGet]
    public IActionResult List([FromQuery] TicketSearchArguments ticketSearchArguments)
    {
        var tickets = _ticketService.GetTickets(ticketSearchArguments);
        var viewModel = new TicketListViewModel()
        {
            Tickets = tickets,
        };
        return View(viewModel);
    }
}