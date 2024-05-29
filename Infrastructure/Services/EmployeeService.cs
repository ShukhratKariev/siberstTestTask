using Application.Dtos;
using Application.Interfaces;
using Core;
using Infrastructure.EntityFrameworkCore;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Employee> GetEmployees(EmployeeSearchArguments searchArguments)
    {
        var employees = _dbContext.Employees;
        var filter = CreateFilter(searchArguments);

        var result = employees.Where(filter).ToList();

        return result;
    }
    

    private static ExpressionStarter<Employee> CreateFilter(EmployeeSearchArguments searchArguments)
    {
        var filter = PredicateBuilder.New<Employee>(true);

        if (searchArguments.Id.HasValue)
        {
            filter = filter.And(x => x.Id == searchArguments.Id);
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.Name))
        {
            filter = filter.And(x => x.Name.Contains(searchArguments.Name));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.Surname))
        {
            filter = filter.And(x => x.Surname.Contains(searchArguments.Surname));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.Patronymic))
        {
            filter = filter.And(x => x.Patronymic.Contains(searchArguments.Patronymic));
        }

        if (!string.IsNullOrWhiteSpace(searchArguments.Email))
        {
            filter = filter.And(x => x.Email.Contains(searchArguments.Email));
        }


        return filter;
    }

    public Employee GetEmployeeById(int id)
    {
        return _dbContext.Employees.FirstOrDefault(x => x.Id == id) ?? _dbContext.Employees.First();
    }

    public Employee EmployeeLookup(string name, string surname, string patronymic)
    {
        throw new NotImplementedException();
    }

    public int CreateEmployee(AddEmployeeDto dto)
    {
        var employee = new Employee()
        {
            Id = dto.Id,
            Name = dto.Name,
            Surname = dto.Surname,
            Patronymic = dto.Patronymic,
            Email = dto.Email
        };

        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();

        return employee.Id;
    }

    public void DeleteEmployee(int id)
    {
        var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        if (employee is null)
            return;
        _dbContext.Employees.Remove(employee);
        _dbContext.SaveChanges();
    }

    public void UpdateEmployee(int id, UpdateEmployeeDto dto)
    {
        var employeeToUpdate = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        if (employeeToUpdate is null)
            throw new ArgumentNullException(nameof(employeeToUpdate), "Employee not found!");


        employeeToUpdate.Name = dto.Name;
        employeeToUpdate.Surname = dto.Surname;
        employeeToUpdate.Patronymic = dto.Patronymic;
        employeeToUpdate.Email = dto.Email;

        _dbContext.SaveChanges();
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
}