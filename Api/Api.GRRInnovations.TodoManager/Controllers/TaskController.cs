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
        private readonly IUserRepository UserRepository;

        public TaskController(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            TaskRepository = taskRepository;
            CategoryRepository = categoryRepository;
            UserRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateTask([FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            //validar
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var wrapperModel = await wrapperInTask.Result();

            var category = await CategoryRepository.GetAsync(wrapperInTask.CategoryName);
            if (category == null)
            {
                category = await CategoryRepository.CreateAsync(wrapperInTask.CategoryName);
            }

            var user = await UserRepository.GetAsync(jwtModel.Model.Uid);

            var task = await TaskRepository.CreatAsync(wrapperModel, user, category);

            var response = await WrapperOutTask.From(task).ConfigureAwait(false);
            return new OkObjectResult(response);
        }
    }
}
