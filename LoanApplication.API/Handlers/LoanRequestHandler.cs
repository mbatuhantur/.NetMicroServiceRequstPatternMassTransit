using LoanApplication.API.DTOs;
using MassTransit;
using MediatR;
using Messaging.CustomerCredit;

namespace LoanApplication.API.Handlers
{
    public class LoanRequestHandler : IRequestHandler<LoanRequestDto>
    {
        private readonly IRequestClient<GetCreditScoreRequest> client;

        public LoanRequestHandler(IRequestClient<GetCreditScoreRequest> client, IMediator mediator)
        {
            this.client = client;
        }
        // request handler gelen requesti işleyecek olan usecase servisimiz
        public async Task Handle(LoanRequestDto request, CancellationToken cancellationToken)
        {
            var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);

            var response = await this.client.GetResponse<CreditSuitableResponse, CreditNotSuitableResponse>(creditScoreReq);

            await Task.CompletedTask;

        }
    }
}
