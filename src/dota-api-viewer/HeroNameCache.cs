using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaApiViewer
{
    public static class HeroCache
    {
        private static object s_lock = new object();
        private static Dictionary<int, Hero> s_heroes;
        private static bool s_initialized;

        private static void EnsureInitialized()
        {
            if (!s_initialized)
            {
                lock (s_lock)
                {
                    var heroes = ApiMethods.GetAllHeroes(ApiKey.UserKey, Language.English).Execute().Result.Value.Result.Heroes;
                    s_heroes = new Dictionary<int, Hero>(heroes.Length);
                    foreach (var hero in heroes)
                    {
                        s_heroes.Add(hero.ID, hero);
                    }
                }

                s_initialized = true;
            }
        }

        public static Hero GetHeroByID(int id)
        {
            EnsureInitialized();

            return s_heroes[id];
        }
    }
}
