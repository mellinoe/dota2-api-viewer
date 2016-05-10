using System;

namespace DotaApiViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var heroQueryResult = ApiMethods.GetAllHeroes(ApiKey.UserKey).Execute().Result;
            var itemQueryResult = ApiMethods.GetAllItems(ApiKey.UserKey).Execute().Result;

            Hero[] heroes = heroQueryResult.Value.Heroes;
            Item[] items = itemQueryResult.Value.Items;

            foreach (var hero in heroes)
            {
                Console.WriteLine(hero);
            }

            int width = Console.WindowWidth;
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
