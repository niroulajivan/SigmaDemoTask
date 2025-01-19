using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Business.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepo _repository;

        public CandidateService(ICandidateRepo repository)
        {
            _repository = repository;
        }

        public async Task<Candidate> AddOrUpdateCandidateAsync(Candidate candidate)
        {
            await _repository.AddOrUpdateAsync(candidate);
            return candidate;
        }
    }
}
