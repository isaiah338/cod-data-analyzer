using cod_data_analyzer.Data;
using cod_data_analyzer.Services;
using cod_data_analyzer.Models;
using cod_data_analyzer.Services.Helper;
using cod_data_analyzer.Services.Util;
using cod_data_analyzer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using Match = cod_data_analyzer.Models.Match;
using Microsoft.AspNetCore.Authorization;
namespace cod_data_analyzer.Controllers
{
    public class AnalyzerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MultiplayerDataDatabaseContext _context;
        private UserManager<ApplicationUser> _userManager;

        public AnalyzerController(
            MultiplayerDataDatabaseContext context,
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("index");
            return View();
        }

        [HttpGet]
        public IActionResult Data(AnalyzerDataViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ResetData()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            _context.RemoveAllMatchDataByUser(userId);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(string matchesEncoded)
        {
            // verify user logged in
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            // decode matches and get user id
            List<Match> allMatches = StringUtil.DeserializedEncodedJson<Match>(matchesEncoded);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            // get all matches for user
            List<Match> matches = _context.GetAllMatchesByUser(userId);

            // add user id to matches 
            foreach(Match m in allMatches)
                m.UserId = userId;


            // check if any matches for user exists
            if(matches != null && matches.Count > 0)
            {
                // if matches exist, get most recent end date

                // all user matches by end datetime
                matches.Sort((x, y) => DateTime.Compare(x.MatchEnd, y.MatchEnd));
                // save last match based on end datetime
                DateTime lastMatchEnd = matches.Last().MatchEnd;

                // collect all matches from upload that are after given date time
                List<Match> validMatches = [.. allMatches.Where(am => am.MatchStart > lastMatchEnd)];


                // update to database with valid matches
                _context.UploadMatchList(validMatches);
                _context.SaveChanges();
            }
            else
            {
                // if not exists, upload all matches with user id
                if (allMatches.Count > 0)
                {
                    _context.UploadMatchList(allMatches);
                    _context.SaveChanges();
                }
            }
            // redirect to load data endpoint
            return View("Index");
        }


        [HttpPost]
        public IActionResult Report(
            AnalyzerDataViewModel viewModel,
            List<int> maps,
            List<int> partyModes, 
            List<int> coreModes,
            string matchesEncoded)
        {
            //_logger.LogInformation("gamemodes:\t"+ core.Count);
            //_logger.LogInformation("maps:\t"+ party.Count);
            //_logger.LogInformation("matches:\t" + map.Count);
            // https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
            if (matchesEncoded.IsNullOrEmpty())
            {
    
                ErrorViewModel errorViewModel = new()
                {
                    Errors = [
                        new UserError()
                            {
                                Header = "No valid matches detected",
                                Detail = "",
                                Severity = 2
                            }
                    ]
                };

                TempData["ErrorModel"] = JsonSerializer.Serialize(errorViewModel);
                return RedirectToAction("Error");
            }

            List<GameMode> allGameModes = _context.GetGameModes();
            List<Map> allMaps = _context.GetAllMaps();
            List<GameTitle> gameTitles = _context.GetAllGameModes();

            List<int> allGameModeId = allGameModes.Select(agm => agm.GameModeId).ToList();

            AnalyzerReportHelper helper = new();
            AnalyzerSettings settings = viewModel.Settings;
            List<KeyValuePair<string, bool>> settingsEnabled = helper.GetSettingsEnabled(viewModel);

            List<Match> allMatches = StringUtil.DeserializedEncodedJson<Match>(matchesEncoded);

            List<GameMode> gamemodes = [];

            foreach (GameTitle gameTitle in gameTitles)
                foreach (GameMode gamemode in gameTitle.GameModes)
                    if (gamemode.GameType.GameTypeId == 1)
                        gamemodes.Add(gamemode);

            

            List<int> selectedGameModeIds = [];
            selectedGameModeIds.AddRange(partyModes);
            selectedGameModeIds.AddRange(coreModes);

            settings.SelectedMaps.AddRange(allMaps.Where(am => maps.Contains(am.MapId)));
            settings.SelectedGameModes.AddRange(allGameModes.Where(agm => selectedGameModeIds.Contains(agm.GameModeId)));

            AnalalyzerReportValidator validator = new(settings, settingsEnabled);
            List<Match> validMatches = validator.FilterMatches(allMatches);
            //validMatches = allMatches;

            foreach (Match m in validMatches)
            {
                m.GameMode = allGameModes.FirstOrDefault(am => am.GameModeId == m.GameModeId);
                m.Map = allMaps.FirstOrDefault(am => am.MapId == m.MapId);
            }

            int matchCount = validMatches.Count;

            ReportTotals totals = helper.GetReportTotals(validMatches);
            ReportAverages averages = helper.GetReportAverages(totals, matchCount);

            AnalyzerReportViewModel reportViewModel = new()
            {
                Matches = validMatches,
                MatchCount = matchCount,
                IsSettingEnabled = settingsEnabled,
                ReportTotals = totals,
                ReportAverages = averages,
                AnalyzerSettings = settings
            };
           return View(reportViewModel);
        }
        [HttpGet]
        public IActionResult LoadMatches()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            List<Match> matchData = _context.GetAllMatchesByUser(userId); // Fetch data
            List<Map> maps = _context.GetAllMaps();
            List<GameMode> coreGameModes = _context.GetAllGameModesByType(1);
            List<GameMode> partyGameModes = _context.GetAllGameModesByType(3);

            AnalyzerDataViewModel viewModel = new()
            {
                Matches = matchData,
                Settings = new AnalyzerSettings(),
                Maps = maps,
                PartyGameModes = partyGameModes,
                CoreGameModes = coreGameModes
            };
            //foreach(Map m in )
            return View("Data", viewModel);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            
            // check for empty file
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // init parser utility object & filtered lines list
            MultiplayerDataParser parser = new MultiplayerDataParser();
            List<string[]> filteredCsvList = await parser.FilterCsvFromFileUpload(file);


            // save list of maps and gamemodes to find all map
            // data for match in view model
            List<Map> maps = _context.GetAllMaps();

            // get all game mode titles from db
            List<GameMode> gameModes = _context.GetGameModes();

            List<Match> matches = parser.GetMatchDataFromCsvList(maps, gameModes, filteredCsvList);
            AnalyzerDataViewModel viewModel = new()
            {
                Matches = matches,
                Settings = new AnalyzerSettings(),
                Maps = _context.GetAllMaps(),
                CoreGameModes = _context.GetAllGameModesByType(1),
                PartyGameModes = _context.GetAllGameModesByType(3)
            };
            return View("Data", viewModel);
        }


        public IActionResult Error()
        {
            if (TempData["ErrorModel"] is string json)
            {
                ErrorViewModel model 
                        = JsonSerializer.Deserialize<ErrorViewModel>(json)
                            ?? new ErrorViewModel();
                return View(model);
            }

            return View(new ErrorViewModel()); // fallback
        }


    }
}
