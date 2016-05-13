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
            string heroID = null;
            Language language = Language.English;

            MatchHistoryQueryOptions mhqo = new MatchHistoryQueryOptions();
            string steamIDs = null;

            string matchID = null;

            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineCommand("hero", ref command, Strings.HeroCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("id|heroid", ref heroID, "Return information about the hero with this ID.");
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));

                syntax.DefineCommand("item", ref command, Strings.ItemCommandDescription);
                syntax.DefineOption("n|name", ref name, Strings.NameOptionDescription);
                syntax.DefineOption("l|language", ref language, ParseLanguage, string.Format(Strings.LanguageOptionDescription, language));

                syntax.DefineCommand("matchhistory", ref command, Strings.MatchHistoryCommandDescription);
                syntax.DefineOption("h|heroid", ref mhqo.HeroID, "Filter results to matches containing this hero ID. Use the \"hero\" command to enumerate hero IDs.");
                syntax.DefineOption("gamemode", ref mhqo.GameMode, ParseGameMode, "Filter matches to this game mode.");
                syntax.DefineOption("s|skilllevel", ref mhqo.SkillLevel, ParseSkillLevel, "Filter matches to this skill level.");
                syntax.DefineOption("mindate", ref mhqo.MinDate, "Filter matches to after this date.");
                syntax.DefineOption("maxdate", ref mhqo.MaxDate, "Filter matches to before this date.");
                syntax.DefineOption("minplayers", ref mhqo.MinPlayers, "Filter to matches with at least this many players.");
                syntax.DefineOption("a|accountid", ref mhqo.AccountID, "Filter matches to those containing this Steam account ID.");
                syntax.DefineOption("leagueid", ref mhqo.LeagueID, "Filter matches to those from this league.");
                syntax.DefineOption("startatmatchid", ref mhqo.StartAtMatchID, "Filter matches to those after this match ID.");
                syntax.DefineOption("n|count|matchesrequested", ref mhqo.MatchesRequested, "The number of matches to include.");
                syntax.DefineOption("t|tournamentgamesonly", ref mhqo.TournamentGamesOnly, "Filter to official tournament matches only.");

                syntax.DefineCommand("matchdetails", ref command, Strings.MatchDetailsCommandDescription);
                syntax.DefineOption("id|matchid", ref matchID, "The ID of the match to query.");

                syntax.DefineCommand("steamid", ref command, Strings.SteamIDCommandDescription);
                syntax.DefineOption("ids", ref steamIDs, "a comma-separated list of Steam IDs to query.");
            });

            if (command == "hero")
            {
                HeroCommand(name, heroID, language);
            }
            else if (command == "item")
            {
                ItemCommand(name, language);
            }
            else if (command == "matchhistory")
            {
                MatchHistoryCommand(mhqo);
            }
            else if (command == "matchdetails")
            {
                MatchDetailsCommand(matchID);
            }
            else if (command == "steamid")
            {
                SteamIDCommand(steamIDs);
            }
        }

        private static void MatchDetailsCommand(string matchIDString)
        {
            long matchID;
            if (!long.TryParse(matchIDString, out matchID))
            {
                throw new InvalidOperationException("Invalid match ID: " + matchIDString);
            }

            var matchDetailsQueryResult = ApiMethods.GetMatchDetails(ApiKey.UserKey, matchID).Execute().Result;
            var result = matchDetailsQueryResult.Value.Result;

            Console.WriteLine($"Match {result.MatchID}, {result.LobbyType}, {result.GameMode}");
            var steamIDs = string.Join(",", result.PlayerResults.Select(pr => Convert32to64BitID((ulong)pr.SteamID)));
            var playerQuery = ApiMethods.GetSteamProfileSummaries(ApiKey.UserKey, steamIDs).Execute().Result.Value.Response;
            var players = playerQuery.Players;

            foreach (var player in result.PlayerResults)
            {
                PrintPlayerResult(
                    player,
                    playerQuery.Players.SingleOrDefault(p => p.SteamID == Convert32to64BitID((ulong)player.SteamID))?.PersonaName);
                Console.WriteLine();
            }
        }

        private static void PrintPlayerResult(PlayerResult player, string accountName)
        {
            string dispName = accountName != null ? accountName : player.SteamID.ToString();
            Console.WriteLine($" - {dispName}, Level {player.Level} {HeroCache.GetHeroByID(player.HeroID).LocalizedName}, {new PlayerSlot(player.PlayerSlot)}");
            Console.WriteLine($"   - GPM:{player.GoldPerMinute}, XPM:{player.ExperiencePerMinute}, Gold:{player.Gold}");
            Console.WriteLine($"   - KDA:{player.Kills}/{player.Deaths}/{player.Assists}, LHD:{player.LastHits}/{player.Denies}, HD/TD/HH{player.HeroDamage}/{player.TowerDamage}/{player.HeroHealing}");
            Console.WriteLine($"   - Items:");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item0)?.LocalizedName}");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item1)?.LocalizedName}");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item2)?.LocalizedName}");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item3)?.LocalizedName}");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item4)?.LocalizedName}");
            Console.WriteLine($"     * {ItemCache.GetItemByID(player.Item5)?.LocalizedName}");
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

        private static DateTime ConvertUnixTime(uint time)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(time).ToLocalTime();
        }

        private static void PrintPlayerSummary(SteamPlayerSummary player)
        {
            var lastLogOffLocal = ConvertUnixTime(player.LastLogOffTime);
            Console.WriteLine($"{player.PersonaName} [{player.SteamID}], {player.ProfileUrl}, Last Log Off: {lastLogOffLocal}");
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

            Console.WriteLine($"{ms.LobbyType} ID:{ms.MatchID} [{ms.Players.Length} players] Start time: {ConvertUnixTime(ms.StartTime)}");
            foreach (var player in ms.Players)
            {
                string personaName = players.SingleOrDefault(p => p.SteamID == Convert32to64BitID(player.SteamID))?.PersonaName;
                Console.WriteLine(
                    $"  * {personaName ?? "<unknown>"} ({player.SteamID}), Hero:{HeroCache.GetHeroByID(player.HeroID).LocalizedName} ({player.HeroID})");
            }
        }

        private const ulong conversionConstant = 76561197960265728u;

        private static string Convert64to32BitID(string accountID)
        {
            ulong id64 = ulong.Parse(accountID);
            ulong id32 = (ulong)(id64 - conversionConstant);
            return id32.ToString();
        }

        private static string Convert32to64BitID(string accountID)
        {
            ulong id32 = ulong.Parse(accountID);
            return Convert32to64BitID(id32);
        }

        private static string Convert32to64BitID(ulong id32)
        {
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

        private static void HeroCommand(string name, string heroIDString, Language language)
        {
            if (name != null && heroIDString != null)
            {
                Console.WriteLine("Name and HeroID are both specified. Ignoring HeroID.");
            }

            int heroID = -1;
            if (!string.IsNullOrEmpty(heroIDString) && !int.TryParse(heroIDString, out heroID))
            {
                throw new InvalidOperationException("Couldn't parse hero ID: " + heroIDString);
            }

            var heroQueryResult = ApiMethods.GetAllHeroes(ApiKey.UserKey, language).Execute().Result;
            Hero[] heroes = heroQueryResult.Value.Result.Heroes;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(heroIDString))
            {
                Func<Hero, bool> filter;
                if (!string.IsNullOrEmpty(name))
                {
                    filter = h => h.LocalizedName == name;
                }
                else
                {
                    filter = h => h.ID == heroID;
                }
                var matchingHeroes = heroes.Where(filter);
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
            else
            {
                foreach (var hero in heroes)
                {
                    Console.WriteLine(hero);
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
