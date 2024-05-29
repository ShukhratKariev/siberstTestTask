using Application.Dtos;
using Application.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SibersTestTask.Models.Projects;

namespace SibersTestTask.Controllers;

[Route("[controller]/[action]")]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IEmployeeService _employeeService;

    public ProjectsController(
        IProjectService projectService,
        IEmployeeService employeeService)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ProjectSearchArguments projectSearchArguments)
    {
        var projects =  _projectService.GetProjects(projectSearchArguments);
        var viewModel = new ProjectListViewModel
        {
            Projects = projects,
        };
        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var project = _projectService.GetProjectById(id);
        if (project is null)
            return RedirectToAction(nameof(List));
        var viewModel = new ProjectDetailsViewModel
        {
            Project = project
        };
        return View(viewModel);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _projectService.DeleteProject(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var employees = await _employeeService.GetEmployeesForSelectListAsync();
        var selectListItems = employees.Select(x => 
                new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var viewModel = new AddProjectViewModel
        {
            EmployeesSelectListItems = selectListItems,
            Form = new AddProjectForm(),
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            
            var employees = await _employeeService.GetEmployeesForSelectListAsync();
            var selectListItems = employees.Select(x => 
                    new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var viewModel = new AddProjectViewModel
            {
                EmployeesSelectListItems = selectListItems,
                Form = form,
            };
            return View(viewModel);
        }

        var addProjectDto = new AddProjectDto
        {
            ProjectName = form.Name,
            ClientCompanyName = form.Client,
            ContractorCompanyName = form.ContractorCompanyName,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            EmployeeIds = form.ProjectEmployeeIds,
            ProjectLeadId = form.ProjectLeadId,
            Priority = form.Priority
        };
        var id = _projectService.AddProject(addProjectDto);
        return RedirectToAction("Details", new {id});
    }

    [HttpGet("{id:int}")]
    public IActionResult Edit(int id)
    {
        var project = _projectService.GetProjectById(id); ;
        var viewForm = new EditProjectForm
        {
            Name = project.Name,
            Client = project.ClientCompanyName,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            ContractorCompanyName = project.ContractorCompanyName,
            Priority = project.Priority
        };
        return View(viewForm);
    }

    [HttpPost("{id:int}")]
    public IActionResult Edit(int id, EditProjectForm form)
    {
        if (!ModelState.IsValid)
            return View();

        var editProjectDto = new EditProjectDto
        {
            Name = form.Name,
            ClientCompanyName = form.Client,
            ContractorName = form.ContractorCompanyName,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Priority = form.Priority
        };
        _projectService.UpdateProject(id, editProjectDto);
        return RedirectToAction("Details", new {id});
    }
}