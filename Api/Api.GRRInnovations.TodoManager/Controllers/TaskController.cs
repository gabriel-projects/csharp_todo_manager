using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository TaskRepository;
        private readonly ICategoryRepository CategoryRepository;

        public TaskController(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            TaskRepository = taskRepository;
            CategoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateTask([FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            //validar
            var user = await HttpContext.JwtInfo();
            if (user == null) return Unauthorized();

            var taskModel = await wrapperInTask.Result();

            var category = await CategoryRepository.GetAsync(wrapperInTask.CategoryName);
            if (category == null)
            {
                //criar
            }

            TaskRepository.CreatAsync(taskModel, user.Model);

            return Ok();
        }
    }
}
