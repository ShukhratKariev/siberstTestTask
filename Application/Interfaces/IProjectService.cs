using Application.Dtos;
using Core;

namespace Application.Interfaces;

public interface IProjectService
{
    List<Project> GetProjects(ProjectSearchArguments searchArguments);
    Project GetProjectById(int id);
    int AddProject(AddProjectDto addProjectDto);
    void UpdateProject(int id, EditProjectDto editProjectDto);
    void DeleteProject(int id);
}
