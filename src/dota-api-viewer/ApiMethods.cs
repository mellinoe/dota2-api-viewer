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
}
