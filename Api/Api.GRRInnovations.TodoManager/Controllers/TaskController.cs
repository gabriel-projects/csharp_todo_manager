using Api.GRRInnovations.TodoManager.Application.Interfaces;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Extensions;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Api.GRRInnovations.TodoManager.Infrastructure.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userRepository;

        public TaskController(IOpenAIService openAIService, ITaskService taskService, ICategoryService categoryService, IUserService userRepository)
        {
            _openAIService = openAIService;
            _taskService = taskService;
            _categoryService = categoryService;
            _userRepository = userRepository;
        }

        [HttpGet("uid/{taskUid}")]
        [Authorize]
        public async Task<ActionResult> GetTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null)
            {
                return Unauthorized();
            }

            var task = await _taskService.GetAsync(taskUid);
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

            var options = new TaskOptions()
            {
                FilterUsers = new List<Guid> { jwtModel.Uid },
            };

            var tasks = await _taskService.GetAllAsync(options);
            if (tasks == null)
            {
                return NotFound();
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

            var taskModel = await wrapperInTask.Result();
            if (taskModel == null) return BadRequest();

            var inCategory = await _categoryService.CreateCategoryIfNotExistAsync(taskModel?.Category?.Name);

            IUserModel inUser = await _userRepository.GetAsync(jwtModel.Uid);

            var task = await _taskService.CreatAsync(taskModel, inUser, inCategory);

            var response = await WrapperOutTask.From(task).ConfigureAwait(false);
            return new OkObjectResult(response);
        }


        [HttpPut("uid/{taskUid}/update")]
        [Authorize]
        public async Task<ActionResult> UpdateTask(Guid taskUid, [FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await _taskService.GetAsync(taskUid);
            if (task == null) return NotFound();

            var result = await _taskService.UpdateAsync(json, task).ConfigureAwait(false);
            if (result == null) return UnprocessableEntity();

            var response = await WrapperOutTask.From(result).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpPost("uid/{taskUid}/complete")]
        [Authorize]
        public async Task<ActionResult> CompleteTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await _taskService.GetAsync(taskUid);
            if (task == null) return NotFound();

            var result = await _taskService.TaskCompletedAsync(task);
            if (result == null) return UnprocessableEntity();

            var response = await WrapperOutTask.From(result).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpDelete("uid/{taskUid}")]
        [Authorize]
        public async Task<ActionResult> DeleteTask(Guid taskUid)
        {
            var jwtModel = await HttpContext.JwtInfo();
            if (jwtModel == null) return Unauthorized();

            var task = await _taskService.GetAsync(taskUid);
            if (task == null) return NotFound();

            var result = await _taskService.DeleteAsync(task);
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
