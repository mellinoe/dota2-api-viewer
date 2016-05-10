using System;
using System.CommandLine;
using System.Linq;

namespace DotaApiViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string command = null;
            string name = null;
            Language language = Language.English;

            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineCommand("hero", ref command, Strings.HeroCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));

                syntax.DefineCommand("item", ref command, Strings.ItemCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));
            });

            if (command == "hero")
            {
                HeroCommand(name, language);
            }
            else if (command == "item")
            {
                ItemCommand(name, language);
            }
            else
            {
                HeroCommand(name, language);
                ItemCommand(name, language);
            }
        }

        private static void ItemCommand(string name, Language language)
        {
            var itemQueryResult = ApiMethods.GetAllItems(ApiKey.UserKey, language).Execute().Result;
            Item[] items = itemQueryResult.Value.Items;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                var matchingItems = items.Where(i => i.LocalizedName == name);
                if (!matchingItems.Any())
                {
                    Console.WriteLine(string.Format(Strings.NoMatchingItems, name));
                }
                else
                {
                    foreach (var item in matchingItems)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }

        private static void HeroCommand(string name, Language language)
        {
            var heroQueryResult = ApiMethods.GetAllHeroes(ApiKey.UserKey, language).Execute().Result;
            Hero[] heroes = heroQueryResult.Value.Heroes;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var hero in heroes)
                {
                    Console.WriteLine(hero);
                }
            }
            else
            {
                var matchingHeroes = heroes.Where(h => h.LocalizedName == name);
                if (!matchingHeroes.Any())
                {
                    Console.WriteLine(string.Format(Strings.NoMatchingHeroes, name));
                }
                else
                {
                    foreach (var hero in matchingHeroes)
                    {
                        Console.WriteLine(hero);
                    }
                }
            }
        }

        private static Language ParseLanguage(string s) => (Language)Enum.Parse(typeof(Language), s);
    }

    public enum Language
    {
        English
    }
}
