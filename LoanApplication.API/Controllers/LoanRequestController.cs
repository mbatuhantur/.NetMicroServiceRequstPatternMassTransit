using LoanApplication.API.DTOs;
using MassTransit;
using MassTransit.Mediator;
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
        private readonly IMediator mediator; // kule tüm request sürecini yönetecek
        public LoanRequestController(IRequestClient<GetCreditScoreRequest> client, IMediator mediator)
        {
            this.client = client;
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> GetLoanRequest([FromBody] LoanRequestDto request)
        {
            var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);

            var response = await this.client.GetResponse<CreditSuitableResponse, CreditNotSuitableResponse>(creditScoreReq);

            //return Ok(response.Message);

            if (response.Is(out Response<CreditSuitableResponse> result))
            {
                return Ok(result.Message);
            }
            else if (response.Is(out Response<CreditNotSuitableResponse> result2))
            {
                return BadRequest(result2.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("/v2")]
        public async Task<IActionResult> GetLoanRequestWithMediatR([FromBody] LoanRequestDto request)
        {
            //MassTransit de commandlar Send metodu ile gönderiliyordu. Eventlerde Publish ile göndereliyordu.
            //Message Broker üzerinden çalışan
            //Mediator => sadece API içindeki servislerin event driven geliştrilmesi için bir teknik. In Memory
            //Mediator içinde RequestHandlerlar command görevi görür. Send ile çağırılır.
            //Eğer bir event varsa INotification  interface ile bu süreci yöneteceği<
            //Bu tarz durumlarda ise Publis methodunu kullanacağız


            await this.mediator.Send(request);
            return Ok();
        }
    }
}
