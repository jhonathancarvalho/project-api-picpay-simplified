using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PicPaySimplified.Models.Enum;

namespace PicPaySimplified.Services.Authorizer
{
    public class AuthorizerService : IAuthorizerService
    {
        private readonly HttpClient _httpClient;

        private const string Url = "https://util.devi.tools/api/v2/authorize";

        public AuthorizerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AuthorizeAsync()
        {
            var response = await _httpClient.GetAsync(Url);

            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Resposta da API:");
            Console.WriteLine(content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<ApiResponse>(content, options);

            return result?.Status == "success" && result.Data?.Authorization == true;
        }


        private class ApiResponse
        {
            public string Status { get; set; }
            public DataResponse Data { get; set; }
        }

        private class DataResponse
        {
            public bool Authorization { get; set; }
        }
    }
}
