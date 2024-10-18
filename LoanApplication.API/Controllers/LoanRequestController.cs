using Messaging.CustomerCredit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestController : ControllerBase
    {
        private readonly IRequestClient<GetCreditScoreRequest> client;

        public LoanRequestController(IRequestClient<GetCreditScoreRequest>client)
        {
            this.client = client;
        }
        [HttpPost]
        public async Task<IActionResult> GetLoanRequest([FromBody] LoanRequestDto request)
        {
            var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);
        }
    }
}
