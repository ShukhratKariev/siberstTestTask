namespace Application.Dtos;

public class EmployeeSearchArguments
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
}