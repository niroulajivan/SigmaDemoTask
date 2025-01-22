using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidateManagement.Business.Services;
using SigmaCandidateManagement.Core.Entities;
using SigmaCandidateManagement.Core.Entities.Configuration;
using SigmaCandidateManagement.Core.Interfaces.Services;

namespace SigmaCandidateManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// Add or update a candidate.
        /// </summary>
        /// <param name="candidate">Candidate details to be added or updated.</param>
        /// <returns>A status message indicating success or failure.</returns>
        [Authorize]
        [HttpPost("AddUpdateCandidate")]
        public async Task<IActionResult> AddOrUpdateCandidate(Candidate candidate)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList().FirstOrDefault();
                var errorMessage = string.Join(", ", errors);

                return BadRequest(new ApiResponse<object>(
                    400,
                    "Failed",
                    new { },
                    errorMessage
                ));
            }

            // Your logic to create/update the candidate
            var createdCandidate = await _candidateService.AddOrUpdateCandidateAsync(candidate);

            return Ok(new ApiResponse<object>(
                200,
                "Success",
                createdCandidate,
                ""
            ));
        }

    }
}
