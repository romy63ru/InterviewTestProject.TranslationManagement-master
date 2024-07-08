using Microsoft.AspNetCore.Mvc;
using Moq;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.Interfaces;

namespace TranslationManagement.Api.Test
{
    public class TranslatorManagementControllerTest
    {
        [Fact]
        public void GetTranslators_ReturnsAllTranslators()
        {
            // Arrange
            var mockTranslators = new List<TranslatorModel>
            {
                new TranslatorModel { Id = 1, Name = "Translator 1" },
                new TranslatorModel { Id = 2, Name = "Translator 2" },
                new TranslatorModel { Id = 3, Name = "Translator 3" }
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetTranslators()).Returns(mockTranslators);

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = translatorManagementController.GetTranslators();

            // Assert
            Assert.Equal(mockTranslators, result);
        }

        [Fact]
        public void GetTranslatorsByName_ValidName_ReturnsTranslators()
        {
            // Arrange
            var name = "Translator 1";
            var mockTranslators = new List<TranslatorModel>
            {
                new TranslatorModel { Id = 1, Name = "Translator 1" },
                new TranslatorModel { Id = 2, Name = "Translator 2" }
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetTranslatorsByName(name)).Returns(mockTranslators);

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = translatorManagementController.GetTranslatorsByName(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(mockTranslators, okResult.Value);
        }

        [Fact]
        public void GetTranslatorsByName_InvalidName_ReturnsNotFound()
        {
            // Arrange
            var name = "Translator 3";
            var mockTranslators = new List<TranslatorModel>
            {
                new TranslatorModel { Id = 1, Name = "Translator 1" },
                new TranslatorModel { Id = 2, Name = "Translator 2" }
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetTranslatorsByName(name)).Returns(new List<TranslatorModel>());

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = translatorManagementController.GetTranslatorsByName(name);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddTranslator_ValidTranslator_ReturnsCreated()
        {
            // Arrange
            var translator = new TranslatorModel { Id = 1, Name = "Translator 1" };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.AddTranslatorAsync(translator)).Returns(Task.FromResult(translator));

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = await translatorManagementController.AddTranslator(translator);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            var createdResult = (CreatedAtActionResult)result;
            Assert.Equal(nameof(TranslatorManagementController.AddTranslator), createdResult.ActionName);
            Assert.Equal(translator, createdResult.Value);
        }

        [Fact]
        public void UpdateTranslatorStatus_ValidParameters_ReturnsOk()
        {
            // Arrange
            var translatorId = 1;
            var newStatus = "Active";

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.UpdateTranslatorStatus(translatorId, newStatus)).Returns(true);

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = translatorManagementController.UpdateTranslatorStatus(translatorId, newStatus);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void UpdateTranslatorStatus_InvalidParameters_ReturnsBadRequest()
        {
            // Arrange
            var translatorId = 1;
            var newStatus = "InvalidStatus";

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.UpdateTranslatorStatus(translatorId, newStatus)).Returns(false);

            var translatorManagementController = new TranslatorManagementController(mockRepository.Object);

            // Act
            var result = translatorManagementController.UpdateTranslatorStatus(translatorId, newStatus);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}