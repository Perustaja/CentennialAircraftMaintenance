using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using CAM.Core.Interfaces.Repositories;
using CAM.Web.Controllers;
using CAM.Web.ViewModels.Parts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CAM.Tests.UnitTests.Web.Controllers
{
    public class PartsControllerTests
    {
        private Mock<IFileHandler> _filerHandler;
        private Mock<IMapper> _autoMapper;
        private Mock<ILogger<PartsController>> _logger;
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

            var controller = new PartsController(repo.Object, _autoMapper.Object, _filerHandler.Object, _logger.Object);

            // act
            


        }
        private List<Part> GetTestParts()
        {
            return new List<Part>()
            {
                new Part()
            };
        }
    }
}