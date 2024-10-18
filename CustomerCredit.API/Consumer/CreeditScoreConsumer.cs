using MassTransit;
using Messaging.CustomerCredit;
using System.Collections.Concurrent;

namespace CustomerCredit.API.Consumer
{
    public class CreeditScoreConsumer : IConsumer<GetCreditScoreRequest>
    {
        public async Task Consume(ConsumeContext<GetCreditScoreRequest> context)
        {
            if (context.Message.requestAmount > 10000)
            {
                await context.RespondAsync(new CreditNotSuitableResponse("Maksimum 1000,000 e kadar kredi çekebilirsiniz"));
            }
            else
            {
                await context.RespondAsync(new CreditSuitableResponse(80000, creditScore:150));
            }
        }
    }
}
