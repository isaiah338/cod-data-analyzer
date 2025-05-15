
using cod_data_analyzer.Controllers;
using cod_data_analyzer.Data;
using System.Security.Claims;
using cod_data_analyzer.Models;
using cod_data_analyzer.Services.Helper;
using cod_data_analyzer.Services.Util;
using cod_data_analyzer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Match = cod_data_analyzer.Models.Match;

[TestClass]
public class AnalalyzerReportValidatorTests
{
    [TestMethod]
    public void AnalalyzerReportValidatorTest_ValidMatch_PassesAllChecks()
    {
        // Arrange
        var settings = new AnalyzerSettings
        {
            MinScore = 500,
            MinSkill = 100,
            MatchQuitThreshold = 60,
            isGameModeWhitelist = true,
            isMapWhitelist = true,
            SelectedGameModes = new List<GameMode> { new GameMode { GameModeId = 1 } },
            SelectedMaps = new List<Map> { new Map { MapId = 1 } },
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2025, 1, 1)
        };

        var enabledSettings = new List<KeyValuePair<string, bool>>
        {
            new("MatchQuitThreshold", true),
            new("MinScore", true),
            new("MinSkill", true),
            new("DateRange", true),
            new("MinTimePlayedTopGame", false),
            new("MinTimePlayedTopMap", false),
        };

        var validator = new AnalalyzerReportValidator(settings, enabledSettings);

        var match = new Match
        {
            MatchLength = 120,
            MatchStart = new DateTime(2024, 6, 1),
            GameModeId = 1,
            MapId = 1,
            PlayerStats = new PlayerStats
            {
                Score = 1000,
                Skill = 200
            }
        };

        // Act
        bool isValid = validator.ValidMatch(match);

        // Assert
        Assert.IsTrue(isValid, "Expected match to be valid but it was filtered out.");
    }

    [TestClass]
    public class MathUtilTests
    {
        [TestMethod]
        public void Average_NormalValues_ReturnsCorrectResult()
        {
            float result = MathUtil.Average(10f, 2f);
            Assert.AreEqual(5f, result);
        }

        [TestMethod]
        public void Average_DivideByZero_ReturnsZero()
        {
            float result = MathUtil.Average(10f, 0f);
            Assert.AreEqual(0f, result);
        }

        [TestMethod]
        public void SecondsElapsedInt_CorrectCalculation()
        {
            DateTime start = new DateTime(2024, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2024, 1, 1, 0, 1, 40); // 100 seconds later

            int result = MathUtil.SecondsElapsedInt(start, end);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void SecondsElapsedLong_CorrectCalculation()
        {
            DateTime start = new DateTime(2024, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2024, 1, 1, 0, 5, 0); // 300 seconds later

            long result = MathUtil.SecondsElapsedLong(start, end);
            Assert.AreEqual(300, result);
        }
    }
    [TestClass]
    public class AnalyzerControllerTests
    {

       
    }

}
