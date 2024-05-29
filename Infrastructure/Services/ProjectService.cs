using Application.Dtos;
using Application.Interfaces;
using Core;
using Core.Enums;
using Infrastructure.EntityFrameworkCore;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Project> GetProjects(ProjectSearchArguments searchArguments)
    {
        var projects = _dbContext.Projects;
        var filter = CreateFilter(searchArguments);

        var result = projects.Where(filter).ToList();
        return result;
    }

    public Project GetProjectById(int id)
    {
        var projects = _dbContext.Projects
            .Include(x => x.Employees)
            .ThenInclude(x => x.Employee);

        var result = projects.FirstOrDefault(x => x.Id == id);
        if (result is not null)
            return result;
        throw new ArgumentException($"Project with {id} does not exist");
    }

    public int AddProject(AddProjectDto addProjectDto)
    {
        var projectToAdd = new Project()
        {
            StartDate = addProjectDto.StartDate,
            EndDate = addProjectDto.EndDate,
            Name = addProjectDto.ProjectName,
            ClientCompanyName = addProjectDto.ClientCompanyName,
            ContractorCompanyName = addProjectDto.ContractorCompanyName,
            Employees = addProjectDto.EmployeeIds.Select(x => new ProjectEmployee { EmployeeId = x }).ToList(),
            Priority = addProjectDto.Priority
        };
        _dbContext.Projects.Add(projectToAdd);
        _dbContext.SaveChanges();
        return projectToAdd.Id;
    }

    public void UpdateProject(int id, EditProjectDto editProjectDto)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
            throw new ArgumentException("Project not found!"); 
        project.Name = editProjectDto.Name;
        project.Priority = editProjectDto.Priority;
        if (editProjectDto.StartDate != null) project.StartDate = editProjectDto.StartDate.Value;
        if (editProjectDto.EndDate != null) project.EndDate = editProjectDto.EndDate.Value;
        project.ClientCompanyName = editProjectDto.ClientCompanyName;
        project.ContractorCompanyName = editProjectDto.ContractorName;

        _dbContext.SaveChanges();
    }

    public void DeleteProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project is null)
            return;
        _dbContext.Projects.Remove(project);
        _dbContext.SaveChanges();
    }

    private static ExpressionStarter<Project> CreateFilter(ProjectSearchArguments projectSearchArguments)
    {
        var filter = PredicateBuilder.New<Project>(true);
        if (!string.IsNullOrWhiteSpace(projectSearchArguments.Name))
        {
            filter = filter.And(x => x.Name.Contains(projectSearchArguments.Name));
        }

        if (projectSearchArguments.Priority.HasValue)
        {
            filter = filter.And(x => x.Priority == projectSearchArguments.Priority);
        }

        if (projectSearchArguments.StartDate.HasValue)
        {
            filter = filter.And(x => x.StartDate >= projectSearchArguments.StartDate);
        }

        if (projectSearchArguments.EndDate.HasValue)
        {
            filter = filter.And(x => x.EndDate >= projectSearchArguments.EndDate);
        }

        if (!string.IsNullOrWhiteSpace(projectSearchArguments.Client))
        {
            filter = filter.And(x => x.ClientCompanyName.Contains(projectSearchArguments.Client));
        }

        if (!string.IsNullOrWhiteSpace(projectSearchArguments.Contractor))
        {
            filter = filter.And(x => x.ContractorCompanyName.Contains(projectSearchArguments.Contractor));
        }

        return filter;
    }
}