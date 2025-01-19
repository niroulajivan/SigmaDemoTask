using SigmaCandidateManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Core.Interfaces.Services
{
    public interface ICandidateService
    {
        Task<Candidate> AddOrUpdateCandidateAsync(Candidate candidate);
       // Task<Candidate> GetCandidateByEmailAsync(string email);
    }
}
