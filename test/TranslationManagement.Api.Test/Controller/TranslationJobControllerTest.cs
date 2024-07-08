using Microsoft.Extensions.Configuration;
using TranslationManagement.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Interfaces;

namespace TranslationManagement.Api.Test
{
    public class TranslationJobControllerTest
    {
        [Fact]
        public void GetJobs_All_ReturnAllJobs()
        {
            var mockTranslationJobs = new List<TranslationJob>();
            for (int i = 0; i < 10; i++)
            {
                mockTranslationJobs.Add(new TranslationJob
                {
                    Id = i,
                    CustomerName = $"Customer Name {i}"
                });
            }

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetJobs()).
                Returns(() => mockTranslationJobs.AsEnumerable());


            var translationJobController = new TranslationJobController(mockRepository.Object);

            var result = translationJobController.GetJobs();

            Assert.Equal(10, result.Count());
            mockRepository.Verify(x => x.GetJobs(), Times.Once());
        }

        [Fact]
        public void UpdateJobStatus_ValidParameters_ReturnsTrue()
        {
            // Arrange
            var jobId = 1;
            var translatorId = 2;
            var newStatus = "Completed";

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.UpdateJobStatus(jobId, translatorId, newStatus))
                .Returns(true);

            var translationJobController = new TranslationJobController(mockRepository.Object);

            // Act
            var result = translationJobController.UpdateJobStatus(jobId, translatorId, newStatus);

            // Assert
            Assert.True(result);
            mockRepository.Verify(x => x.UpdateJobStatus(jobId, translatorId, newStatus), Times.Once());
        }

        [Fact]
        public void UpdateJobStatus_InvalidParameters_ReturnsFalse()
        {
            // Arrange
            var jobId = 1;
            var translatorId = 2;
            var newStatus = "InvalidStatus";

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.UpdateJobStatus(jobId, translatorId, newStatus))
                .Returns(false);

            var translationJobController = new TranslationJobController(mockRepository.Object);

            // Act
            var result = translationJobController.UpdateJobStatus(jobId, translatorId, newStatus);

            // Assert
            Assert.False(result);
            mockRepository.Verify(x => x.UpdateJobStatus(jobId, translatorId, newStatus), Times.Once());
        }

        [Fact]
        public async Task CreateJob_ValidJob_ReturnsOkResult()
        {
            // Arrange
            var job = new TranslationJob
            {
                CustomerName = "Customer Name",
                OriginalContent = "Original Content",
                Price = 10,
                Status = "New",
                TranslatedContent = "Translated Content"
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.CreateJobAsync(job))
                .ReturnsAsync(job);

            var translationJobController = new TranslationJobController(mockRepository.Object);

            // Act
            var result = await translationJobController.CreateJob(job) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateJob_InvalidJob_ReturnsBadRequestResult()
        {
            // Arrange
            var job = new TranslationJob
            {
                CustomerName = "Customer Name",
                OriginalContent = "Original Content",
                Price = 10,
                Status = "New",
                TranslatedContent = "Translated Content"
            };

            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.CreateJobAsync(job))
                .ReturnsAsync((TranslationJob?)null);

            var translationJobController = new TranslationJobController(mockRepository.Object);

            // Act
            var result = await translationJobController.CreateJob(job) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Problem with external service", result.Value);
        }
    }
}
