using Moq;
using SigmaCandidateManagement.Business.Services;
using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SigmaCandidateManagement.Test
{
    public class CandidateServiceTests
    {
        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldAddNewCandidate_WhenEmailIsNew()
        {
            // Arrange
            var newCandidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var mockRepo = new Mock<ICandidateRepo>();
            mockRepo.Setup(r => r.AddOrUpdateAsync(It.IsAny<Candidate>())).Returns(Task.CompletedTask); // Mock AddOrUpdateAsync to return a completed task

            var service = new CandidateService(mockRepo.Object);

            // Act
            var result = await service.AddOrUpdateCandidateAsync(newCandidate);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal("John", result.FirstName);  // Ensure FirstName matches
            Assert.Equal("Doe", result.LastName);   // Ensure LastName matches
            Assert.Equal("john.doe@example.com", result.Email);  // Ensure Email matches

            // Verify that AddOrUpdateAsync was called once with the new candidate
            mockRepo.Verify(r => r.AddOrUpdateAsync(It.IsAny<Candidate>()), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldUpdateExistingCandidate_WhenEmailExists()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var updatedCandidate = new Candidate
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var mockRepo = new Mock<ICandidateRepo>();
            mockRepo.Setup(r => r.AddOrUpdateAsync(It.IsAny<Candidate>())).Returns(Task.CompletedTask); // Mock AddOrUpdateAsync to return a completed task
            mockRepo.Setup(r => r.AddOrUpdateAsync(existingCandidate));  // Mock existing candidate

            var service = new CandidateService(mockRepo.Object);

            // Act
            var result = await service.AddOrUpdateCandidateAsync(updatedCandidate);

            // Assert
            Assert.NotNull(result);  // Ensure result is not null
            Assert.Equal("Jane", result.FirstName);  // Ensure FirstName is updated
            Assert.Equal("Doe", result.LastName);   // Ensure LastName remains unchanged
            Assert.Equal("john.doe@example.com", result.Email);  // Ensure Email remains unchanged

            // Verify that AddOrUpdateAsync was called once with the updated candidate
            mockRepo.Verify(r => r.AddOrUpdateAsync(It.IsAny<Candidate>()), Times.Once);
        }
    }

}
