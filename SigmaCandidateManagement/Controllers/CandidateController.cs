using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            // Validate the incoming model (candidate)
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                var errorMessage = string.Join(", ", errors);

                // Return a structured error response
                return BadRequest(new ApiResponse<object>(
                    400,
                    "Failed",
                    new { },
                    errorMessage
                ));
            }
            try
            {
                // Perform the add or update operation asynchronously
                var result = await _candidateService.AddOrUpdateCandidateAsync(candidate);

                // Return a success response
                return Ok(new ApiResponse<object>(
                    200,
                    "Success",
                    result,
                    ""
                ));
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during the operation
                return StatusCode(500, new ApiResponse<object>(
                    500,
                    "Error",
                    new { },
                    $"An error occurred while processing your request: {ex.Message}"
                ));
            }
        }

    }
}
