using Application.Dtos;
using Core;

namespace SibersTestTask.Models.Employees;

public class EmployeeListViewModel
{
    public List<Employee> Employees { get; set; }

    public EmployeeSearchArguments EmployeeSearchArguments { get; set; }
}