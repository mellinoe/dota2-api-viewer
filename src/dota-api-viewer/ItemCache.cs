using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaApiViewer
{
    public static class ItemCache
    {
        private static object s_lock = new object();
        private static Dictionary<int, Item> s_items;
        private static bool s_initialized;

        private static void EnsureInitialized()
        {
            if (!s_initialized)
            {
                lock (s_lock)
                {
                    var items = ApiMethods.GetAllItems(ApiKey.UserKey, Language.English).Execute().Result.Value.Result.Items;
                    s_items = new Dictionary<int, Item>(items.Length);
                    foreach (var hero in items)
                    {
                        s_items.Add(hero.ID, hero);
                    }
                }

                s_initialized = true;
            }
        }

        public static Item GetItemByID(int id)
        {
            EnsureInitialized();

            Item i = null;
            s_items.TryGetValue(id, out i);
            return i;
        }
    }
}
