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

            MatchHistoryQueryOptions mhqo = new MatchHistoryQueryOptions();
            string steamIDs = null;

            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineCommand("hero", ref command, Strings.HeroCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));

                syntax.DefineCommand("item", ref command, Strings.ItemCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));

                syntax.DefineCommand("matchhistory", ref command, Strings.MatchHistoryCommandDescription);
                syntax.DefineOption("heroid", ref mhqo.HeroID, "Filter results to matches containing this hero ID. Use the \"hero\" command to enumerate hero IDs.");
                syntax.DefineOption("gamemode", ref mhqo.GameMode, ParseGameMode, "Filter matches to this game mode.");
                syntax.DefineOption("skilllevel", ref mhqo.SkillLevel, ParseSkillLevel, "Filter matches to this skill level.");
                syntax.DefineOption("mindate", ref mhqo.MinDate, "Filter matches to after this date.");
                syntax.DefineOption("maxdate", ref mhqo.MaxDate, "Filter matches to before this date.");
                syntax.DefineOption("minplayers", ref mhqo.MinPlayers, "Filter to matches with at least this many players.");
                syntax.DefineOption("accountid", ref mhqo.AccountID, "Filter matches to those containing this Steam account ID.");
                syntax.DefineOption("leagueid", ref mhqo.LeagueID, "Filter matches to those from this league.");
                syntax.DefineOption("startatmatchid", ref mhqo.StartAtMatchID, "Filter matches to those after this match ID.");
                syntax.DefineOption("matchesrequested", ref mhqo.MatchesRequested, "The number of matches to include.");
                syntax.DefineOption("tournamentgamesonly", ref mhqo.TournamentGamesOnly, "Filter to official tournament matches only.");

                syntax.DefineCommand("steamid", ref command, Strings.SteamIDCommandDescription);
                syntax.DefineOption("ids", ref steamIDs, "a comma-separated list of Steam IDs to query.");
            });

            if (command == "hero")
            {
                HeroCommand(name, language);
            }
            else if (command == "item")
            {
                ItemCommand(name, language);
            }
            else if (command == "matchhistory")
            {
                MatchHistoryCommand(mhqo);
            }
            else if (command == "steamid")
            {
                SteamIDCommand(steamIDs);
            }
        }

        private static void SteamIDCommand(string steamIDs)
        {
            var playerSummaryQueryResult = ApiMethods.GetSteamProfileSummaries(ApiKey.UserKey, steamIDs).Execute().Result;
            var players = playerSummaryQueryResult.Value.Response.Players;
            foreach (var player in players)
            {
                PrintPlayerSummary(player);
            }
        }

        private static void PrintPlayerSummary(SteamPlayerSummary player)
        {
            Console.WriteLine($"{player.PersonaName} [{player.SteamID}], {player.ProfileUrl}, Last Online: {player.LastLogOffTime}");
        }

        private static void MatchHistoryCommand(MatchHistoryQueryOptions mhqo)
        {
            var queryResult = ApiMethods.GetMatchResults(ApiKey.UserKey, mhqo).Execute().Result;
            MatchHistoryQueryResult result = queryResult.Value.Result;

            Console.WriteLine($"Retrieved {result.NumResults} matches. {result.ResultsRemaining} remaining.");
            foreach (MatchSummary ms in result.Matches)
            {
                PrintMatchSummary(ms);
                Console.WriteLine();
            }
        }

        private static void PrintMatchSummary(MatchSummary ms)
        {
            var steamIDs = string.Join(",", ms.Players.Select(p => Convert32to64BitID(p.SteamID)));
            var playerQuery = ApiMethods.GetSteamProfileSummaries(ApiKey.UserKey, steamIDs).Execute().Result.Value.Response;
            var players = playerQuery.Players;

            Console.WriteLine($"{ms.LobbyType} ({ms.MatchID}) [{ms.Players.Length} players] Start time: {ms.StartTime}");
            foreach (var player in ms.Players)
            {
                string personaName = players.SingleOrDefault(p => p.SteamID == Convert32to64BitID(player.SteamID))?.PersonaName;
                Console.WriteLine(
                    $"  * {personaName ?? "<unknown>"} ({player.SteamID}), Hero:{HeroCache.GetHeroByID(player.HeroID).LocalizedName}({player.HeroID})");
            }
        }

        private const ulong conversionConstant = 76561198121710206u - 161444478;

        private static string Convert64to32BitID(string accountID)
        {
            ulong id64 = ulong.Parse(accountID);
            ulong id32 = (ulong)(id64 - conversionConstant);
            return id32.ToString();
        }

        private static string Convert32to64BitID(string accountID)
        {
            ulong id32 = ulong.Parse(accountID);
            ulong id64 = id32 + conversionConstant;
            return id64.ToString();
        }

        private static void ItemCommand(string name, Language language)
        {
            var itemQueryResult = ApiMethods.GetAllItems(ApiKey.UserKey, language).Execute().Result;
            Item[] items = itemQueryResult.Value.Result.Items;
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
            Hero[] heroes = heroQueryResult.Value.Result.Heroes;
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
        private static GameMode ParseGameMode(string s) => (GameMode)Enum.Parse(typeof(GameMode), s);
        private static SkillLevel ParseSkillLevel(string s) => (SkillLevel)Enum.Parse(typeof(SkillLevel), s);
    }

    public enum Language
    {
        English
    }
}
