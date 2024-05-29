using Application.Dtos;
using Core;

namespace Application.Interfaces;

public interface IEmployeeService
{
    List<Employee> GetEmployees(EmployeeSearchArguments searchArguments);
    
    public Employee GetEmployeeById(int id);

    public Employee EmployeeLookup(string name, string surname, string patronymic);

    public int CreateEmployee(AddEmployeeDto dto);

    public void DeleteEmployee(int id);

    public void UpdateEmployee(int id, UpdateEmployeeDto dto);
    
    Task<List<EmployeeShortInfoDto>> GetEmployeesForSelectListAsync();

}