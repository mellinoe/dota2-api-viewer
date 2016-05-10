using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiViewer
{
    public class ApiQuery<T>
    {
        private readonly string _apiKey;
        private readonly string _url;
        private readonly QueryParameter[] _parameters;
        private readonly HttpClient _httpClient = new HttpClient();

        public ApiQuery(string url, string apiKey, params QueryParameter[] parameters)
        {
            _url = url;
            _apiKey = apiKey;
            _parameters = parameters;
        }

        public async Task<ApiResult<T>> Execute()
        {
            string fullUrl = GetFullQueryUrl(_url, _apiKey, _parameters);
            HttpResponseMessage response = await _httpClient.GetAsync(fullUrl);
            return new ApiResult<T>(response);
        }

        private static string GetFullQueryUrl(string url, string apiKey, QueryParameter[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var param in parameters)
            {
                sb.Append($"&{param.Key}={param.Value}");
            }

            return $"https://api.steampowered.com/{url}?key={apiKey}{sb.ToString()}";
        }
    }
}
