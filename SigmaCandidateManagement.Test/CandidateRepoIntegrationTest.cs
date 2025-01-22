using Microsoft.EntityFrameworkCore;
using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Data;
using SigmaCandidateManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SigmaCandidateManagement.Test
{
    public class CandidateRepoIntegrationTest
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public CandidateRepoIntegrationTest()
        {
            // Use the actual MSSQL connection string
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=localhost;Database=CandidateMgmt;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            // Ensure the database is clean before starting the tests
            using (var context = new AppDbContext(_dbContextOptions))
            {
                context.Database.EnsureCreated(); // Create the database schema if not already existing
            }
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldAddNewCandidate()
        {
            // Arrange
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CandidateRepo(context);

                var newCandidate = new Candidate
                {
                    Email = "anish123@gmail.com",
                    FirstName = "Anish",
                    LastName = "Shrestha",
                    PhoneNumber = "1234567890",
                    Comments = "New candidate",
                };

                // Act
                await repository.AddOrUpdateAsync(newCandidate);

                // Assert
                var savedCandidate = await context.Candidates.FindAsync("anish123@gmail.com");
                Assert.NotNull(savedCandidate);
                Assert.Equal("John", savedCandidate.FirstName);
                Assert.Equal("Doe", savedCandidate.LastName);
            }
        }

        [Fact]
        public async Task AddOrUpdateAsync_ShouldUpdateExistingCandidate()
        {
            // Arrange
            using (var context = new AppDbContext(_dbContextOptions))
            {
                var repository = new CandidateRepo(context);
                var existingCandidateInDb = await context.Candidates.FindAsync("yojana123@gmail.com");
                if (existingCandidateInDb != null)
                {
                    context.Candidates.Remove(existingCandidateInDb);
                    await context.SaveChangesAsync();
                }

                // Add an existing candidate
                var existingCandidate = new Candidate
                {
                    Email = "yojana123@gmail.com",
                    FirstName = "Yojana",
                    LastName = "Subedi",
                    PhoneNumber = "9876543210",
                    Comments = "Existing candidate",
                };
                context.Candidates.Add(existingCandidate);
                await context.SaveChangesAsync();

                // Updated candidate data
                var updatedCandidate = new Candidate
                {
                    Email = "yojana123@gmail.com",
                    FirstName = "UpdatedName",
                    LastName = "UpdatedLast",
                    PhoneNumber = "1112223333",
                    Comments = "Updated candidate details",
                };

                // Act
                await repository.AddOrUpdateAsync(updatedCandidate);

                // Assert
                var savedCandidate = await context.Candidates.FindAsync("yojana123@gmail.com");
                Assert.NotNull(savedCandidate);
                Assert.Equal("UpdatedName", savedCandidate.FirstName);
                Assert.Equal("UpdatedLast", savedCandidate.LastName);
                Assert.Equal("1112223333", savedCandidate.PhoneNumber);
                Assert.Equal("Updated candidate details", savedCandidate.Comments);
            }
        }
    }
}
