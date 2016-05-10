using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiViewer
{
    public class ApiMethods
    {
        public static ApiQuery<HeroQueryResult> GetAllHeroes(string apiKey)
        {
            return new ApiQuery<HeroQueryResult>(
                "IEconDOTA2_570/GetHeroes/v1", apiKey,
                new QueryParameter("language", "en"));
        }

        public static ApiQuery<ItemQueryResult> GetAllItems(string apiKey)
        {
            return new ApiQuery<ItemQueryResult>(
                "IEconDOTA2_570/GetGameItems/v1", apiKey,
                new QueryParameter("language", "en"));
        }
    }

    public class ApiResult<T>
    {
        private readonly HttpResponseMessage _response;
        private readonly Lazy<T> _lazyValue;

        public T Value
        {
            get
            {
                return _lazyValue.Value;
            }
        }

        private T DeserializeResult()
        {
            if (!_response.IsSuccessStatusCode)
            {
                string errorMessage = _response.Content.ReadAsStringAsync().Result;
                throw new DotApiViewerException(string.Format(Strings.RequestFailed, errorMessage));
            }

            JsonSerializer serializer = JsonSerializer.CreateDefault();

            using (var jsonReader = new JsonTextReader(new StringReader(_response.Content.ReadAsStringAsync().Result)))
            {
                return serializer.Deserialize<ApiResultHolder>(jsonReader).Result;
            }
        }

        public ApiResult(HttpResponseMessage response)
        {
            _response = response;
            _lazyValue = new Lazy<T>(DeserializeResult);
        }

        private class ApiResultHolder
        {
            public T Result { get; set; }
        }
    }

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

    public struct QueryParameter
    {
        public readonly string Key;
        public readonly string Value;

        public QueryParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
