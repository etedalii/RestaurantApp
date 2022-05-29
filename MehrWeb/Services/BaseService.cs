using MehrWeb.Models;
using MehrWeb.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace MehrWeb.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest api)
        {
            try
            {
                var client = httpClient.CreateClient("MangoApi");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.Add("Accept", "application/json");
                httpRequestMessage.RequestUri = new Uri(api.Url);
                client.DefaultRequestHeaders.Clear();
                if(api.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(api.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? httpResponse = null;
                switch (api.ApiType)
                {
                    case SD.ApiType.GET:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                httpResponse = await client.SendAsync(httpRequestMessage);

                var apiContent = await httpResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
