using Application.Dtos;
using Core;

namespace SibersTestTask.Models.Projects;

public class ProjectListViewModel
{
    public List<Project> Projects { get; set; }
    public ProjectSearchArguments ProjectSearchArguments { get; set; }
}