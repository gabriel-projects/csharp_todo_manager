using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Models;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository TaskRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IOpenAIService _openAIService;

        public TaskController(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IJwtService jwtService, IOpenAIService openAIService)
        {
            TaskRepository = taskRepository;
            CategoryRepository = categoryRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _openAIService = openAIService;
        }

        [HttpGet("uid/{taskUid}")]
        [Authorize]
        public async Task<ActionResult> GetTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null)
            {
                return this.Unauthorized();
            }

            var options = new TaskOptions()
            {
                FilterUsers = new List<Guid> { jwtModel.Uid }
            };

            var task = await TaskRepository.GetAsync(taskUid, options);
            if (task == null) return NotFound();

            var response = await WrapperOutTask.From(task).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet("tasks")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            //filter only open
            var options = new TaskOptions()
            {
                FilterUsers = new List<Guid> { jwtModel.Uid }
            };

            var tasks = await this.TaskRepository.GetAllAsync(options);
            if (tasks == null)
            {
                return this.NotFound();
            }

            var response = await WrapperOutTask.From(tasks).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateTask([FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var wrapperModel = await wrapperInTask.Result();

            var category = await CreateCategoryIfNotExist(wrapperInTask);

            IUserModel user = await _userRepository.GetAsync(jwtModel.Uid);

            var task = await TaskRepository.CreatAsync(wrapperModel, user, category);

            var response = await WrapperOutTask.From(task).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        private async Task<ICategoryModel> CreateCategoryIfNotExist(WrapperInTask<TaskModel> wrapperInTask)
        {
            if (string.IsNullOrEmpty(wrapperInTask.CategoryName)) return null;

            var category = await CategoryRepository.GetAsync(wrapperInTask.CategoryName);
            if (category == null)
            {
                return await CategoryRepository.CreateAsync(wrapperInTask.CategoryName);
            }

            return category;
        }

        [HttpPut("uid/{taskUid}/update")]
        [Authorize]
        public async Task<ActionResult> UpdateTask(Guid taskUid, [FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await TaskRepository.GetAsync(taskUid, new TaskOptions { FilterUids = new List<Guid> { jwtModel.Uid } });
            if (task == null) return NotFound();

            var json = JsonSerializer.Serialize(wrapperInTask);

            var result = await TaskRepository.UpdateAsync(json, task).ConfigureAwait(false);
            if (result == null) return UnprocessableEntity();

            var response = await WrapperOutTask.From(result).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpPost("uid/{taskUid}/completed")]
        [Authorize]
        public async Task<ActionResult> CompleteTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await TaskRepository.GetAsync(taskUid, new TaskOptions { FilterUids = new List<Guid> { jwtModel.Uid } });
            if (task == null) return NotFound();

            var result = await TaskRepository.TaskCompletedAsync(task);
            if (result == null) return UnprocessableEntity();

            var response = await WrapperOutTask.From(result).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpDelete("uid/{taskUid}/delete")]
        [Authorize]
        public async Task<ActionResult> DeleteTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await TaskRepository.GetAsync(taskUid, new TaskOptions { FilterUids = new List<Guid> { jwtModel.Uid } });
            if (task == null) return NotFound();

            var result = await TaskRepository.DeleteAsync(task);
            if (result == false) return UnprocessableEntity();

            return Ok();
        }

        [HttpPost("interpret")]
        public async Task<IActionResult> InterpretTask([FromBody] WrapperInInterpretTask wrapperInInterpretTask)
        {
            var (isValid, errorMessage) = wrapperInInterpretTask.Validate();
            if (!isValid)
            {
                return BadRequest(new WrapperOutError(errorMessage));
            }

            var task = await _openAIService.InterpretTaskAsync(wrapperInInterpretTask.Message, "");
            if (task == null)
                return BadRequest("Não foi possível interpretar a mensagem.");

            return Ok(task);
        }
    }
}
