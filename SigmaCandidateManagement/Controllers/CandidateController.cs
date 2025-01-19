using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Core.Interfaces.Services;

namespace SigmaCandidateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        [HttpPost("addUpdateCandidate")]
        public async Task<IActionResult> AddOrUpdateCandidate(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateService.AddOrUpdateCandidateAsync(candidate);
            return Ok(new { message = "Candidate added or updated successfully" });
        }
    }
}
