using MassTransit;
using Messaging.CustomerCredit;

namespace CustomerCredit.API.Consumers
{
    public class CreditScoreConsumer : IConsumer<GetCreditScoreRequest>
    {
        public async Task Consume(ConsumeContext<GetCreditScoreRequest> context)
        {
            if (context.Message.requestAmount > 100000)
            {
               await context.RespondAsync(new CreditNotSuitableResponse("Maksimum 1000,000 e kadar kredi çekebilirsin"));
            }
            else {
               

                await context.RespondAsync(new CreditSuitableResponse(800000,creditScore:150));
            }
        }
    }
}
