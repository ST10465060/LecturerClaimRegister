using LecturerClaimRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LecturerClaimRegister.Controllers
{
    /// <summary>
    /// RESTful endpoint exposing the claim records as JSON.
    /// Reads from the same in-memory store as the MVC controller,
    /// so both views of the data stay in sync.
    /// </summary>
    [ApiController]
    [Route("api/claims")]
    [Produces("application/json")]
    public class ClaimsApiController : ControllerBase
    {
        /// <summary>
        /// GET: api/claims
        /// Returns every claim currently held in the store.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Claim>> GetClaims()
        {
            return Ok(ClaimStore.GetAll());
        }

        /// <summary>
        /// GET: api/claims/{id}
        /// Returns a single claim, or 404 if the id does not exist.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Claim> GetClaim(int id)
        {
            var claim = ClaimStore.GetAll().FirstOrDefault(c => c.ClaimId == id);

            if (claim is null)
            {
                return NotFound(new { message = $"No claim found with id {id}." });
            }

            return Ok(claim);
        }
    }
}
