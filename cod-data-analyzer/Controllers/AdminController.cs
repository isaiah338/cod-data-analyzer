using Microsoft.AspNetCore.Mvc;
using cod_data_analyzer.Models;
using cod_data_analyzer.ViewModels;
using cod_data_analyzer.Data;
using Microsoft.AspNetCore.Authorization;
namespace cod_data_analyzer.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private MultiplayerDataDatabaseContext _context;
        public AdminController(MultiplayerDataDatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index() {
            List<GameTitle> titles = _context.GetAllGameModes();
            return View(new AdminViewModel
               {
                   Maps = _context.GetAllMaps(),
                   Matches = _context.GetAllMatchData(),
                   Modes = titles.Where(t => t.GameModes.Any(gm => gm.GameType.GameTypeName != "party")).ToList(),
               });
        }

        //[HttpPost]
        //public IActionResult Maps([FromBody] AdminMapsViewModel model)
        //{
        //    // verify map exists
        //    if(ModelState.IsValid)
        //    {
        //        _context.AddMap(model.EditMap);
        //        _context.SaveChanges();
        //    }
            

        //    return Ok(model);
        //}

        [HttpPut]
        public IActionResult Maps([FromBody] AdminMapsViewModel model, [FromQuery] int id)
        {
            // verify map exists
            if (model?.EditMap == null)
                return BadRequest("Invalid payload");

            model.EditMap.MapId = id;
            _context.UpdateMap(model.EditMap);
            _context.SaveChanges();

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Maps( )
        {
            List<Map> maps = _context.GetAllMaps();
            AdminMapsViewModel viewModel = new() { 
                AllMaps = maps,
                EditMap = new Map()
            };
            return View(viewModel);
        }

        [HttpDelete]
        public IActionResult Maps([FromQuery] int id)
        {
            _context.DeleteMap(id);
            _context.SaveChanges();
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Maps([FromBody] AdminMapsViewModel model)
        {
            if (model?.EditMap == null)
                return BadRequest("Invalid payload");

            _context.AddMap(model.EditMap);
            _context.SaveChanges();

            return Ok(model.EditMap);
        }


        //[HttpPut]
        //public IActionResult Maps([FromBody] AdminMapsViewModel model, [FromQuery] int id)
        //{
        //    // verify map exists

        //    model.EditMap.MapId = id;
        //    _context.UpdateMap(model.EditMap);
        //    _context.SaveChanges();

        //    return Ok(model);
        //}

        [HttpGet]
        public IActionResult Modes()
        {
            List<GameMode> gameModes = _context.GetGameModes();
            List<GameType> gameTypes = _context.GetGameTypes();
            List<GameTitle> gameTitles = _context.GetGameTitles();
            AdminModesViewModel viewModel = new() { 
                GameModes = gameModes,
                GameTitles = gameTitles,
                GameTypes = gameTypes
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Modes([FromBody] AdminModesViewModel model)
        {
            if (model?.EditGameMode == null)
                return BadRequest("Invalid payload");

            _context.AddGameMode(model.EditGameMode);
            _context.SaveChanges();

            return Ok(model.EditGameMode);
        }
        [HttpPut]
        public IActionResult Modes([FromBody] AdminModesViewModel model, [FromQuery] int id)
        {
            if (model?.EditGameMode == null)
                return BadRequest("Invalid payload");

            _context.UpdateGameMode(model.EditGameMode);
            _context.SaveChanges();
            return Ok(id);
        }
        [HttpDelete]
        public IActionResult Modes([FromQuery] int id)
        {
            _context.DeleteGameMode(id);
            _context.SaveChanges();
            return Ok(id);
        }
        [HttpPost]
        public IActionResult ModeType([FromBody] AdminModesViewModel model)
        {
            if (model?.EditType == null)
                return BadRequest("Invalid payload");
            
            _context.AddGameType(model.EditType);
            _context.SaveChanges();

            return Ok(model.EditType);
        }
        [HttpPut]
        public IActionResult ModesType([FromBody] AdminModesViewModel model, [FromQuery] int id)
        {
            // verify map exists
            if (model?.EditType == null)
                return BadRequest("Invalid payload");

            _context.UpdateGameType(model.EditType);
            _context.SaveChanges();

            return Ok(model);
        }


        [HttpDelete]
        public IActionResult ModesType([FromQuery] int id)
        {
            _context.DeleteGameType(id);
            _context.SaveChanges();
            return Ok(id);
        }

        [HttpPut]
        public IActionResult ModesTitle([FromBody] AdminModesViewModel model, [FromQuery] int id)
        {
            if (model?.EditTitle == null)
                return BadRequest("Invalid payload");

            _context.UpdateGameTitle(model.EditTitle);
            _context.SaveChanges();
            return Ok(id);
        }
        [HttpDelete]
        public IActionResult ModesTitle([FromQuery] int id)
        {
            _context.DeleteGameTitle(id);
            _context.SaveChanges();
            return Ok(id);
        }
        [HttpPost]
        public IActionResult ModeTitle([FromBody] AdminModesViewModel model)
        {
            if (model?.EditTitle == null)
                return BadRequest("Invalid payload");

            _context.AddGameTitle(model.EditTitle);
            _context.SaveChanges();

            return Ok(model.EditTitle);
        }

        //[HttpPost]
        //public IActionResult Maps([FromBody] AdminMapsViewModel viewModel)
        //{
        //    _context.AddMap(viewModel.EditMap);
        //    _context.SaveChanges();

        //    return Ok(viewModel.EditMap);
        //}

    }
}
