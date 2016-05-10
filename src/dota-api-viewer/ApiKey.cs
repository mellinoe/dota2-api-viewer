using System;
using System.IO;

namespace DotaApiViewer
{
    public static class ApiKey
    {
        private const string FallbackKeyLocation = "apikey.txt";
        private static string s_apiKey;

        private static string LoadApiKey()
        {
            string key = Environment.GetEnvironmentVariable("DOTA_API_VIEWER_KEY");
            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }

            string keyLocation = Environment.GetEnvironmentVariable("DOTA_API_VIEWER_KEYFILE");
            if (string.IsNullOrEmpty(keyLocation))
            {
                keyLocation = Path.Combine(AppContext.BaseDirectory, FallbackKeyLocation);
            }

            if (!File.Exists(keyLocation))
            {
                throw new DotApiViewerException(string.Format(Strings.KeyFileMissing, FallbackKeyLocation));
            }

            return File.ReadAllText(keyLocation);
        }

        public static string UserKey
        {
            get
            {
                if (s_apiKey == null)
                {
                    s_apiKey = LoadApiKey();
                }

                return s_apiKey;
            }
        }
    }
}
