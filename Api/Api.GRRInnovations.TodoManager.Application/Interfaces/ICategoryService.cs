using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Domain.Models;
using Api.GRRInnovations.TodoManager.Domain.Wrappers.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ICategoryModel> CreateCategoryIfNotExistAsync(string? categoryName);
    }
}
