using cod_data_analyzer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cod_data_analyzer.Data
{
    public class MultiplayerDataDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        private string _connectionString;
        public MultiplayerDataDatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MultiplayerDataDatabaseContext(DbContextOptions<MultiplayerDataDatabaseContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //// GAMEMODE JOIN TABLE
            //// =================
            // This sets up the primary key of the JOIN table 
            modelBuilder.Entity<GameMode>()
                .HasKey(gm => new { gm.GameModeId });

            // This sets up the foreign in the JOIN table
            //  that relates to the GameType table.
            modelBuilder.Entity<GameMode>()
                .HasOne(gm => gm.GameType)
                .WithMany(gt => gt.GameModes)
                .HasForeignKey(gm => gm.GameTypeId);

            // This sets up the foreign key in the JOIN table
            //  that relates to the GameTitle table.
            modelBuilder.Entity<GameMode>()
                .HasOne(gm => gm.GameTitle)
                .WithMany(gt => gt.GameModes)
                .HasForeignKey(gm => gm.GameTitleId);

            //// ANALYZER SETTINGS MODE JOIN TABLE
            //// =================
            //modelBuilder.Entity<AnalyzerSettings>()
            //    .HasKey(asm => asm.AnalyzerSettingsId);

            //modelBuilder.Entity<AnalyzerSettings>()
            //    .HasOne(asm => asm.AnalyzerSettings)
            //    .WithMany(a => a.AnalyzerSettingsModes)
            //    .HasForeignKey(asm => asm.AnalyzerSettingsId);

            //modelBuilder.Entity<AnalyzerSettingsMode>()
            //    .HasOne(asm => asm.GameMode)
            //    .WithMany(gm => gm.AnalyzerSettingsModes)
            //    .HasForeignKey(asm => asm.GameModeId);



            //// ANALYZER SETTINGS MAP JOIN TABLE
            //// =================
        }


        // create db sets for table interaction
        public DbSet<LifetimeStats> LifetimeStats { get; set; }
        public DbSet<Map> Map { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<XpStats> XpStats { get; set; }
        public DbSet<GameMode> GameMode { get; set; }
        public DbSet<GameType> GameType{ get; set; }
        public DbSet<GameTitle> GameTitle{ get; set; }
        //public DbSet<AnalyzerSettings> AnalyzerSettings { get; set; }
        //public DbSet<AnalyzerSettingsMode> AnalyzerSettingsMode { get; set; }
        //public DbSet<AnalyzerSettingsMap> AnalyzerSettingsMap { get; set; }
        public void DeleteMap(int id)
            => Map.Remove(Map.Where(m => m.MapId == id).First());
        public void UpdateMap(Map map)
            => Map.Update(map);
        public bool MapExists(int id)
            => Map.Where(m => m.MapId == id).ToList().Count>0;
        public Map GetMapById(int id)
            => Map.Where(m => m.MapId == id).First();


        public GameMode GetGameModeById(int id)
            => GameMode.Where(gm => gm.GameModeId == id).First();
        public List<GameMode> GetAllGameModesByType(int id)
        {
            return [.. GameMode
                        .Where(gm => gm.GameTypeId == id)
                        .Include(gm => gm.GameType)
                        .Include(gm => gm.GameTitle)];
        }
        public List<GameTitle> GetAllGameModes()
            => [.. GameTitle
                    .Include(gt => gt.GameModes)
                    .ThenInclude(gm => gm.GameType)];
        public List<GameMode> GetGameModes()
            => [.. GameMode
                    .Include(gm => gm.GameTitle)
                    .Include(gm => gm.GameType)];
        public void UpdateGameMode(GameMode mode)
            => GameMode.Update(mode);
        public void AddGameMode(GameMode mode)
            => GameMode.Add(mode);
        public void DeleteGameMode(int id)
            => GameMode.Remove(GameMode.Where(gt => gt.GameModeId == id).First());
        public List<GameType> GetGameTypes()
            => [.. GameType];
        public void AddGameType(GameType type)
            => GameType.Add(type);
        public void UpdateGameType(GameType type)
            => GameType.Update(type);

        public void DeleteGameType(int id)
            => GameType.Remove(GameType.Where(gt => gt.GameTypeId == id).First());

        public List<GameTitle> GetGameTitles()
            => [.. GameTitle];
        public void DeleteGameTitle(int id)
            => GameTitle.Remove(GameTitle.Where(gt => gt.GameTitleId == id).First());
        public void AddGameTitle(GameTitle title)
            => GameTitle.Add(title);
        public void UpdateGameTitle(GameTitle title)
            => GameTitle.Update(title);

        public List<Match> GetAllMatchData()
            => [.. Match
                    .Include(m => m.PlayerStats)
                    .Include(m => m.XpStats)
                    .Include(m => m.Map)
                    .Include(m => m.LifetimeStats)
                    .Include(m => m.GameMode.GameTitle)
                    .ThenInclude(gt => gt.GameModes)
                    .Include(m => m.GameMode.GameType)
                    .ThenInclude(gt => gt.GameModes)];
        public void RemoveAllMatchDataByUser(string userId)
            => Match.RemoveRange(GetAllMatchesByUser(userId));
        public List<Match> GetAllMatches()
            => [.. Match];
        public List<Match> GetAllMatchesByUser(string userId)
            => [.. Match
                    .Where(m => m.User.Id == userId)
                    .Include(m => m.PlayerStats)
                    .Include(m => m.LifetimeStats)
                    .Include(m => m.XpStats)];
        //public bool MatchesContainUser(string userId)
        //    => GetAllMatches().Any(m => m.UserMatches.Any(um => um.User.Id == userId));
        public void AddMatchData(Match match)
        {

        }
        public void UploadMatchList(List<Match> matches)
            => Match.AddRange(matches);

        public void AddMap(Map map)
            => Map.Add(map);

        public List<Map> GetAllMaps()
            => [.. Map];

        // https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.take?view=net-9.0&redirectedfrom=MSDN#System_Linq_Queryable_Take__1_System_Linq_IQueryable___0__System_Int32_
        // https://learn.microsoft.com/en-us/ef/ef6/querying/
        public Map GetMapByName(string mapName)
            => Map.Where(m => m.MapName.Replace(" ", string.Empty) == mapName).ToArray()[0];

        public List<PlayerStats> GetPlayerStatsByMatch(int matchId)
            => [.. PlayerStats.Where(ps => ps.MatchId == matchId)];

        public List<LifetimeStats> GetLifetimeStatsByMatch(int matchId)
            => [.. LifetimeStats.Where(ls => ls.MatchId == matchId)];
    }
}
