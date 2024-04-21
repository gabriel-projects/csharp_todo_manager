using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Interfaces;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Extensions;
using App.GRRInnovations.TodoManager.Integration.TodoManager.Api.Enums;

namespace App.GRRInnovations.TodoManager.Integration.TodoManager.Api.ServicesConfiguration
{
    public abstract class ServiceBase : IServiceBase, IDisposable
    {
        static HttpClient Client;
        string Controller;
        string ApiAction;

        //
        const string BASE_URL = "https://bcfd-191-5-227-92.ngrok-free.app/api/";

        //todo: criar uma anotação para nao nulo ou vazio
        protected ServiceBase([NotNull]string controller)
        {
            Controller = controller;
            Configs(controller);
        }

        public Task<IServiceResult<TResult>> DeleteAsync<TResult>()
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<TResult>> GetAsync<TResult>(string actionApi)
        {
            ApiAction = actionApi;
            return await Command<TResult>(EHttpMethod.GET, null);
        }

        public Task<IServiceResult<TResult>> PostAsync<TData, TResult>(TData data)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<TResult>> PutAsync<TData, TResult>(TData data)
        {
            throw new NotImplementedException();
        }

        private async Task<IServiceResult<TResult>> Command<TResult>(EHttpMethod method, object input, bool useToken = true)
        {
            if (Client.BaseAddress.ToString() != BASE_URL)
            {
                CreateHttpClientAndConfigJson();
            }

            ServiceResult<TResult> result = new ServiceResult<TResult>();
            string actionName = string.Empty;

            //todo: verificar se podemos usar esse enum para os httpmethods
            //WebRequestMethods.Http

            try
            {
                //todo: buscar do sqlite
                //ResultCommunic<TokenResultVM> vToken = null;
                //if (useToken)
                //{
                //    vToken = (_TokenWebDbCommunic != null) ? await _TokenWebDbCommunic.GetTokenAsync(pRec) : null;
                //    if (!vToken.Sucesso())
                //    {
                //        vRetorno.Mensagem = vToken.Mensagem;
                //        vRetorno.Resultado = vToken.Resultado;
                //        return vRetorno;
                //    }
                //}

                if (Client.DefaultRequestHeaders.Authorization == null)
                {
                    Client.DefaultRequestHeaders.Add("authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IiIsInVzZXJfdWlkIjoiYzUxODI5ZGMtZTRmMi00MzNjLThjNDQtMDZiNWQ1ODAwNGJlIiwibmJmIjoxNzEzNjI3NDY5LCJleHAiOjE3MTQ0OTE0NjksImlhdCI6MTcxMzYyNzQ2OSwiaXNzIjoiaHR0cHM6Ly9hcGkudG9kby5tYW5hZ2VyLmNvbS5iciIsImF1ZCI6IkRlZmF1bHRBdWRpZW5jZSJ9.zhYzE7_usLRNtZiZQVU49nH_r7SFb70ECq1ErAU_nVQ");
                }

                HttpResponseMessage response = new();
                switch (method)
                {
                    //Referência: https://www.pluralsight.com/blog/software-development/intro-to-polly
                    case EHttpMethod.GET:
                        actionName = WebRequestMethods.Http.Get;
                        Uri url = MontaURI((string)input);
                        response = await Client.GetAsync(url);
                        break;
                    case EHttpMethod.DELETE:
                        actionName = HttpMethod.Delete.Method;
                        response = await Client.DeleteAsync(MontaURI((string)input));
                        break;
                    case EHttpMethod.POST:
                        actionName = "POST";
                        StringContent vContents = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                        Uri urlPost = MontaURI();
                        response = await Client.PostAsync(urlPost, vContents);
                        break;
                    case EHttpMethod.PUT:
                        break;
                    default:
                        break;
                }

                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    result.ResultType = EResult.ServidorNaoEncontrado;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) // Se na primeira tentativa não deu certo, solicita o token
                {
                    //return await _Command<TResult>(pAcao, pObjeto, true);
                }
                else if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        result.ResultType = EResult.NaoEncontrado;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.PartialContent)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<ServiceResult<TResult>>(json);
                        result.ResultType = EResult.ErroDeRegra;
                    }
                    else
                    {
                        var json = await response.Content?.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(json) == false)
                        {
                            result.Value = JsonConvert.DeserializeObject<TResult>(json);

                            result.ResultType = EResult.Sucess;
                        }
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    string vRes = response.Content.ReadAsStringAsync().Result;
                    if (vRes.Contains("html") || vRes.Contains("<!"))
                    {
                        result = new ServiceResult<TResult>() { Message = "Address not found", ResultType = EResult.Erro };
                    }
                    else
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        result = JsonConvert.DeserializeObject<ServiceResult<TResult>>(json);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    result = new ServiceResult<TResult>() { Message = "AppResources.SemAltorizacao", ResultType = EResult.Erro };
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<ServiceResult<TResult>>(json);
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = "";
                    try
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        message = JsonConvert.DeserializeObject<ServiceResult<TResult>>(json).Message;
                    }
                    catch { }
                    if (string.IsNullOrEmpty(message))
                    {
                        message = "AppResources.MensagemRetornouNula";
                    }
                    //CriarLog(Mensagem, (int)response.StatusCode, _UrlAtualCompleta, ActionName, response.Content.ReadAsStringAsync()?.Result);
                }
                response?.Dispose();
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is WebException)
                {
                    WebException vWebEx = (WebException)ex.InnerException;
                    switch (vWebEx.Status)
                    {
                        case WebExceptionStatus.ConnectFailure:
                        case WebExceptionStatus.Pending:
                        case WebExceptionStatus.RequestCanceled:
                        case WebExceptionStatus.SendFailure:
                            result.Message = vWebEx.GetException();
                            result.ResultType = EResult.SemInternet;
                            break;
                        case WebExceptionStatus.Success:
                            break;
                        case WebExceptionStatus.MessageLengthLimitExceeded:
                        case WebExceptionStatus.UnknownError:
                        default:
                            result.Message = ex.GetException();
                            break;
                    }
                }
                else
                {
                    result.Message = ex.GetException();
                }
                //CriarLog(vRetorno.Mensagem, 0, _UrlAtualCompleta, ActionName, null);
            }
            catch (WebException ex)
            {
                result.Message = ex.GetException();
                //CriarLog(vRetorno.Mensagem, 0, _UrlAtualCompleta, ActionName, null);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                //CriarLog(vEx.Message, 0, _UrlAtualCompleta, ActionName, null);
            }
            return result;
        }

        void Configs(string apiAction)
        {
            //_URL = string.IsNullOrEmpty(url) ? AppConfiguration.UrlBase : url;
            if (Client == null)
            {
                CreateHttpClientAndConfigJson();
            }
        }

        Uri MontaURI(string pParams = null)
        {
            var url = $"{BASE_URL.TrimEnd('/')}/{Controller}/{ApiAction}{(string.IsNullOrEmpty(pParams) ? "" : $"/{pParams}")}";
            return new Uri(url);
        }

        private void CreateHttpClientAndConfigJson()
        {
            Client = new HttpClient();
            //_Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentCulture.Name);
            Client.BaseAddress = new Uri(BASE_URL);
            //_Client.Timeout = new TimeSpan(0, 0, 10);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
