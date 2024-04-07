using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories
{
    [Ioc(Interface = typeof(UserRepository))]
    internal class UserRepository : IUserRepository
    {
    }
}
