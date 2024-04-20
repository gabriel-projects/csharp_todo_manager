using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.TodoManager.Infrastructure.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetException(this Exception exception)
        {
            if (exception is WebException)
            {
                return GetMessageEx(exception);
            }


            if (string.IsNullOrEmpty(exception.Message))
            {
                return exception.InnerException.Message;
            }
            else
            {
                if (exception.Message.Contains("Socket closed"))
                {
                    return "AppResources.ServidorForaDoAr";
                }
                return exception.Message;
            }
        }

        private static string GetMessageEx(Exception pException)
        {
            //http://www.gnu.org/software/dotgnu/pnetlib-doc/System/Net/WebExceptionStatus.html
            switch ((int)(pException as WebException).Status)
            {
                case 2:
                    return "AppResources.FalhaConexao";
                case 8:
                    return "AppResources.ConexaoFechada";
                case 12:
                    return "AppResources.FalhaKeep";
                case 1:
                    return" AppResources.URLInvalida";
                default:
                    return (pException as WebException).Message;
            }
        }
    }
}
