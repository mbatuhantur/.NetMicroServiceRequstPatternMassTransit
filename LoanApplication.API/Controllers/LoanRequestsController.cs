using LoanApplication.API.DTOs;
using MassTransit;
using Messaging.CustomerCredit;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestsController:ControllerBase
    {
        private readonly IRequestClient<GetCreditScoreRequest> client;
        public LoanRequestsController(IRequestClient<GetCreditScoreRequest> client)
        {
                this.client = client;
        }
        [HttpPost]
        public async Task<IActionResult> GetLoanRequest([FromBody]LoanRequestDto request)
        {
            var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);

            var response=await this.client.GetResponse<CreditNotSuitableResponse,CreditSuitableResponse>(creditScoreReq);

            if(response.Is(out Response<CreditSuitableResponse> result))
            {
                return Ok(result);
            }
            else if (response.Is(out Response<CreditNotSuitableResponse> failresult))
            {
                return BadRequest(failresult.Message);
            }

            return Ok(response.Message);

        }
    }
}
