using System.Collections.Generic;

namespace DotaApiViewer
{
    public class ApiMethods
    {
        public static ApiQuery<ResultHolder<HeroQueryResult>> GetAllHeroes(string apiKey, Language language)
        {
            return new ApiQuery<ResultHolder<HeroQueryResult>>(
                "IEconDOTA2_570/GetHeroes/v1", apiKey,
                new QueryParameter("language", language.ToQueryFormat()));
        }

        public static ApiQuery<ResultHolder<ItemQueryResult>> GetAllItems(string apiKey, Language language)
        {
            return new ApiQuery<ResultHolder<ItemQueryResult>>(
                "IEconDOTA2_570/GetGameItems/v1", apiKey,
                new QueryParameter("language", language.ToQueryFormat()));
        }

        public static ApiQuery<ResultHolder<MatchHistoryQueryResult>> GetMatchResults(string apiKey, MatchHistoryQueryOptions mhqo)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            if (mhqo.HeroID != -1)
            {
                parameters.Add(QueryParameter.Create("hero_id", (uint)mhqo.HeroID));
            }
            if (mhqo.GameMode != GameMode.Any)
            {
                parameters.Add(QueryParameter.Create("game_mode", (uint)mhqo.GameMode));
            }
            if (mhqo.SkillLevel != SkillLevel.Any)
            {
                parameters.Add(QueryParameter.Create("skill_level", (uint)mhqo.SkillLevel));
            }
            if (mhqo.MinDate != -1)
            {
                parameters.Add(QueryParameter.Create("date_min", (uint)mhqo.MinDate));
            }
            if (mhqo.MaxDate != -1)
            {
                parameters.Add(QueryParameter.Create("date_max", (uint)mhqo.MaxDate));
            }
            if (mhqo.MinPlayers != -1)
            {
                parameters.Add(QueryParameter.Create("min_players", (uint)mhqo.MinPlayers));
            }
            if (mhqo.AccountID != null)
            {
                parameters.Add(QueryParameter.Create("account_id", mhqo.AccountID));
            }
            if (mhqo.LeagueID != null)
            {
                parameters.Add(QueryParameter.Create("league_id", mhqo.LeagueID));
            }
            if (mhqo.StartAtMatchID != null)
            {
                parameters.Add(QueryParameter.Create("start_at_match_id", mhqo.StartAtMatchID));
            }
            if (mhqo.MatchesRequested != -1)
            {
                parameters.Add(QueryParameter.Create("matches_requested", mhqo.MatchesRequested));
            }
            if (mhqo.TournamentGamesOnly)
            {
                parameters.Add(new QueryParameter("tournament_games_only", mhqo.TournamentGamesOnly.ToString("1", "0")));
            }

            return new ApiQuery<ResultHolder<MatchHistoryQueryResult>>("IDOTA2Match_570/GetMatchHistory/v1", apiKey, parameters.ToArray());
        }

        public static ApiQuery<ResultHolder<MatchDetailsQueryResult>> GetMatchDetails(string apiKey, long matchID)
        {
            return new ApiQuery<ResultHolder<MatchDetailsQueryResult>>(
                "IDOTA2Match_570/GetMatchDetails/v1",
                apiKey,
                QueryParameter.Create("match_id", matchID));
        }

        public static ApiQuery<ResponseHolder<PlayerSummaryQueryResult>> GetSteamProfileSummaries(string apiKey, string steamIDs)
        {
            return new ApiQuery<ResponseHolder<PlayerSummaryQueryResult>>(
                "ISteamUser/GetPlayerSummaries/v0002",
                apiKey,
                new QueryParameter("steamids", steamIDs));
        }
    }


    public class ResultHolder<T>
    {
        public T Result { get; set; }
    }

    public class ResponseHolder<T>
    {
        public T Response { get; set; }
    }
}