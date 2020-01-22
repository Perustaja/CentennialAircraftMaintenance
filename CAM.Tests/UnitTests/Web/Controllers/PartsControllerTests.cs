using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using CAM.Core.Interfaces.Repositories;
using CAM.Tests.Builders;
using CAM.Web.Controllers;
using CAM.Web.ViewModels.Parts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CAM.Tests.UnitTests.Web.Controllers
{
    public class PartsControllerTests
    {
        private HttpClient _Client;
        // private PartsCreateViewModel _validVm = ;

        // [Fact]
        // public async Task Redirects_To_Login_If_Not_Authenticated()
        // {
        //     var response = await _Client.GetAsync("/inventory/p/new");
        //     response.EnsureSuccessStatusCode();
        //     var responseAsString = await response.Content.ReadAsStringAsync();

        //     Assert.Contains("Forgot your password?", responseAsString);
        // }

        // [Fact]
        // public async Task Create_Returns_Error_If_Duplicate()
        // {
        //     // Arrange
        //     var mockRepo = new Mock<IPartRepository>();
        //     mockRepo.Setup(repo => repo.CheckForExistingRecordAsync(String.Empty).Returns()
        //         .ReturnsAsync(GetTestSessions());
        //     var controller = new HomeController(mockRepo.Object);

        //     // Act
        //     var result = await controller.Index();

        //     // Assert
        //     var viewResult = Assert.IsType<ViewResult>(result);
        //     var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //         viewResult.ViewData.Model);
        //     Assert.Equal(2, model.Count());
        // }
        // [Fact]
        // public async Task Create_Returns_Error_On_Exception()
        // {
        //     // arrange
        //     var repo = new Mock<IPartRepository>();
        //     repo.Setup(r => r.AddAsync(null)).ThrowsAsync(new DbUpdateException());
        //     var controller = new PartsController(repo.Object, _autoMapper.Object, _filerHandler.Object, _logger.Object);

        //     // act
        //     var result = await controller.Create(); 

        // }
        
        [Fact]
        public async Task Create_Redirects_On_Success()
        {
            // arrange
            var repo = new Mock<IPartRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<Part>())).Returns(Task.CompletedTask).Verifiable();
            var mapper = new Mock<IMapper>();
            var fileHandler = new Mock<IFileHandler>();
            fileHandler.Setup(f => f.TrySaveImageAndReturnPathAsync(String.Empty, null, String.Empty)).ReturnsAsync("test");
            var logger = new Mock<ILogger<PartsController>>();

            var controller = new PartsController(repo.Object, mapper.Object, fileHandler.Object, logger.Object);

            // act
            var result = await controller.Create(PartBuilder.ReturnValidPartsCreateViewModel());

            // assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Inventory", redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
            repo.Verify();
        }
    }
}