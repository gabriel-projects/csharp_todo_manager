using App.GRRInnovations.TodoManager.Interfaces.ApiTodoManagerCommunic;
using App.GRRInnovations.TodoManager.Interfaces.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Infrastructure.ApiCommunic
{
    public class ResultCommunic<T> : IResultCommunic<T>
    {
        public EResult ResultType { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
