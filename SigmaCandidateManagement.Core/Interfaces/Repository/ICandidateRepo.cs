using SigmaCandidateManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Core.Interfaces.Repository
{
    public interface ICandidateRepo
    {
        Task AddOrUpdateAsync(Candidate candidate);
        //Task<Candidate> GetCandidateByEmailAsync(string email);
    }
}
