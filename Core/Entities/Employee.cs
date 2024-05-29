namespace Core;

public class Employee
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Patronymic { get; set; }
    
    public string Email { get; set; }
    
    public List<ProjectEmployee> Projects { get; set; }
    
    public List<Ticket> AssignedTickets { get; set; }
    public List<Ticket> CreatedTickets { get; set; }
   

    public string GetFullName() => Name + " " + Surname + " " + Patronymic;

}