using System.Linq;
using cod_data_analyzer.Models;
using cod_data_analyzer.Services.Util;
using Microsoft.VisualBasic.FileIO;

namespace cod_data_analyzer.Services
{
    public class MultiplayerDataParser
    {
        public static readonly string[] DATA_COLUMN_HEADERS = new[]
        {
            "UTC Timestamp", // index 0
            "Game Type", // index 3
            "Match Start Timestamp", // index 5
            "Match End Timestamp", // index 6
            "Map", // index 7
            "Match Outcome", // index 9
            "Skill", // index 13
            "Score", // index 14
            "Shots", // index 15
            "Hits", // index 16
            "Assists", // index 17
            "Longest Streak", // index 18
            "Kills", // index 19
            "Deaths", // index 20
            "Headshots", // index 21
            "Executions", // index 22
            "Suicides", // index 23
            "Damage Done", // index 24
            "Damage Taken", // index 25
            "Percentage Of Time Moving", // index 31
            "Total XP", // index 32
            "Score XP", // index 33
            "Challenge XP", // index 34
            "Match XP", // index 35
            "Medal XP", // index 36
            "Bonus XP", // index 37
            "Misc XP", // index 38
            "Accolade XP", // index 39
            "Weapon XP", // index 40
            "Operator XP", // index 41
            "Clan XP", // index 42
            "Battle Pass XP", // index 43
            "Rank at Start", // index 44
            "Rank at End", // index 45
            "Lifetime Wins", // index 55
            "Lifetime Losses", // index 56
            "Lifetime Kills", // index 57
            "Lifetime Deaths", // index 58
            "Lifetime Hits", // index 59
            "Lifetime Misses" // index 60
        };
        // 0 - UTC Timestamp, 3 - Game Type, 5 - Match Start Timestamp, 6 - Match End Timestamp, 7 - Map, 9 - Match Outcome, 13 - Skill, 14 - Score, 15 - Shots, 16 - Hits, 17 - Assists, 18 - Longest Streak, 19 - Kills, 20 - Deaths, 21 - Headshots, 22 - Executions, 23 - Suicides, 24 - Damage Done, 25 - Damage Taken, 31 - Percentage Of Time Moving, 32 - Total XP, 33 - Score XP, 34 - Challenge XP, 35 - Match XP, 36 - Medal XP, 37 - Bonus XP, 38 - Misc XP, 39 - Accolade XP, 40 - Weapon XP, 41 - Operator XP, 42 - Clan XP, 43 - Battle Pass XP, 44 - Rank at Start, 45 - Rank at End, 55 - Lifetime Wins, 56 - Lifetime Losses, 57 - Lifetime Kills, 58 - Lifetime Deaths, 59 - Lifetime Hits, 60 - Lifetime Misses

        private static readonly int[] CSV_COLUMN_INDEX_SELECTION =
            {
                0, 3, 5, 6, 7, 9, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 25, 31, 32,
                33, 34, 35, 36, 37, 38, 39, 40, 41, 42,
                43, 44, 45, 55, 56, 57, 58, 59, 60
            };
        public static readonly int DATA_COLUMN_COUNT = 62;
        private static readonly string CSV_LINE_DELIMITER = ",";
        public List<Match> GetMatchDataFromCsvList(
                            List<Map> maps, 
                            List<GameMode> gameModes, 
                            List<string[]> filteredCsv)
        {
            List<Match> matches = [];
            List<GameMode> coreGameModes = [.. gameModes.Where(gm => gm.GameTypeId == 1)];
            List<GameMode> partyGameModes = [.. gameModes.Where(gm => gm.GameTypeId == 3)];


            // create models from csv data
            foreach (string[] matchInput in filteredCsv)
            {
                // save upload map / mode string
                string inputMap = StringUtil.Normalize(matchInput[4]);
                string inputMode = StringUtil.Normalize(matchInput[1]);

                // save match time info
                DateTime matchStart = DateTime.Parse(matchInput[2]);
                DateTime matchEnd = DateTime.Parse(matchInput[3]);
                int matchLength = MathUtil.SecondsElapsedInt(matchStart, matchEnd);

                // find map id of map string
                int mapId = maps.Find(
                    m => StringUtil.Normalize(m.MapName) == inputMap
                ).MapId;

                // find game type id & game title id
                /// note: currently cant differentiate 
                ///   between modes besides party | main
                int gameTypeId;
                if (coreGameModes.Contains(
                    gameModes.Find(
                        gm => StringUtil.Normalize(
                            gm.GameTitle.GameTitleName) == inputMode)))
                {
                    gameTypeId = 1;
                }
                else
                {
                    gameTypeId = 3;
                }

                GameMode gameMode = gameModes.Find(
                    gm => gm.GameTypeId == gameTypeId
                          && StringUtil.Normalize(gm.GameTitle?.GameTitleName ?? "") == inputMode);


                // maps.Find(m => m.MapId == mapId)
                // GameMode = gameModes.Find(gm => gm.GameModeId == gameModeId)
                // add match object based off csv line
                // to matches list
                matches.Add(new Match
                {
                    MatchStart = matchStart,
                    MatchEnd = matchEnd,
                    MatchWin = matchInput[5].ToLower() == "win",
                    MatchLength = matchLength,
                    MapId = mapId,

                    GameModeId = gameMode.GameModeId,

                    LifetimeStats = new LifetimeStats
                    {
                        LifetimeWins = int.Parse(matchInput[34]),
                        LifetimeLosses = int.Parse(matchInput[35]),
                        LifetimeKills = int.Parse(matchInput[36]),
                        LifetimeDeaths = int.Parse(matchInput[37]),
                        LifetimeHits = int.Parse(matchInput[38]),
                        LifetimeMisses = int.Parse(matchInput[39])
                    },
                    PlayerStats = new PlayerStats
                    {
                        Skill = int.Parse(matchInput[6]),
                        Score = int.Parse(matchInput[7]),
                        Shots = int.Parse(matchInput[8]),
                        Hits = int.Parse(matchInput[9]),
                        Assists = int.Parse(matchInput[10]),
                        LongestStreak = int.Parse(matchInput[11]),
                        Kills = int.Parse(matchInput[12]),
                        Deaths = int.Parse(matchInput[13]),
                        Headshots = int.Parse(matchInput[14]),
                        Executions = int.Parse(matchInput[15]),
                        Suicides = int.Parse(matchInput[16]),
                        DamageDone = int.Parse(matchInput[17]),
                        PercentTimeMoving = double.Parse(matchInput[19].TrimEnd('%')),
                    },
                    XpStats = new XpStats
                    {
                        TotalXp = int.Parse(matchInput[20]),
                        ScoreXp = int.Parse(matchInput[21]),
                        ChallengeXp = int.Parse(matchInput[22]),
                        MatchXp = int.Parse(matchInput[23]),
                        MiscXp = int.Parse(matchInput[26]),
                        AccoladeXp = int.Parse(matchInput[27]),
                        WeaponXp = int.Parse(matchInput[28]),
                        OperatorXp = int.Parse(matchInput[29]),
                        BattlepassXp = int.Parse(matchInput[31]),
                        RankStart = int.Parse(matchInput[32]),
                        RankEnd = int.Parse(matchInput[33])
                    }
                });

            }
            return matches;
        }

        public async Task<List<string[]>> FilterCsvFromFileUpload(IFormFile file)
        {
            List<string[]>? filteredCsvList = [];

            // open file in reader
            using var reader = new StreamReader(file.OpenReadStream());

            // read through uploaded file
            while (!reader.EndOfStream)
            {
                // read line and save filtered output
                string[] filteredLineArr = FilterCsvLine(await reader.ReadLineAsync());

                // verify column count & data isnt header data
                // then add to data list
                //
                // checking for header data involves looking at first index, if it matches 
                // given string, assume line is header line
                if (filteredLineArr.Length > 0 && !filteredLineArr.Contains("UTC Timestamp"))
                    filteredCsvList.Add(filteredLineArr);
            }
            return filteredCsvList;
        }

        // convert ints from selected indexs list
        // to index positions in csv line
        // 
        // select
        // i = current number in list of selected indexes
        // if i is greater than or equal to 0 & less than csv line length
        // then select the index equal to i from csv line array
        // 
        // after, convert new collection to array
        //
        //
        // if length of split array does not match excpected, empty string array returned
        // can check for empty string array to handle accordingly
        public string[] FilterCsvLine(string[] splitLineArr)
            => splitLineArr.Length == DATA_COLUMN_COUNT
                        ? CSV_COLUMN_INDEX_SELECTION.Select(i
                            => i >= 0 && i < splitLineArr.Length
                                ? splitLineArr[i].Trim()
                                : $"[Invalid Index: {i}]"
                            ).ToArray()
                        : [];

        public string[] FilterCsvLine(string csvLine)
            => FilterCsvLine(csvLine.Split(CSV_LINE_DELIMITER));
        
        public string[] ParseMatchDataHtml()
        {
            return [];
        }
    }
}
