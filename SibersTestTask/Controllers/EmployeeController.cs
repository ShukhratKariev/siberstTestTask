using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SibersTestTask.Models.Employees;

namespace SibersTestTask.Controllers;

[Route("[controller]/[action]")]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddEmployeeForm employeeForm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var addEmployeeDto = new AddEmployeeDto()
        {
            Id = employeeForm.Id,
            Name = employeeForm.FirstName,
            Surname = employeeForm.LastName,
            Email = employeeForm.Email,
            Patronymic = employeeForm.MiddleName
        };
        var id = _employeeService.CreateEmployee(addEmployeeDto);
        return RedirectToAction("List", new {id});
    }


    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee is null)
            return RedirectToAction(nameof(List));
        var viewModel = new EmployeeDetailsViewModel()
        {
            Employee = employee
        };
        return View(viewModel);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _employeeService.DeleteEmployee(id);
        return Ok();
    }


    [HttpGet("{id:int}")]
    public IActionResult Edit(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        var viewForm = new EditEmployeeForm()
        {
            FirstName = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Email = employee.Email
        };
        return View(viewForm);
    }


    [HttpPost("{id:int}")]
    public IActionResult Edit(int id, EditEmployeeForm form)
    {
        if (!ModelState.IsValid)
            return View();

        var editEmployeeDto = new UpdateEmployeeDto()
        {
            Name = form.FirstName,
            Surname = form.Surname,
            Email = form.Email,
            Patronymic = form.Patronymic
        };
        _employeeService.UpdateEmployee(id, editEmployeeDto);
        return RedirectToAction("Details", new {id});
    }


    [HttpGet]
    public IActionResult List([FromQuery] EmployeeSearchArguments employeeSearchArguments)
    {
        var employees = _employeeService.GetEmployees(employeeSearchArguments);
        var viewModel = new EmployeeListViewModel()
        {
            Employees = employees,
        };
        return View(viewModel);
    }
}