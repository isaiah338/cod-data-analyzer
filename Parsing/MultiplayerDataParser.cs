using Microsoft.VisualBasic.FileIO;

namespace Parsing
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
                        : Array.Empty<string>();

        public string[] FilterCsvLine(string csvLine)
            => FilterCsvLine(csvLine.Split(CSV_LINE_DELIMITER));
        
        public string[][] ParseMatchDataCsv(string csvFilePath)
        {
            string[] selectedDataValues = new string[0];
            List<string[]> returnCsvDataValues = [];
            try
            {
                // verify file exists
                if (File.Exists(csvFilePath))
                {
                    Console.WriteLine("file exists");

                    // open file stream
                    using (TextFieldParser parser = new(csvFilePath))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(CSV_LINE_DELIMITER);
                        while (!parser.EndOfData)
                        {
                            string[] filteredCsvLineArr 
                                = FilterCsvLine(parser.ReadFields());
                            if (filteredCsvLineArr.Length > 0 )
                                returnCsvDataValues.Add(filteredCsvLineArr);
                        }
                    }
                }
                else
                {
                    throw new IOException("file not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Has Occured:\n" + e.Message);

            }

            return [.. returnCsvDataValues];
        }
        public string[] ParseMatchDataHtml()
        {
            return new string[0];
        }
    }
}
