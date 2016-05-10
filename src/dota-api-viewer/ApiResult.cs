using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;

namespace DotaApiViewer
{

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
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        public ApiResult(HttpResponseMessage response)
        {
            _response = response;
            _lazyValue = new Lazy<T>(DeserializeResult);
        }
    }
}
