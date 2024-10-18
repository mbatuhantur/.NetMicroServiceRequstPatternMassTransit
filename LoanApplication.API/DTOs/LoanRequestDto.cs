using MediatR;

namespace LoanApplication.API.DTOs
{
    public record class LoanRequestDto (string firstName,string accountNumber,decimal requestAmount,decimal annulatIncome) : IRequest;


}


