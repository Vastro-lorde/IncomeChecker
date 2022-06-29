using IncomeChecker.Models;
using IncomeChecker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeChecker.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IHttpClientManager _httpClientManager;
        private readonly IAuth _auth;
        public IncomeController(IHttpClientManager httpClientManager, IAuth auth)
        {
            _httpClientManager = httpClientManager;
            _auth = auth;
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <remarks> Generated Token expires in 10mins</remarks>
        /// <returns>Generated Token</returns>
        [HttpGet("/createToken")]
        [Produces("application/json")]
        [ProducesDefaultResponseType]
        public IActionResult GetToken() => Ok(_auth.CreateToken());


        /// <summary>
        /// Gets the User Income
        /// </summary>
        /// <returns>Return the users average, yearly and monthly income</returns>
        /// <param name="Id">Id of account</param>
        /// <remarks>
        /// Sample Request
        /// GET /api/Income?Id=62b2d5f6f5c89628113e0d06
        /// </remarks>
        [Produces("application/json")]
        [HttpGet, Authorize]
        [ProducesResponseType(typeof(IncomeResponseModel), 200), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIncome(string Id)
        {
            if (Id == null || Id == "") return BadRequest();

            try
            {
                string requestUrl = $"{Id.Trim()}/income"; // concatinating the strings to get ID/income which is required in the endpoint of mono income.
                var result = await _httpClientManager.GetRequestAsync(requestUrl);
                if (!result.IsSuccessStatusCode) return NotFound("Account Not found");
                return Ok(result);
            }
            catch (Exception er)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,er.Message);
            }
            
        }
    }
}
