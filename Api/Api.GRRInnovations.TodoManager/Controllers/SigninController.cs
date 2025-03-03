﻿using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.Out;
using Api.GRRInnovations.TodoManager.Infrastructure.Authentication;
using Api.GRRInnovations.TodoManager.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Api.GRRInnovations.TodoManager.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly UserService UserService;

        public SigninController(UserService userService)
        {
            this.UserService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<WrapperOutJwtResult>> SigninUid([FromBody] WrapperInLogin wrapperInLogin)
        {
            if (string.IsNullOrEmpty(wrapperInLogin.Login) || string.IsNullOrEmpty(wrapperInLogin.Password)) return new BadRequestObjectResult(new WrapperOutError { Title = "Dados inválidos." });

            var remoteUser = await this.UserService.Validade(wrapperInLogin.Login, wrapperInLogin.Password).ConfigureAwait(false);
            if (remoteUser == null) return new UnauthorizedResult();

            var response = await JwtService.JwtResult(remoteUser).ConfigureAwait(false);

            return response;
        }
    }
}
