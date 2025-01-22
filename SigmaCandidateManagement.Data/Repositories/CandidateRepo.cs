using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Core.Interfaces.Repository;
using SigmaCandidateManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Data.Repositories
{
    public class CandidateRepo : ICandidateRepo
    {
        private readonly AppDbContext _context;

        public CandidateRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateAsync(Candidate candidate)
        {
            var existingCandidate = await _context.Candidates.FindAsync(candidate.Email);
            if (existingCandidate == null)
            {
                _context.Candidates.Add(candidate);
            }
            else
            {
                _context.Entry(existingCandidate).CurrentValues.SetValues(candidate);
                _context.Entry(existingCandidate).Property(x => x.Id).IsModified = false;
                _context.Entry(existingCandidate).Property(x => x.Email).IsModified = false;
            }
            await _context.SaveChangesAsync();
        }
    }
}
