using Microsoft.AspNetCore.Mvc.Rendering;

namespace SibersTestTask.Models.Projects;

public class AddProjectViewModel
{
    public List<SelectListItem> EmployeesSelectListItems { get; set; }
    public AddProjectForm Form { get; set; }
}