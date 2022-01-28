using CollegeApp_DotNet.BusinessDomain.Interface;
using CollegeApp_DotNet.BusinessDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp_DotNet.WebServices.Controllers
{
    [Route("Accounts")]
    [ApiController]
    public class AccountsController : Controller
    {
        #region Private Variables

        private readonly IAccountsBL accountsBl;

        #endregion

        #region Constructor

        public AccountsController(IAccountsBL accountsBl)
        {
            this.accountsBl = accountsBl;
        }

        #endregion

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] AddUserBM userDetails, string facultyOrStudent)
        {
            var response = this.accountsBl.AddUser(userDetails, facultyOrStudent);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
